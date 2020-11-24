using System;

namespace LetterToIndex
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(TransLetterToIndex("Всем привет! Тестовая строка: тест, строка."));
        }

        static string TransLetterToIndex(string input)
        {
            string result = string.Empty;

            for (int i = 0; i < input.Length; i++)
            {
                result += char.IsLetter(input[i]) ? $"[{i.ToString()}]" : input[i].ToString();
            }

            return result;
        }
    }
}
