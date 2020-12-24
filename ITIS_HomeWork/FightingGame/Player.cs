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

    enum ActionType
    {
        Died,
        Handle,
        UseSkill,
        BlockAttack,
        PirouetteAttack,
        VortexAttack,
    }

    class Player
    {
        public ControlKeys? KeyBinds { get; set; }
        public Game GameObject { get; set; }
        public Player Rival { get; set; }
        public Point Position { get; set; }
        public List<Animation> Animations { get; set; }
        public bool IsInBlock { get; private set; }
        public ActionType CurrentAction { get; set; }

        public string Name { get; set; }
        public int MaxHealth { get; set; }
        public double MaxAdrenaline { get; set; }

        private int health = 100;
        private double adrenaline = 0;

        public int Health
        {
            get { return health; }
            set
            {
                health = value < 0 ? 0 : value > MaxHealth ? MaxHealth : value;
            }
        }
        public double Adrenaline
        {
            get { return adrenaline; }
            set
            {
                adrenaline = value < 0 ? 0 : value > 3 ? 3 : value;
            }
        }

        public Player(string name, ControlKeys? keyBinds, Point point, int maxHealth = 100, int maxAdrenaline = 3)
        {
            Name = name;
            Position = point;
            Health = MaxHealth = health;
            MaxAdrenaline = maxAdrenaline;

            this.KeyBinds = keyBinds;
        }

        private async Task PlayAnimAsync(string animName)
        {
            foreach (var anim in Animations)
                anim.Stop();

            var animation = Animations.FirstOrDefault(x => x.Name.Equals(animName));
            await animation.PlayAsync();
        }

        public void ChageAllAnimColors(ConsoleColor color)
        {
            foreach (var animation in Animations)
                animation.Color = color;
        }

        private async Task FlashingAsync(ConsoleColor firstColor, ConsoleColor secondColor, int times, int delay)
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
            CurrentAction = ActionType.Handle;
            _ = PlayAnimAsync("armed_handle");
        }

        public async Task PirouetteAttackAsync()
        {
            if (CurrentAction == ActionType.Died) return;

            if (CurrentAction != ActionType.Handle
                && CurrentAction != ActionType.BlockAttack) return;

            CurrentAction = ActionType.PirouetteAttack;
            IsInBlock = false;
            var random = new Random();

            if (!Rival.IsInBlock)
            {
                if (Rival.CurrentAction != ActionType.Died)
                {
                    Rival.Health -= random.Next(6, 8);
                    Adrenaline += GetRandomNumber(0.1, 0.2);
                }
            }
            else
            {
                _ = Rival.PlayAnimAsync("block_attack");
            }

            var color = Rival.IsInBlock ? ConsoleColor.Blue : ConsoleColor.Red;
            var task1 = Rival.FlashingAsync(color, ConsoleColor.Gray, 5, 150);
            var task2 = PlayAnimAsync("pirouette_attack");

            await Task.WhenAll(new[] { task1, task2 });

            CurrentAction = ActionType.Handle;
            _ = PlayAnimAsync(IsInBlock ? "block_attack" : "armed_handle");

            if (Rival.Health <= 0 && Rival.CurrentAction != ActionType.Died)
            {
                _ = Rival.PlayAnimAsync("poof_die");
                Rival.CurrentAction = ActionType.Died;
            }

            _ = GameObject.InterfaceUpdateAsync();
        }

        public async Task VortexAttackAsync()
        {
            if (CurrentAction == ActionType.Died) return;

            if (CurrentAction != ActionType.Handle
                && CurrentAction != ActionType.BlockAttack) return;

            CurrentAction = ActionType.VortexAttack;
            IsInBlock = false;
            var random = new Random();

            if (!Rival.IsInBlock)
            {
                if (Rival.CurrentAction != ActionType.Died)
                {
                    Rival.Health -= random.Next(3, 4);
                    Adrenaline += GetRandomNumber(0.2, 0.3);
                }
            }
            else
            {
                _ = Rival.PlayAnimAsync("block_attack");
            }

            var color = Rival.IsInBlock ? ConsoleColor.Blue : ConsoleColor.Red;
            var task1 = Rival.FlashingAsync(color, ConsoleColor.Gray, 3, 100);
            var task2 = PlayAnimAsync("vortex_attack");

            await Task.WhenAll(new[] { task1, task2 });

            CurrentAction = ActionType.Handle;
            _ = PlayAnimAsync(IsInBlock ? "block_attack" : "armed_handle");

            if (Rival.Health <= 0 && Rival.CurrentAction != ActionType.Died)
            {
                _ = Rival.PlayAnimAsync("poof_die");
                Rival.CurrentAction = ActionType.Died;
            }

            _ = GameObject.InterfaceUpdateAsync();
        }

        public async Task UseSkill()
        {
            if (CurrentAction == ActionType.Died) return;

            if (CurrentAction != ActionType.Handle
                && CurrentAction != ActionType.BlockAttack) return;

            CurrentAction = ActionType.UseSkill;

            if (Adrenaline >= 3 && !IsInBlock && Rival.CurrentAction != ActionType.Died)
            {
                IsInBlock = false;
                var random = new Random();

                Rival.Health -= random.Next(13, 17);
                Adrenaline = 0;

                _ = PlayAnimAsync("armed_handle");

                CurrentAction = ActionType.Handle;

                if (Rival.Health <= 0)
                {
                    _ = Rival.PlayAnimAsync("poof_die");
                    Rival.CurrentAction = ActionType.Died;
                }

                _ = GameObject.InterfaceUpdateAsync();
            }

            CurrentAction = ActionType.Handle;
        }

        public async Task SetBlockedAsync()
        {
            if (CurrentAction == ActionType.Died) return;

            if (CurrentAction != ActionType.Handle) return;

            IsInBlock = !IsInBlock;

            if (IsInBlock)
            {
                await PlayAnimAsync("set_block");
                CurrentAction = ActionType.BlockAttack;
            }
            else
            {
                CurrentAction = ActionType.Handle;
                _ = PlayAnimAsync("armed_handle");
            }
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
