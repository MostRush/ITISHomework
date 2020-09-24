using System;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Threading;

namespace QuadraticEquation
{
    class Program
    {
        /* Пример: 18x^2+56x+23=0 */
        static void Main(string[] args)
        {
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write("\n\tВведите квадратное уравнение: ");
                string eqantion = Console.ReadLine();

                var values = ParseQuadratic(eqantion);

                if (values is null)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"\n\tВыражение {eqantion} не является квадратным уравнением!");
                    Thread.Sleep(2000);
                    Console.Clear();
                }
                else
                    Console.WriteLine(Calculate(values[0], values[1], values[2]));
            }
        }

        public static int[] ParseQuadratic(string s)
        {
            s = s.Replace(" ", "");

            int[] result = new int[3];

            var isMatch = Regex.IsMatch(s, @"(\d*)x\^2(?:([+\\-]\d*)x)?([+\-]\d*)?");

            if (isMatch)
            {
                var numbers = Regex.Matches(s, @"(-?\d+)x|(-?\d+)=");

                for (int i = 0; i < 3; i++)
                {
                    var match = Regex.Match(numbers[i].Value, @"(-?\d+)").Value;
                    result[i] = int.Parse(match);
                }
            }
            else
                return null;

            return result;
        }


        static string Calculate(double a, double b, double c)
        {
            var discriminant = Math.Pow(b, 2) - 4 * a * c;

            if (discriminant > 0 || discriminant == 0)
            {
                var x1 = (-b + Math.Sqrt(discriminant)) / (2 * a);
                var x2 = (-b - Math.Sqrt(discriminant)) / (2 * a);
                return $"\tx1 =\t{x1}\n\tx2 =\t{x2}";
            }
            else
                return "\tРешение не имеет корней!";
        }
    }
}
