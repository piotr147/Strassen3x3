namespace Strassen3x3
{
    public static class Multiplicator
    {
        public static int[,] Multiplicate(int[,] A, int[,] B)
        {
            if (A.Length == 1)
                return new int[1, 1] { { A[0, 0] * B[0, 0] } };

            int[,][,] a = new int[,][,]
            {
                {null, null, null, null },
                { null, GenerateMatrix(A, 1, 2), GenerateMatrix(A, 1, 2), GenerateMatrix(A, 1, 3) },
                { null, GenerateMatrix(A, 2, 1), GenerateMatrix(A, 2, 2), GenerateMatrix(A, 2, 3) },
                { null, GenerateMatrix(A, 3, 1), GenerateMatrix(A, 3, 2), GenerateMatrix(A, 3, 3) }
            };

            int[,][,] b = new int[,][,]
            {
                {null, null, null, null },
                { null, GenerateMatrix(B, 1, 1), GenerateMatrix(B, 1, 2), GenerateMatrix(B, 1, 3) },
                { null, GenerateMatrix(B, 2, 1), GenerateMatrix(B, 2, 2), GenerateMatrix(B, 2, 3) },
                { null, GenerateMatrix(B, 3, 1), GenerateMatrix(B, 3, 2), GenerateMatrix(B, 3, 3) }
            };

            int[][,] m = GenerateMatrixM(a, b);

            int[,][,] c = new int[4, 4][,];

            c[1, 1] = m[6].Plus(m[14]).Plus(m[19]);
            // ...
            c[3, 3] = m[6].Plus(m[7]).Plus(m[8]).Plus(m[9]).Plus(m[23]);

            return MergeToOneMatrix(c);
        }

        private static int[,] GenerateMatrix(int[,] m, int v1, int v2)
        {
            int resultLength = m.GetLength(0) / 3;
            int[,] result = new int[resultLength, resultLength];
            int startIndex1 = (v1 - 1) * resultLength;
            int startIndex2 = (v2 - 1) * resultLength;

            for (int i = 0; i < resultLength; ++i)
            {
                for (int j = 0; j < resultLength; ++j)
                {
                    result[i, j] = m[startIndex1 + i, startIndex2 + j];
                }
            }

            return result;
        }

        private static int[][,] GenerateMatrixM(int[,][,] a, int[,][,] b)
        {
            int[][,] m = new int[24][,];

            m[0] = null;
            m[1] = Multiplicate(
                a[1, 1].Plus(a[1, 2]).Plus(a[1, 3]).Minus(a[2, 1]).Minus(a[2, 2]).Minus(a[3, 2]).Minus(a[3, 3]),
                b[3, 3]);
            // ...
            m[23] = Multiplicate(a[3, 3], b[3, 3]);

            return m;
        }

        private static int[,] MergeToOneMatrix(int[,][,] c)
        {
            int cLength = c[1, 1].GetLength(0);
            int[,] result = new int[cLength * 3, cLength * 3];

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
