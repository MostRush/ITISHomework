using System;

namespace ArrayToTriangle
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = 0;

            do
            {
                Console.Clear();
                Console.Write("\n\tВведите размер матрицы: ");
            } while (!int.TryParse(Console.ReadLine(), out n) || n < 1);

            var matrix = GenerateMatrix(n);

            Console.WriteLine("\n\tСгенерированная матрица:\n");

            PrintMatrix(matrix);

            var triMatrix = ToTriangularMatrix(matrix);

            Console.WriteLine("\n\n\tТрехугольная матрица:\n");

            PrintMatrix(triMatrix);

            Console.ReadKey();
        }

        static int[,] GenerateMatrix(int n)
        {
            var matrx = new int[n, n];
            var random = new Random();

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    matrx[i, j] = random.Next(0, 99);
                }
            }

            return matrx;
        }

        static int[,] ToTriangularMatrix(int[,] matrix)
        {
            var matrixSize = matrix.GetLength(0);

            var result = new int[matrixSize, matrixSize];

            for (int i = 0; i < matrixSize; i++)
                for (int j = 0; j < matrixSize; j++)
                    result[i, j] = (i <= j) ? matrix[i, j] : 0;

            return result;
        }

        public static void PrintMatrix(int[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                Console.Write("\t");

                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    var space = matrix[i, j] < 10 ? "  " : " ";
                    Console.Write($"{matrix[i, j]}{space}");
                }

                Console.WriteLine("\n");
            }

            Console.WriteLine();
        }
    }
}
