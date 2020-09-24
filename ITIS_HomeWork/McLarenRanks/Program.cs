using System;
using System.Data.Common;

namespace McLarenRanks
{
    class Program
    {
        static void Main(string[] args)
        {
            double accuracy = Math.Pow(10, -9);

            Console.WriteLine(Function1(7, accuracy));
            Console.WriteLine(Function2(7, accuracy));
            Console.WriteLine(Function3(7, accuracy));
            Console.WriteLine(Function4(7, accuracy));
            Console.WriteLine(Function5(7, accuracy));
        }

        static public int Factorial(int numb)
        {
            int res = 1;
            for (int i = numb; i > 1; i--)
                res *= i;
            return res;
        }

        static double Function1(double x, double accuracy)
        {
            double result = 1;
            int n = 0;

            while (true)
            {
                if (result < accuracy) break;

                result += Math.Pow(x, n) / Factorial(n);

                n++;
            }

            return result;
        }

        static double Function2(double x, double accuracy)
        {
            double result = 1;
            int n = 0;

            while (true)
            {
                if (result < accuracy) break;

                result += (Math.Pow(-1, n) * Math.Pow(x, 2 * n)) / Factorial(2 * n);

                n++;
            }

            return result;
        }

        static double Function3(double x, double accuracy)
        {
            double result = 1;
            int n = 0;

            while (true)
            {
                if (result < accuracy) break;

                result += (Math.Pow(-1, n) * Math.Pow(x, 2 * n + 1)) / Factorial(2 * n + 1);

                n++;
            }

            return result;
        }

        static double Function4(double x, double accuracy)
        {
            double result = 1;
            int n = 0;

            while (true)
            {
                if (result < accuracy) break;

                result += Math.Pow(x, 2 * n) / Factorial(2 * n);

                n++;
            }

            return result;
        }

        static double Function5(double x, double accuracy)
        {
            double result = 1;
            int n = 0;

            while (true)
            {
                if (result < accuracy) break;

                result += Math.Pow(x, 2 * n + 1) / Factorial(2 * n + 1);

                n++;
            }

            return result;
        }
    }
}
