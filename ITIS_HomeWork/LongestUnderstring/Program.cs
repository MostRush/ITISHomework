using System;

namespace LongestUnderstring
{
    class Program
    {
        static void Main(string[] args)
        {
            var strigns = new string[]
            {
                "66677773010200111110010",
                "66659399920234030403000",
                "77773727134000000003232",
                "fffffGGGddDkkdIIljJJjjJ",
            };

            foreach (var str in strigns)
                Console.WriteLine($"{str} - {FindLongestUnderstring(str)} - {GetSequences(str)}");

            Console.ReadKey();
        }

        static string FindLongestUnderstring(string value)
        {
            var prevChar = value[0];

            var prevResult = string.Empty;
            var currResult = string.Empty;

            for (int i = 0; i < value.Length; i++)
            {
                if (value[i] == prevChar)
                {
                    currResult += prevChar = value[i];
                }

                if (value[i] != prevChar)
                {
                    prevResult = prevResult.Length > currResult.Length ? prevResult : currResult;
                    currResult = (prevChar = value[i]).ToString();
                }
            }

            return prevResult;
        }

        static string GetSequences(string value)
        {
            var prevChar = '\0';
            var result = string.Empty;

            for (int i = 0; i < value.Length; i++)
                result += prevChar != value[i] ? (prevChar = value[i]).ToString() : "";

            return result;
        }
    }
}
