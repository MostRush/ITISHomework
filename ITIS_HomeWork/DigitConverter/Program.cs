using System;

namespace DigitConverter
{
    static class Converter
    {
        public static string AnyToAny(string s, int from, int to)
        {
            const string symbols = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            string result = string.Empty;
            int discharge = 0, temporary = 0, valuePower = 1;

            for (int i = s.Length - 1; i >= 0; i--)
            {
                discharge = Convert.ToInt32(s[i]);
                if ((discharge >= 48) && (discharge <= 57))
                    discharge = discharge - 48;
                else
                {
                    if ((discharge >= 65) && (discharge <= 90))
                        discharge = discharge - 65;
                    else
                    {
                        if ((discharge >= 97) && (discharge <= 122))
                            discharge = discharge - 97;
                    }
                }

                temporary = temporary + discharge * valuePower;
                valuePower = valuePower * from;
            }

            result = string.Empty;

            while (temporary != 0)
            {
                discharge = temporary % to;
                result = symbols[discharge] + result;
                temporary = temporary / to;
            }

            return result;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Converter.AnyToAny("256", 10, 2));
            Console.WriteLine(Converter.AnyToAny("256", 10, 8));
            Console.WriteLine(Converter.AnyToAny("256", 10, 16));
            Console.WriteLine(Converter.AnyToAny("F3A2", 16, 10));
            Console.WriteLine(Converter.AnyToAny("35478", 8, 10));
            Console.WriteLine(Converter.AnyToAny("1001010", 2, 10));

            Console.WriteLine(Converter.AnyToAny("12345", 5, 2));
            Console.WriteLine(Converter.AnyToAny("FSAKFKAJWL", 30, 2));
        }
    }
}
