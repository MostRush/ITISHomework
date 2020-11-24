using System;

namespace Transliteration
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Transliterate("Тестовое предложение: Привет мир!"));
        }

        static string Transliterate(string input)
        {
            string result = string.Empty;

            string rus = "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭФЯ";

            string[] eng = { "A", "B", "V", "G", "D", "E", "E", "ZH",
                "Z", "I", "Y", "K", "L", "M", "N", "O", "P", "R", "S",
                "T", "U", "F", "KH", "TS", "CH", "SH", "SHCH",
                null, "Y", null, "E", "YU", "YA" };

            for (int i = 0; i < input.Length; i++)
                result += char.IsLetter(input[i]) ? eng[rus.IndexOf(char.ToUpper(input[i]))] : input[i].ToString();

            return result.ToLower();
        }
    }
}
