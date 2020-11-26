using System;
using System.Text.RegularExpressions;

namespace Regex2TwoZero
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Введите слово: ");
            string word = Console.ReadLine();

            Regex regex = new Regex(@"00$");

            Console.WriteLine($"Слово заканчивается на два нуля: {regex.IsMatch(word)}");
        }
    }
}
