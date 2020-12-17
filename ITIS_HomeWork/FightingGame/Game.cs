using System;
using System.Collections.Generic;
using System.IO;
using System.Media;
using System.Threading;
using System.Threading.Tasks;

namespace FightingGame
{
    class Game
    {
        public static object locker = new object();

        Player player1;
        Player player2;

        public Dictionary<string, SoundPlayer> Sounds { get; set; }
        public Dictionary<string, string> StaticObjects { get; set; }

        public Game(Player player1, Player player2)
        {
            this.player1 = player1;
            this.player2 = player2;

            this.player1.Rival = player2;
            this.player2.Rival = player1;

            this.player1.GameObject = this;
            this.player2.GameObject = this;
        }

        public async Task StartGame()
        {
            Console.SetWindowSize(155, 45);

            var movemontTask = Task.Run(() => MovementHandler());

            player1.Initialization();
            player2.Initialization();

            Sounds = this.InitSounds();
            StaticObjects = this.InitStaticObjects();

            _ = DrawStaticObjects();
            _ = InterfaceUpdate();

            await Task.WhenAll(new[] { movemontTask });
        }

        public void StopGame()
        {

        }

        private Dictionary<string, SoundPlayer> InitSounds()
        {
            var sounds = new Dictionary<string, SoundPlayer>();

            var soundPath = Environment.CurrentDirectory + @$"\sounds\";
            var soundFiles = Directory.GetFiles(soundPath);

            foreach (var sound in soundFiles)
            {
                sounds[Path.GetFileNameWithoutExtension(sound)] = new SoundPlayer(sound);
                sounds[Path.GetFileNameWithoutExtension(sound)].Load();
            }

            return sounds;
        }

        private Dictionary<string, string> InitStaticObjects()
        {
            var objects = new Dictionary<string, string>();

            var staticPath = Environment.CurrentDirectory + @$"\materials\static\";
            var txtFiles = Directory.GetFiles(staticPath);

            foreach (var txt in txtFiles)
            {
                objects[Path.GetFileNameWithoutExtension(txt)] = File.ReadAllText(txt);
            }

            return objects;
        }

        private void MovementHandler()
        {
            while (!Console.KeyAvailable)
            {
                var key = Console.ReadKey(true).Key;

                var players = new[] { player1, player2 };

                foreach (var player in players)
                {
                    if (key == player.KeyBinds.Test) player.TestMethod();

                    if (key == player.KeyBinds.BlockAttack) player.BlockAttack();

                    if (key == player.KeyBinds.PirouetteAttack) player.PirouetteAttack();

                    if (key == player.KeyBinds.VortexAttack) player.VortexAttack();

                    if (key == player.KeyBinds.UseSkill) player.UseSkill();
                }
            }
        }

        public async Task InterfaceUpdate()
        {
            await Task.Run(() =>
            {
                lock (Game.locker)
                {
                    Console.ForegroundColor = ConsoleColor.White;

                    Console.SetCursorPosition(20, 5);
                    Console.Write(new string(' ', 40));
                    Console.SetCursorPosition(20, 5);
                    Console.WriteLine(player1);

                    Console.SetCursorPosition(100, 5);
                    Console.Write(new string(' ', 40));
                    Console.SetCursorPosition(100, 5);
                    Console.WriteLine(player2);
                }
            });
        }

        public async Task DrawStaticObjects()
        {
            await Task.Run(() =>
            {
                lock (Game.locker)
                {
                    Console.SetCursorPosition(0, 0);
                    Console.Write(new string('_', 155));

                    Console.SetCursorPosition(0, 43);
                    Console.Write(new string('_', 155));

                    Console.CursorTop = 3;

                    var strings = StaticObjects["hp_bar_1"].Split('\n');

                    foreach (var str in strings)
                    {
                        Console.CursorLeft = 5;
                        Console.WriteLine(str);
                    }

                    Console.CursorTop = 3;

                    strings = StaticObjects["hp_bar_2"].Split('\n');

                    foreach (var str in strings)
                    {
                        Console.CursorLeft = 95;
                        Console.WriteLine(str);
                    }

                    Console.SetCursorPosition(0, 37);
                    Console.Write(StaticObjects["floor"]);
                }
            });
        }
    }
}
