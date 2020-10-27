using System;

namespace BarAndHole
{
    class Program
    {
        static void Main(string[] args)
        {
            int x = 0, y = 0, z = 0;
            int a = 0, b = 0;

            var isValidated = false;

            do
            {
                Console.SetCursorPosition(0, 0);
                Console.Write(new string(' ', 60));
                Console.SetCursorPosition(0, 0);

                Console.Write("Введите стороны бруса [x y z]:\t  ");
                var sides = Console.ReadLine().Split(' ');

                isValidated = false;

                if (sides.Length == 3)
                {
                    isValidated = int.TryParse(sides[0], out x) 
                        && int.TryParse(sides[1], out y) 
                        && int.TryParse(sides[2], out z);
                }

            } while (!isValidated);

            do
            {
                Console.SetCursorPosition(0, 1);
                Console.Write(new string(' ', 60));
                Console.SetCursorPosition(0, 1);

                Console.Write("Введите стороны отверстия [a b]:  ");
                var sides = Console.ReadLine().Split(' ');

                isValidated = false;

                if (sides.Length == 2)
                {
                    isValidated = int.TryParse(sides[0], out a)
                        && int.TryParse(sides[1], out b);
                }

            } while (!isValidated);

            bool isPassed = (x <= a && y <= b) || (x <= b && y <= a) 
                || (y <= a && z <= b) || (y <= b && z <= a) 
                || (z <= a && x <= b) || (z <= b && x <= a);

            Console.WriteLine($"\n\tБрус проходит через отверстие: {isPassed}");
            Console.ReadLine();
        }
    }
}
