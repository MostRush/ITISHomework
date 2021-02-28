using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace FightingGame
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var firstPlayerKeys = new ControlKeys
            {
                Test = ConsoleKey.F,
                BlockAttack = ConsoleKey.S,
                PirouetteAttack = ConsoleKey.W,
                VortexAttack = ConsoleKey.D,
                UseSkill = ConsoleKey.A
            };

            var secondPlayerKeys = new ControlKeys
            {
                Test = ConsoleKey.Enter,
                BlockAttack = ConsoleKey.DownArrow,
                PirouetteAttack = ConsoleKey.UpArrow,
                VortexAttack = ConsoleKey.LeftArrow,
                UseSkill = ConsoleKey.RightArrow
            };

            var isGameWithBot = false;

            var firstPlayer = new Player("Heralt", firstPlayerKeys, new Point(13, 10));

            Game gameObject;

            if (isGameWithBot)
            {
                var secondPlayer = new PlayerBot("Vesemir (Bot)", secondPlayerKeys, new Point(78, 10));
                secondPlayer.Animations = InitAnimations(secondPlayer.Position, "character2");

                gameObject = new Game(firstPlayer, secondPlayer);
            }
            else
            {
                var secondPlayer = new Player("Vesemir", secondPlayerKeys, new Point(78, 10));
                secondPlayer.Animations = InitAnimations(secondPlayer.Position, "character2");

                gameObject = new Game(firstPlayer, secondPlayer);
            }

            firstPlayer.Animations = InitAnimations(firstPlayer.Position, "character1");

            await gameObject.StartGameAsync();
        }

        static public List<Animation> InitAnimations(Point position, string character)
        {
            var animations = new List<Animation>();

            var animNames = new string[]
            {
                "pirouette_attack",
                "armed_handle",
                "vortex_attack",
                "poof_die",
                "block_attack",
                "set_block"
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
