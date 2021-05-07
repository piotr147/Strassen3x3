using System;

namespace Strassen3x3
{
    public static class Multiplicator
    {
        public static int[,] Multiplicate(int[,] A, int[,] B)
        {
            int wantedSize = FindWantedSize(A, B);
            int[,] correctSizeA = AppendZerosToHaveDesiredSize(A, wantedSize);
            int[,] correctSizeB = AppendZerosToHaveDesiredSize(B, wantedSize);

            int[,] result = MultiplicateWithCorrectSizes(correctSizeA, correctSizeB);

            return PreparePreviousSize(result, A.GetLength(0), B.GetLength(1));
        }

        private static int FindWantedSize(int[,] a, int[,] b)
        {
            int size = a.GetLength(0);
            size = size < a.GetLength(1) ? a.GetLength(1) : size;
            size = size < b.GetLength(0) ? b.GetLength(0) : size;
            size = size < b.GetLength(1) ? b.GetLength(1) : size;

            int wantedSize = 1;

            while (wantedSize < size)
            {
                wantedSize *= 3;
            }

            return wantedSize;
        }

        private static int[,] PreparePreviousSize(int[,] m, int size1, int size2)
        {
            if (m.GetLength(0) == size1 && m.GetLength(1) == size2)
                return m;

            int[,] result = new int[size1, size2];

            for (int i = 0; i < size1; ++i)
            {
                for (int j = 0; j < size2; ++j)
                {
                    result[i, j] = m[i, j];
                }
            }

            return result;
        }

        private static int[,] AppendZerosToHaveDesiredSize(int[,] m, int wantedSize)
        {
            int[,] result = new int[wantedSize, wantedSize];

            for (int i = 0; i < m.GetLength(0); ++i)
            {
                for (int j = 0; j < m.GetLength(1); ++j)
                {
                    result[i, j] = m[i, j];
                }
            }

            return result;
        }

        private static int[,] MultiplicateWithCorrectSizes(int[,] A, int[,] B)
        {
            if (A.Length == 1)
                return new int[1, 1] { { A[0, 0] * B[0, 0] } };

            int[,][,] a = new int[,][,]
            {
                {null, null, null, null },
                { null, GenerateMatrix(A, 1, 1), GenerateMatrix(A, 1, 2), GenerateMatrix(A, 1, 3) },
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
            c[1, 2] = m[1].Plus(m[4]).Plus(m[5]).Plus(m[6]).Plus(m[12]).Plus(m[14]).Plus(m[15]);
            c[1, 3] = m[6].Plus(m[7]).Plus(m[9]).Plus(m[10]).Plus(m[14]).Plus(m[16]).Plus(m[18]);
            c[2, 1] = m[2].Plus(m[3]).Plus(m[4]).Plus(m[6]).Plus(m[14]).Plus(m[16]).Plus(m[17]);
            c[2, 2] = m[2].Plus(m[4]).Plus(m[5]).Plus(m[6]).Plus(m[20]);
            c[2, 3] = m[14].Plus(m[16]).Plus(m[17]).Plus(m[18]).Plus(m[21]);
            c[3, 1] = m[6].Plus(m[7]).Plus(m[8]).Plus(m[11]).Plus(m[12]).Plus(m[13]).Plus(m[14]);
            c[3, 2] = m[12].Plus(m[13]).Plus(m[14]).Plus(m[15]).Plus(m[22]);
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
                b[2, 2]);
            m[2] = Multiplicate(
                a[1, 1].Minus(a[2, 1]),
                b[1, 2].Minus().Plus(b[2, 2]));
            m[3] = Multiplicate(
                a[2, 2],
                b[1, 1].Minus().Plus(b[1, 2]).Plus(b[2, 1]).Minus(b[2, 2]).Minus(b[2, 3]).Minus(b[3, 1]).Plus(b[3, 3]));
            m[4] = Multiplicate(
                a[1, 1].Minus().Plus(a[2, 1]).Plus(a[2, 2]),
                b[1, 1].Minus(b[1, 2]).Plus(b[2, 2]));
            m[5] = Multiplicate(
                a[2, 1].Plus(a[2, 2]),
                b[1, 1].Minus().Plus(b[1, 2]));
            m[6] = Multiplicate(
                a[1, 1],
                b[1, 1]);
            m[7] = Multiplicate(
                a[1, 1].Minus().Plus(a[3, 1]).Plus(a[3, 2]),
                b[1, 1].Minus(b[1, 3]).Plus(b[2, 3]));
            m[8] = Multiplicate(
                a[1, 1].Minus().Plus(a[3, 1]),
                b[1, 3].Minus(b[2, 3]));
            m[9] = Multiplicate(
                a[3, 1].Plus(a[3, 2]),
                b[1, 1].Minus().Plus(b[1, 3]));
            m[10] = Multiplicate(
                a[1, 1].Plus(a[1, 2]).Plus(a[1, 3]).Minus(a[2, 2]).Minus(a[2, 3]).Minus(a[3, 1]).Minus(a[3, 2]),
                b[2, 3]);
            m[11] = Multiplicate(
                a[3, 2],
                b[1, 1].Minus().Plus(b[1, 3]).Plus(b[2, 1]).Minus(b[2, 2]).Minus(b[2, 3]).Minus(b[3, 1]).Plus(b[3, 2]));
            m[12] = Multiplicate(
                a[1, 3].Minus().Plus(a[3, 2]).Plus(a[3, 3]),
                b[2, 2].Plus(b[3, 1]).Minus(b[3, 2]));
            m[13] = Multiplicate(
                a[1, 3].Minus(a[3, 3]),
                b[2, 2].Minus(b[3, 2]));
            m[14] = Multiplicate(
                a[1, 3],
                b[3, 1]);
            m[15] = Multiplicate(
                a[3, 2].Plus(a[3, 3]),
                b[3, 1].Minus().Plus(b[3, 2]));
            m[16] = Multiplicate(
                a[1, 3].Minus().Plus(a[2, 2]).Plus(a[2, 3]),
                b[2, 3].Plus(b[3, 1]).Minus(b[3, 3]));
            m[17] = Multiplicate(
                a[1, 3].Minus(a[2, 3]),
                b[2, 3].Minus(b[3, 3]));
            m[18] = Multiplicate(
                a[2, 2].Plus(a[2, 3]),
                b[3, 1].Minus().Plus(b[3, 3]));
            m[19] = Multiplicate(
                a[1, 2],
                b[2, 1]);
            m[20] = Multiplicate(
                a[2, 3],
                b[3, 2]);
            m[21] = Multiplicate(
                a[2, 1],
                b[1, 3]);
            m[22] = Multiplicate(
                a[3, 1],
                b[1, 2]);
            m[23] = Multiplicate(
                a[3, 3],
                b[3, 3]);

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
    }
}
