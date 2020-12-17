using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace FightingGame
{
    struct Point
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override string ToString()
        {
            return $"[x: {X} y: {Y}]";
        }
    }

    class Animation
    {
        CancellationTokenSource cancelTokenSource;
        CancellationToken cancellationToken;

        private string[] frames;

        public string Name { get; set; }
        public Point Position { get; set; }
        public string AnimPath { get; set; }
        public bool IsLooped { get; set; }
        public int FrameDelay { get; set; } = 40;
        public ConsoleColor Color { get; set; } = ConsoleColor.White;

        public Animation(string name, Point pos, string path)
        {
            Name = name;
            Position = pos;
            AnimPath = path;

            cancelTokenSource = new CancellationTokenSource();
            cancellationToken = cancelTokenSource.Token;

            frames = GetFramesFromResources();
        }

        private string[] GetFramesFromResources()
        {
            var files = Directory.GetFiles(AnimPath);

            string[] frames = new string[files.Length];

            for (int i = 0; i < frames.Length; i++)
                frames[i] = File.ReadAllText(files[i]);

            return frames;
        }

        public async Task Play()
        {
            this.Stop();

            cancelTokenSource = new CancellationTokenSource();
            cancellationToken = cancelTokenSource.Token;

            await Task.Run(() =>
            {
                do
                {
                    foreach (var frame in frames)
                    {
                        lock (Game.locker)
                        {
                            if (cancellationToken.IsCancellationRequested) return;

                            Console.CursorVisible = false;
                            Console.ForegroundColor = Color;
                            Console.CursorTop = Position.Y;

                            var strings = frame.Split('\n');

                            foreach (var str in strings)
                            {
                                Console.CursorLeft = Position.X;
                                Console.WriteLine(str);
                            }
                        }

                        Thread.Sleep(FrameDelay);
                    }
                } 
                while (IsLooped);
            });
        }

        public void Stop()
        {
            cancelTokenSource.Cancel();
        }
    }
}
