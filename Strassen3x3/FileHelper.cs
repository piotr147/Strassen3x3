using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Strassen3x3
{
    public static class FileHelper
    {
        private const string RESULT_PATH = @".\result.txt";

        public static async Task<int[,]> ReadFile(string path)
        {
            try
            {
                return await TryReadFile(path);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Failed to read input: {e.Message}");
                throw;
            }
        }

        public static async Task<int[,]> TryReadFile(string path)
        {
            List<List<int>> inputs = new List<List<int>>();

            using (StreamReader reader = File.OpenText(path))
            {
                string line = await reader.ReadLineAsync();

                while (line != null && !string.IsNullOrWhiteSpace(line))
                {
                    List<int> row = line.Trim().Split(' ').Select(c => int.Parse(c)).ToList();
                    inputs.Add(row);
                    line = await reader.ReadLineAsync();
                }
            }

            int[,] result = new int[inputs.Count, inputs[0].Count];

            for (int i = 0; i < result.GetLength(0); ++i)
            {
                for (int j = 0; j < result.GetLength(1); ++j)
                {
                    result[i, j] = inputs[i][j];
                }
            }

            return result;
        }

        public static async Task WriteOutput(int[,] m)
        {
            File.Create(RESULT_PATH).Dispose();
            int width = FindWidth(m);

            using (StreamWriter sw = new StreamWriter(RESULT_PATH))
            {
                for (int i = 0; i < m.GetLength(0); ++i)
                {
                    StringBuilder sb = new StringBuilder();
                    for (int j = 0; j < m.GetLength(1); ++j)
                    {
                        PrintOnCertainWidth(sb, m[i, j], width);
                    }
                    await sw.WriteLineAsync(sb.ToString());
                }
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

        private static void PrintOnCertainWidth(StringBuilder sb, int n, int width)
        {
            sb.Append($"{n}");
            sb.Append($"{NSpaces(width - FindNumberOfDigits(n) + 1)}");
        }

        private static int FindNumberOfDigits(int n)
        {
            if (n == 0 || n == 1)
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
