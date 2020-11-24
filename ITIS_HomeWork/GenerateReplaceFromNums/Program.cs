using System;

namespace GenerateReplaceFromNums
{
    class Program
    {
        static void Main(string[] args)
        {
            int n;

            do
            {
                Console.Clear();
                Console.Write("\n\tВведите размер: ");
            } while (!int.TryParse(Console.ReadLine(), out n) || n < 1);

            var array = new int[n];
            var rand = new Random();

            for (int i = 0; i < n; i++)
            {
                array[i] = rand.Next(1, n);
            }

            int count = 0;

            for (int i = 0; i < n; i++)
            {
                count += i != array[i] ? 1 : 0;
            }

            Console.Write("\n\tМассив:");

            foreach (var item in array)
                Console.Write(item + " ");

            Console.WriteLine("\n\tПерестановок: " + count);
        }
    }
}
