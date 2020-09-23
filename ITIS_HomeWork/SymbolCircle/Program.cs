using System;
using System.Diagnostics;

namespace SymbolCircle
{
    class Circle
    {
        public int  Radius { get; set; }
        public int Thinkness { get; set; } 
        public string Pattern { get; set; }

        public Circle(int radius)
        {
            Radius = radius;
            Pattern = CreatePattern();
        }

        private string CreatePattern()
        {
            string _pattern = string.Empty;

            for (int y = Radius; y >= -Radius; --y)
            {
                for (double x = -Radius; x < Radius; x += 0.6) // x^2+y^2=r
                {
                    if ((y * y) + (x * x) < Radius * Radius)
                    {
                        _pattern += "+";
                    }
                    else
                    {
                        _pattern += " ";
                    }

                }
                _pattern += "\n";
            }

            return _pattern;
        }

        public override string ToString()
        {
            return Pattern;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(new Circle(25));

            Console.ReadKey();
        }
    }
}
