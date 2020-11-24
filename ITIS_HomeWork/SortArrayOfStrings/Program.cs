using System;
namespace SortArrayOfStrings
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] strArray =
            {
                "Хорошая работа, Олег",
                "Всем привет, с вами Апельсинчик",
                "Я снимаю в, так сказать, половинчатов режиме",
                "Арманын дусак шактаган кешкелдзы",
                "Заменить все буквы в строке цифрами на которой",
                "Вводится строка, представляющая собой",
            };

            Console.WriteLine("\n\tНачальный массив строк:\n");

            foreach (var item in strArray)
                Console.WriteLine($"\t{item}");

            Array.Sort(strArray);

            Console.WriteLine("\n\tОтсоритрованый массив строк:\n");
            
            foreach (var item in strArray)
                Console.WriteLine($"\t{item}");
        }
    }
}
