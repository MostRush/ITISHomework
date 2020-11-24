using System;

namespace Capitalization
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(ToUpper("Задача: Дано предложение: Задача сделать все слова предложения с большой буквы."));
        }

        static string ToUpper(string input)
        {
            string result = string.Empty;

            for (int i = 0; i < input.Length; i++)
            {
                result += char.ToUpper(input[i]);
            }

            return result;
        }
    }
}
