using System;

namespace PointInCircle
{
    class Program
    {
        static void Main(string[] args)
        {
            double Xc = 0;
            double Yc = 0;

            Console.Write("\n\tВведите радиус: ");
            double r = double.Parse(Console.ReadLine());

            Console.Write("\tВведите Х: ");
            double x = double.Parse(Console.ReadLine());

            Console.Write("\tВведите Y: ");
            double y = double.Parse(Console.ReadLine());

            if (((x - Xc) * (x - Xc) + (y - Yc) * (y - Yc)) < r * r)
                Console.WriteLine("Точка принадлежит окружности");
            else if (((x - Xc) * (x - Xc) + (y - Yc) * (y - Yc)) == r * r)
                Console.WriteLine("Точка лежит на окружности");
            else
                Console.WriteLine("Точка не принадлежит окружности");
        }
    }
}
