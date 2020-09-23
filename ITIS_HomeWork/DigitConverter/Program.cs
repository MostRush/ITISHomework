using System;

namespace DigitConverter
{
    static class Converter
    {
        static public string DecimalToBinary(int _decimal)
        {
            string _answer = string.Empty;

            while (_decimal > 0)
            {
                var discharge = _decimal % 2;
                _answer += discharge.ToString();

                _decimal = _decimal % 2;
            }

            return _answer;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Converter.DecimalToBinary(256));
        }
    }
}
