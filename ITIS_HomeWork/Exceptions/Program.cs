using System;

namespace Exceptions
{
    class MyClass
    {
        public int a;

        public MyClass(int a)
        {
            this.a = a;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            
            try
            {
                SimpleMethod(5, 6, new MyClass(5), true);
            }
            catch (IndexOutOfRangeException ex)
            {
                Console.WriteLine(ex.Message);
            }
            
            try
            {
                SimpleMethod(5, 0, new MyClass(5), true);
            }
            catch (DivideByZeroException ex)
            {
                Console.WriteLine(ex.Message);
            }

            try
            {
                SimpleMethod(5, 4, null, true);
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine(ex.Message);
            }

            try
            {
                SimpleMethod(5, 4, new MyClass(5), false);
            }
            catch (InvalidCastException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static double SimpleMethod(int x, int y, MyClass myClass, bool test)
        {
            var mtx = new int[x];

            var a = mtx[y];

            if (myClass is null)
            {
                throw new ArgumentNullException("MyClass can't be null!");
            }

            bool flag = true;

            var ch = !test ? Convert.ToChar(flag) : 'a';

            var b = myClass.a;

            return x / y;
        }
    }
}
