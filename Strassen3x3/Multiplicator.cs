using System;

namespace Strassen3x3
{
    public static class Multiplicator
    {
        private const int STRASSEN_SIZE = 3;

        public static int[,] Multiplicate(int[,] A, int[,] B)
        {
            if (A.Length == 1)
                return CalculateOneElementResult(A, B);

            int[,][,] a = GenerateBlockMatricesFromInput(A);
            int[,][,] b = GenerateBlockMatricesFromInput(B);

            int[][,] m = GenerateMatricesM(a, b);

            int[,][,] c = GenerateBlockMatricesForC(m);

            return MergeToOneMatrix(c);
        }

        private static int[,] CalculateOneElementResult(int[,] a, int[,] b) =>
            new int[1, 1] { { a[0, 0] * b[0, 0] } };

        private static int[,] GenerateMatrix(int[,] m, int ind1, int ind2)
        {
            int resultLength = m.GetLength(0) / STRASSEN_SIZE;
            int[,] result = new int[resultLength, resultLength];
            int startIndex1 = (ind1 - 1) * resultLength;
            int startIndex2 = (ind2 - 1) * resultLength;

            for (int i = 0; i < resultLength; ++i)
            {
                for (int j = 0; j < resultLength; ++j)
                {
                    result[i, j] = m[startIndex1 + i, startIndex2 + j];
                }
            }

            return result;
        }

        private static int[,][,] GenerateBlockMatricesFromInput(int[,] m) =>
            new int[,][,]
            {
                {null, null, null, null },
                { null, GenerateMatrix(m, 1, 2), GenerateMatrix(m, 1, 2), GenerateMatrix(m, 1, 3) },
                { null, GenerateMatrix(m, 2, 1), GenerateMatrix(m, 2, 2), GenerateMatrix(m, 2, 3) },
                { null, GenerateMatrix(m, 3, 1), GenerateMatrix(m, 3, 2), GenerateMatrix(m, 3, 3) }
            };

        private static int[][,] GenerateMatricesM(int[,][,] a, int[,][,] b)
        {
            int[][,] m = new int[24][,];

            m[0] = null;
            m[1] = Multiplicate(
                a[1, 1].Plus(a[1, 2]).Plus(a[1, 3]).Minus(a[2, 1]).Minus(a[2, 2]).Minus(a[3, 2]).Minus(a[3, 3]),
                b[3, 3]);
            // ...
            m[4] = Multiplicate(
                a[1, 1].Minus().Plus(a[2, 1]).Plus(a[2, 2]),
                b[1, 1].Minus(b[1, 2]).Plus(b[2, 2]));
            // ...
            m[23] = Multiplicate(a[3, 3], b[3, 3]);

            return m;
        }

        private static int[,][,] GenerateBlockMatricesForC(int[][,] m)
        {
            int[,][,] c = new int[STRASSEN_SIZE + 1, STRASSEN_SIZE + 1][,];

            c[1, 1] = m[6].Plus(m[14]).Plus(m[19]);
            // ...
            c[3, 3] = m[6].Plus(m[7]).Plus(m[8]).Plus(m[9]).Plus(m[23]);

            return c;
        }

        private static int[,] MergeToOneMatrix(int[,][,] c)
        {
            int cLength = c[1, 1].GetLength(0);
            int[,] result = new int[cLength * STRASSEN_SIZE, cLength * STRASSEN_SIZE];

            for (int i = 1; i < c.GetLength(0); ++i)
            {
                for (int j = 1; j < c.GetLength(1); ++j)
                {
                    for (int ii = 0; ii < cLength; ++ii)
                    {
                        for (int jj = 0; jj < cLength; ++jj)
                        {
                            result[(i - 1) * cLength + ii, (j - 1) * cLength + jj] = c[i, j][ii, jj];
                        }
                    }
                }
            }

            return result;
        }

        //private static void CheckArguments(int[,] a, int[,] b)
        //{
        //    if (a.GetLength(0) != a.GetLength(1)
        //        || b.GetLength(0) != b.GetLength(1))
        //        throw new ArgumentException("Matrices must be square");

        //    if(a.GetLength(0) != b.GetLength(0))
        //        throw new ArgumentException("Matrices must have equal size");

        //    if (!IsPowerOfThree(a.GetLength(0)))
        //        throw new ArgumentException("Matrices length must be power of three");
        //}

        //private static bool IsPowerOfThree(int n)
        //{
        //    int pow = 1;
        //    while(pow <= n)
        //    {
        //        if (pow == n)
        //            return true;
        //    }

        //    return false;
        //}
    }
}
