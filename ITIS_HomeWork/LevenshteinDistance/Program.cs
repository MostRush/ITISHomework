using System;
using System.Text;

namespace LevenshteinDistance
{
    class Program
    {
        public static void Main()
        {
            Console.SetWindowSize(100, 50);
            Console.CursorVisible = false; 
            Console.WriteLine(LevenshteinDistance("Распердоввоавап43г58934", "ДилиБомыКомы3к цкщгц38гт83к "));
        }

        public static int LevenshteinDistance(string string1, string string2)
        {
            /* Строки не должны быть пустыми */
            string throwMessage = "Argument string can't be null!";
            if (string1 is null || string2 is null) throw new Exception($"{throwMessage}");

            /* Создаём массив размерностью на 1 больше длин строк */
            int difference;
            int[,] m = new int[string1.Length + 1, string2.Length + 1];

            /* Заполняем первые строки  */
            for (int i = 0; i < string1.Length; i++)
            {
                Console.SetCursorPosition(0, 0);
                PrintArray(m);
                m[i, 0] = i;
                Console.ReadKey();
            }
            for (int j = 0; j < string2.Length; j++)
            {
                Console.SetCursorPosition(0, 0);
                PrintArray(m);
                m[0, j] = j;
                Console.ReadKey();
            }

            Console.SetCursorPosition(0, 0);
            PrintArray(m);

            Console.ReadKey();

            for (int i = 1; i <= string1.Length; i++)
            {
                for (int j = 1; j <= string2.Length; j++)
                {
                    difference = string1[i - 1] == string2[j - 1] ? 0 : 1;

                    m[i, j] = Math.Min(Math.Min(m[i - 1, j] + 1, m[i, j - 1] + 1), m[i - 1, j - 1] + difference);

                    Console.SetCursorPosition(0, 0);
                    PrintArray(m);

                    Console.ReadKey();
                }
            }

            return m[string1.Length, string2.Length];
        }

        public static void PrintArray(int[,] array)
        {
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    var space = array[i, j] < 10 ? "  " : " ";
                    Console.Write($"{array[i, j]}{space}");
                }

                Console.WriteLine("\n");
            }

            Console.WriteLine();
        }
    }
}
