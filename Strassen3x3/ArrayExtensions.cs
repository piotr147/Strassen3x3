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
                    result[i, j] = m1[i, j] + m2[i, j];
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
    }
}
