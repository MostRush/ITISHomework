using System;
using System.Text.RegularExpressions;

namespace Regex4TwoSameSym
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Введите слово: ");
            string word = Console.ReadLine();

            Regex regex1 = new Regex(@"^(.)\1");
            Regex regex2 = new Regex(@"(.)\1$");

            Console.WriteLine($"Слово заканчивается на два разных символа: {regex1.IsMatch(word) & !regex2.IsMatch(word)}");
        }
    }
}
