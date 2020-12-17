using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FightingGame
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var firstPlayerKeys = new ControlKeys
            {
                BlockAttack = ConsoleKey.A,
                Test = ConsoleKey.W,
                PirouetteAttack = ConsoleKey.D,
                VortexAttack = ConsoleKey.S,
                UseSkill = ConsoleKey.F
            };

            var secondPlayerKeys = new ControlKeys
            {
                BlockAttack = ConsoleKey.LeftArrow,
                Test = ConsoleKey.UpArrow,
                PirouetteAttack = ConsoleKey.RightArrow,
                VortexAttack = ConsoleKey.DownArrow,
                UseSkill = ConsoleKey.Enter
            };

            var firstPlayer = new Player("Heralt", firstPlayerKeys, new Point(10, 10));
            var secondPlayer = new Player("Vesemir", secondPlayerKeys, new Point(75, 10));

            firstPlayer.Animations = InitAnimations(firstPlayer.Position, "character1");
            secondPlayer.Animations = InitAnimations(secondPlayer.Position, "character2");

            var tasks = new List<Task>();

            var game = new Game(firstPlayer, secondPlayer);
            var t3 = game.StartGame();

            tasks.Add(t3);

            await Task.WhenAll(tasks);
        }

        static public List<Animation> InitAnimations(Point position, string character)
        {
            var animations = new List<Animation>();

            var animNames = new string[]
            {
                "pirouette_attack",
                "armed_handle",
                "vortex_attack",
                "poof_die"
            };

            foreach (var name in animNames)
            {
                var animPath = Environment.CurrentDirectory + @$"\materials\anims\{character}\{name}";
                var anim = new Animation(name, position, animPath);
                anim.IsLooped = name.Contains("handle");
                anim.FrameDelay = name.Contains("handle") ? 100 : 50;
                anim.Color = name.Contains("poof") ? ConsoleColor.White : ConsoleColor.Gray;

                animations.Add(anim);
            }

            return animations;
        }
    }
}
