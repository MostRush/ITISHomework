using System;

namespace NaturalQuantity
{
    class Program
    {
        static void Main(string[] args)
        {
            for (int i = 1; i <= 27; i++)
                Console.WriteLine($"{i} - {FindQuantityByExpr(i)}");

            Console.ReadKey();
        }

        static int FindQuantityByExpr(int n)
        {
            if (1 > n || n > 27) throw new Exception("Value is not correct!");

            int quantity = 0;

            for (int i = 100; i <= 999; i++)
            {
                var value = i.ToString();
                char[] discharges = { value[0], value[1], value[2] };

                var summ = 0;

                foreach (var d in discharges)
                    summ += d - '0';

                quantity = summ == n ? quantity + 1 : quantity;
            }

            return quantity;
        }
    }
}
