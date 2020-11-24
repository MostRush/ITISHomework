using System;

namespace CountEngWords
{
    class Program
    {
        static void Main(string[] args)
        {
            var engString = "Falcon 9 is vertical on SpaceX’s West Coast pad ahead\n" +
                "\tof tomorrow’s launch of the Sentinel-6 Michael Freilich mission.\n" +
                "\tWeather forecast is 80% favorable for liftoff";

            Console.WriteLine($"\n\t{engString}");
            Console.WriteLine($"\n\tСлов с заглавной буквы: {CountWordsByCapital(engString)}");
            Console.ReadKey();
        }

        static int CountWordsByCapital(string input)
        {
            var count = 0;

            var words = input.Split(" ,.!?;:\n\t\r".ToCharArray());

            foreach (var word in words)
                if (!string.IsNullOrEmpty(word) && char.IsUpper(word[0]))
                    count++;

            return count;
        }
    }
}
