using System;
using System.Text.RegularExpressions;

namespace Regex3TwoDifSym
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Введите слово: ");
            string word = Console.ReadLine();

            Regex regex = new Regex(@"(.)\1$");

            Console.WriteLine($"Слово заканчивается на два разных символа: {regex.IsMatch(word)}");
        }
    }
}
