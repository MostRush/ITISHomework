using System;
using System.Collections.Generic;

namespace LabyrinthGenerator
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
    }

    class Labyrinth
    {
        public int Height { get; set; }
        public int Width { get; set; }

        public bool[,] Matrix { get; private set; }

        private List<Point> stack;

        public Labyrinth(int height, int width)
        {
            Height = height;
            Width = width;

            stack = new List<Point>();
        }

        public bool[,] Generate()
        {
            Matrix = new bool[Width * 2 + 3, Height * 2 + 3];
            var current = new Point(0, 0);

            for (int y = 0; y < Width * 2 + 3; y++)
            {
                for (int x = 0; x < Height * 2 + 3; x++)
                    Matrix[0, x] = Matrix[Width * 2 + 2, x] = true;

                Matrix[y, 0] = Matrix[y, Height * 2 + 2] = true;
            }

            AddPoint(current, 0, 0);

            while (!IsAllUsed())
            {
                MoveCurrent(current);

                int c = stack.Count - 1;

                while (GetNeighbour(current)[0] && GetNeighbour(current)[1] 
                    && GetNeighbour(current)[2] && GetNeighbour(current)[3])
                {
                    current = stack[c];

                    if (c > 0) c--;
                    else break;
                }
            }

            return Matrix;
        }

        public void Draw()
        {
            for (int y = 1; y < Width * 2 + 2; y++)
            {
                for (int x = 1; x < Height * 2 + 2; x++)
                {
                    Console.BackgroundColor = Matrix[y, x] ? 
                        ConsoleColor.White : ConsoleColor.Black;
                    Console.Write("  ");
                }

                if (y != Width * 2 + 1)
                    Console.WriteLine();
            }
        }

        private void AddPoint(Point pos, int a, int b)
        {
            Matrix[(pos.Y + 1) * 2 + b, (pos.X + 1) * 2 + a] = true;
        }

        private bool GetPoint(Point pos, int a, int b)
        {
            return Matrix[(pos.Y + 1 + b) * 2, (pos.X + 1 + a) * 2];
        }

        private bool[] GetNeighbour(Point pos)
        {
            bool[] neighbours = new bool[4];

            neighbours[0] = GetPoint(pos, 0, -1);
            neighbours[1] = GetPoint(pos, 1, 0);
            neighbours[2] = GetPoint(pos, 0, 1);
            neighbours[3] = GetPoint(pos, -1, 0);

            return neighbours;
        }

        private bool IsAllUsed()
        {
            int count = 0;

            for (int y = 1; y < Width * 2 + 2; y++)
            {
                for (int x = 1; x < Height * 2 + 2; x++)
                    if (x % 2 == 0 && y % 2 == 0 && Matrix[y, x]) count++;
            }

            return count == Width * Height;
        }

        private void MoveCurrent(Point current)
        {
            while (!(GetNeighbour(current)[0] && GetNeighbour(current)[1] 
                && GetNeighbour(current)[2] && GetNeighbour(current)[3]))
            {
                var r = new Random().Next(4);

                while (GetNeighbour(current)[r])
                    r = new Random().Next(4);

                switch (r)
                {
                    case 0: 
                        AddPoint(current, 0, -1); current.Y--; 
                        break;
                    case 1: 
                        AddPoint(current, 1, 0); current.X++; 
                        break;
                    case 2: 
                        AddPoint(current, 0, 1); current.Y++; 
                        break;
                    case 3: 
                        AddPoint(current, -1, 0); current.X--; 
                        break;
                }

                AddPoint(current, 0, 0);

                stack.Add(new Point(current.X, current.Y));
            }
        }
    }
}
