using System;
using System.Collections.Generic;

namespace ArithmeticProgression
{
    class Program
    {
        static void Main(string[] args)
        {
            int step, init;

            begin:

            try
            {
                Console.Clear();
                Console.Write("\n\tВведите шаг: ");
                step = int.Parse(Console.ReadLine());

                Console.Write("\n\tВведите число: ");
                init = int.Parse(Console.ReadLine());
            }
            catch { goto begin; }

            var progression = GenerateProgression(step, init, 16);

            Console.WriteLine($"\n\n\tПрогрессия: {ListToString(progression)}");
        }

        static double[] GenerateProgression(int step, int init, int size)
        {
            double[] array = new double[size];

            for (int i = 0; i < 16; i++)
            {
                array[0] = init;
                array[i] = array[0] + step * (i - 1); ;
            }

            return array;
        }

        static string ListToString(double[] array)
        {
            var result = string.Empty;

            for (int i = 0; i < array.Length; i++)
                result += Math.Round(array[i], 2).ToString() + " ";

            return result;
        }
    }
}
