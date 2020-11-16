using System;

namespace SquareRoot
{
    class Program
    {
        static void Main(string[] args)
        {
            var testValues = new int[] { 5, 7, 13, 32, 128 };

            foreach (var v in testValues)
                Console.WriteLine(Sqrt(v));

            Console.WriteLine();

            foreach (var v in testValues)
                Console.WriteLine(Math.Sqrt(v));
        }

        static double Sqrt(double x, double y = 1)
        {
            double e = Pow(10, -6);

            if (Abs(y * y - x) < e)
                return y;
            else
                return Sqrt(x, y = (y + x / y) / 2);
        }

        static double Abs(double x) => (x < 0) ? -x : x;

        static double Pow(double x, double y)
        {
            double result = x;

            for (int i = 1; i < (y > 0 ? y : checked(-y)); i++)
                result *= x;

            return (y < 0 ? 1 / result : result) * (x < 0 ? -1 : 1);
        }
    }
}
