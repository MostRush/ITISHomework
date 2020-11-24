using System;
using System.Collections.Generic;

namespace GenerateArrayOfLetters
{
    public class RandomNotRepeat : Random
    {
        int _min, _max;
        Stack<int> list;

        public RandomNotRepeat(int minValue, int maxValue)
        {
            _max = maxValue;
            _min = minValue;
            genList();
        }

        protected void genList()
        {
            System.Random rand = new System.Random();
            List<int> temp = new List<int>();
            for (int i = _min; i < _max; i++)
            {
                temp.Add(i);
            }
            list = new Stack<int>();
            while (temp.Count > 0)
            {
                int addInt = temp[rand.Next(0, temp.Count)];
                list.Push(addInt);
                temp.Remove(addInt);
            }
        }
        
        public override int Next()
        {
            if (list.Count > 0)
            {
                return list.Pop();
            }
            else
            {
                genList();
            }
            return list.Pop();
        }

    }

    class Program
    {
        static void Main(string[] args)
        {
            var alphabet = "abcdefghijklmnopqrstuvwxyz".ToCharArray();

            Console.WriteLine(alphabet);
            Console.WriteLine(RandomizeArray(alphabet));
        }

        static char[] RandomizeArray(char[] array)
        {
            var result = new char[array.Length];
            var random = new RandomNotRepeat(0, array.Length);

            for (int i = 0; i < array.Length; i++)
            {
                result[i] = array[random.Next()];
            }

            return result;
        }
    }
}
