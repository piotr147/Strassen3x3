using System;
using System.Text;

namespace Strassen3x3
{
    public static class ArrayExtensions
    {
        public static int[,] Plus(this int[,] m1, int[,] m2)
        {
            int[,] result = new int[m1.GetLength(0), m1.GetLength(0)];

            for (int i = 0; i < result.GetLength(0); ++i)
            {
                for (int j = 0; j < result.GetLength(1); ++j)
                {
                    result[i, j] = m1[i, j] + m2[i, j];
                }
            }

            return result;
        }

        public static int[,] Minus(this int[,] m1, int[,] m2)
        {
            int[,] result = new int[m1.GetLength(0), m1.GetLength(0)];

            for (int i = 0; i < result.GetLength(0); ++i)
            {
                for (int j = 0; j < result.GetLength(1); ++j)
                {
                    result[i, j] = m1[i, j] - m2[i, j];
                }
            }

            return result;
        }

        public static int[,] Minus(this int[,] m1)
        {
            int[,] result = new int[m1.GetLength(0), m1.GetLength(0)];

            for (int i = 0; i < result.GetLength(0); ++i)
            {
                for (int j = 0; j < result.GetLength(1); ++j)
                {
                    result[i, j] = -m1[i, j];
                }
            }

            return result;
        }

        public static void Print(this int[,] m1)
        {
            int width = FindWidth(m1);

            for (int i = 0; i < m1.GetLength(0); ++i)
            {
                for (int j = 0; j < m1.GetLength(1); ++j)
                {
                    PrintOnCertainWidth(m1[i, j], width);
                }

                Console.Write("\n");
            }
        }

        private static int FindWidth(int[,] m1)
        {
            int maxDigitsNumber = 1;

            for (int i = 0; i < m1.GetLength(0); ++i)
            {
                for (int j = 0; j < m1.GetLength(1); ++j)
                {
                    int currentDigitsNumber = FindNumberOfDigits(m1[i, j]);
                    if (currentDigitsNumber > maxDigitsNumber)
                    {
                        maxDigitsNumber = currentDigitsNumber;
                    }
                }
            }

            return maxDigitsNumber + 1;
        }

        private static void PrintOnCertainWidth(int n, int width)
        {
            Console.Write($"{n}");
            Console.Write($"{NSpaces(width - FindNumberOfDigits(n))}");
        }

        private static int FindNumberOfDigits(int n)
        {
            if (n == 0)
                return 1;

            return (int)Math.Ceiling(Math.Log(Math.Abs(n), 10)) + (n < 0 ? 1 : 0);
        }

        private static string NSpaces(int n)
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < n; ++i)
            {
                sb.Append(" ");
            }

            return sb.ToString();
        }
    }
}
