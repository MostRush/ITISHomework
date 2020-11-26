using System;
using System.Text.RegularExpressions;

namespace Regex1TwoSym
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Введите слово: ");
            string word = Console.ReadLine();

            Regex regex = new Regex(@"^(.)\1");

            Console.WriteLine($"Слово начинается на два одинаковых символа: {regex.IsMatch(word)}");
        }
    }
}
