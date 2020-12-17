using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FightingGame
{
    struct ControlKeys
    {
        public ConsoleKey Test { get; set; }
        public ConsoleKey BlockAttack { get; set; }
        public ConsoleKey PirouetteAttack { get; set; }
        public ConsoleKey VortexAttack { get; set; }
        public ConsoleKey UseSkill { get; set; }

        public override string ToString()
        {
            return $"simp: {Test} pow: {BlockAttack} pir: {PirouetteAttack} vor: {VortexAttack} skill: {UseSkill}";
        }
    }

    enum AttackType
    {
        BlockAttack,
        PirouetteAttack,
        VortexAttack,
        UseSkill
    }

    class Player
    {
        public ControlKeys KeyBinds { get; set; }
        public Game GameObject { get; set; }
        public Player Rival { get; set; }
        public Point Position { get; set; }
        public List<Animation> Animations { get; set; }
        public bool IsInBlock { get; private set; }

        public string Name { get; set; }
        public int Health { get; set; }
        public double Adrenaline { get; set; }

        public Player(string name, ControlKeys keyBinds, Point point, int health = 100, int adrenaline = 0)
        {
            Name = name;
            Health = health;
            Adrenaline = adrenaline;
            Position = point;

            this.KeyBinds = keyBinds;
        }

        private async Task PlayAnim(string animName)
        {
            foreach (var anim in Animations)
                anim.Stop();

            var animation = Animations.FirstOrDefault(x => x.Name.Equals(animName));
            await animation.Play();
        }

        public void ChageAllAnimColors(ConsoleColor color)
        {
            foreach (var animation in Animations)
                animation.Color = color;
        }

        private async Task Flashing(ConsoleColor firstColor, ConsoleColor secondColor, int times, int delay)
        {
            await Task.Run(() =>
            {
                var isOtherColor = false;

                for (int i = 0; i < times; i++)
                {
                    this.ChageAllAnimColors(isOtherColor ? firstColor : secondColor);
                    Thread.Sleep(delay);

                    isOtherColor = !isOtherColor;
                }
            });
        }

        internal void TestMethod()
        {
            Position = new Point(Position.X + 1, Position.Y);

            var anim = Animations.FirstOrDefault(x => x.Name.Contains("handle"));
            var rand = new Random();

            anim.Color = (ConsoleColor)rand.Next(1, 15);
        }

        public void Initialization()
        {
            _ = PlayAnim("armed_handle");
        }

        public async void PirouetteAttack()
        {
            var random = new Random(DateTime.Now.Second);

            if (!Rival.IsInBlock)
            {
                _ = Task.Run(() =>
                {
                    Rival.Health -= random.Next(2, 4);

                    _ = GameObject.InterfaceUpdate();

                    Thread.Sleep(500);
                    Rival.Health -= random.Next(2, 4);

                    Adrenaline += GetRandomNumber(0.1, 0.2);

                    _ = GameObject.InterfaceUpdate();
                });
            }

            var color = Rival.IsInBlock ? ConsoleColor.Blue : ConsoleColor.Red;
            _ = Rival.Flashing(color, ConsoleColor.Gray, 5, 150);

            await PlayAnim("pirouette_attack");

            _ = PlayAnim("armed_handle");

            if (Rival.Health < 0)
                _ = Rival.PlayAnim("poof_die");

            _ = GameObject.InterfaceUpdate();
        }

        public async void VortexAttack()
        {
            var random = new Random(DateTime.Now.Second);

            if (!Rival.IsInBlock)
            {
                Rival.Health -= random.Next(2, 4);
                Adrenaline += GetRandomNumber(0.2, 0.3);
            }

            var color = Rival.IsInBlock ? ConsoleColor.Blue : ConsoleColor.Red;
            var task1 = Rival.Flashing(color, ConsoleColor.Gray, 3, 100);
            var task2 = PlayAnim("vortex_attack");

            await Task.WhenAll(new[] { task1, task2 });

            _ = PlayAnim("armed_handle");

            if (Rival.Health < 0)
                _ = Rival.PlayAnim("poof_die");

            _ = GameObject.InterfaceUpdate();
        }

        public async void BlockAttack()
        {
            await Task.Run(() =>
            {
                IsInBlock = true;

                Thread.Sleep(500);

                IsInBlock = false;
            });
        }

        public void UseSkill()
        {
            var random = new Random(DateTime.Now.Second);

            if (Adrenaline >= 3 && !IsInBlock)
            {
                Rival.Health -= random.Next(12, 16);
                Adrenaline = 0;
            }

            if (Rival.Health < 0)
                _ = Rival.PlayAnim("poof_die");

            _ = GameObject.InterfaceUpdate();
        }

        public override string ToString()
        {
            return $"Name: {Name} - hp: {Health} - adr: {Math.Round(Adrenaline, 2)}";
        }

        private double GetRandomNumber(double minimum, double maximum)
        {
            var random = new Random(DateTime.Now.Second);
            return random.NextDouble() * (maximum - minimum) + minimum;
        }
    }
}
