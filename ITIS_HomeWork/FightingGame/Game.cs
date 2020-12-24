using System;
using System.IO;
using System.Media;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace FightingGame
{
    class Game
    {
        public static object locker = new object();

        public bool IsGameWithBot { get; private set; }

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

            IsGameWithBot = (typeof(PlayerBot).Equals(player2.GetType()));
        }

        public Game(Player player1, PlayerBot player2)
        {
            this.player1 = player1;
            this.player2 = player2;

            this.player1.Rival = player2;
            this.player2.Rival = player1;

            this.player1.GameObject = this;
            this.player2.GameObject = this;

            IsGameWithBot = (typeof(PlayerBot).Equals(player2.GetType()));
        }

        public async Task StartGameAsync()
        {
            Console.SetWindowSize(155, 45);

            var movemontTask = Task.Run(() => MovementHandler());

            player1.Initialization();
            player2.Initialization();

            Sounds = this.InitSounds();
            StaticObjects = this.InitStaticObjects();

            if (IsGameWithBot)
                _ = (player2 as PlayerBot).AsyncBotActions();

            _ = DrawStaticObjectsAsync();
            _ = InterfaceUpdateAsync();

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

                var players = IsGameWithBot ? new[] { player1 } : new[] { player1, player2 };

                foreach (var player in players)
                {
                    if (key == player.KeyBinds?.Test) player.TestMethod();

                    if (key == player.KeyBinds?.BlockAttack) player.SetBlockedAsync();

                    if (key == player.KeyBinds?.PirouetteAttack) player.PirouetteAttackAsync();

                    if (key == player.KeyBinds?.VortexAttack) player.VortexAttackAsync();

                    if (key == player.KeyBinds?.UseSkill) player.UseSkill();
                }
            }
        }

        public async Task InterfaceUpdateAsync()
        {
            await Task.Run(() =>
            {
                lock (Game.locker)
                {
                    /* Drawing health bar */

                    DrawHealthBarLine(new Point(17, 5), player1, false);
                    DrawHealthBarLine(new Point(88, 5), player2, true);

                    DrawAdrenalineLine(new Point(6, 8), player1, false);
                    DrawAdrenalineLine(new Point(124, 8), player2, true);

                    Console.ForegroundColor = ConsoleColor.White;

                    Console.SetCursorPosition(20, 2);
                    Console.Write(new string(' ', 40));
                    Console.SetCursorPosition(20, 2);
                    Console.WriteLine(player1);

                    Console.SetCursorPosition(100, 2);
                    Console.Write(new string(' ', 40));
                    Console.SetCursorPosition(100, 2);
                    Console.WriteLine(player2);
                }
            });
        }

        private void DrawHealthBarLine(Point position, Player player, bool isReversive)
        {
            var previousColor = Console.ForegroundColor;

            var percentHealth = 100 / player.MaxHealth * player.Health;

            var healthLineColor = percentHealth <= 25 ? ConsoleColor.Red :
                percentHealth <= 50 ? ConsoleColor.Yellow : ConsoleColor.Green;

            Console.CursorTop = position.Y;
            Console.CursorLeft = position.X;
            Console.ForegroundColor = healthLineColor;

            if (isReversive)
            {
                Console.Write(new string(' ', (int)Math.Round((player.MaxHealth - player.Health) / 2d)));
                Console.Write(new string('/', (int)Math.Round(player.Health / 2d)));
            }
            else
            {
                Console.Write(new string('\\', (int)Math.Round(player.Health / 2d)));
                Console.Write(new string(' ', (int)Math.Round((player.MaxHealth - player.Health) / 2d)));
            }

            Console.ForegroundColor = previousColor;
        }

        private void DrawAdrenalineLine(Point position, Player player, bool isReversive)
        {
            var previousColor = Console.ForegroundColor;

            var percentAdrenaline = 100 / player.MaxAdrenaline * player.Adrenaline;

            var adrLineColor = player.Adrenaline >= player.MaxAdrenaline ?
                ConsoleColor.Magenta : ConsoleColor.DarkMagenta;

            Console.CursorTop = position.Y;
            Console.CursorLeft = position.X;

            if (isReversive)
            {
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write(new string(' ', (int)Math.Round((100 - percentAdrenaline) / 4d)));
                Console.ForegroundColor = adrLineColor;
                Console.Write(new string('/', (int)Math.Round(percentAdrenaline / 4d)));
            }
            else
            {
                Console.ForegroundColor = adrLineColor;
                Console.Write(new string('\\', (int)Math.Round(percentAdrenaline / 4d)));
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write(new string(' ', (int)Math.Round((100 - percentAdrenaline) / 4d)));
            }

            Console.ForegroundColor = previousColor;
        }

        public async Task DrawStaticObjectsAsync()
        {
            await Task.Run(() =>
            {
                lock (Game.locker)
                {
                    var prevForeColor = Console.ForegroundColor;
                    var prevBackColor = Console.BackgroundColor;

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
                        Console.CursorLeft = 86;
                        Console.WriteLine(str);
                    }

                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.DarkRed;
                    Console.SetCursorPosition(0, 37);
                    Console.Write(StaticObjects["floor"]);

                    Console.BackgroundColor = prevBackColor;
                    Console.ForegroundColor = prevForeColor;

                    Console.SetCursorPosition(0, 37);
                    Console.Write(new string('=', 155));
                }
            });
        }
    }
}
