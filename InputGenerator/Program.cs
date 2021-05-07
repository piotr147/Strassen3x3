using CommandLine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace InputGenerator
{
    class Program
    {
        private const int MaxValue = 100;
        private const int MinValue = 0;

        private static CmdOptions _options;
        private static Random _rand;

        static async Task Main(string[] args)
        {
            ReadArgs(args);
            _rand = new Random();

            if (_options.A2 != _options.B1)
            {
                Console.WriteLine("Given dimensions are not correct");
                return;
            }

            int[,] A = CreateRandomMatrix(_options.A1, _options.A2);
            int[,] B = CreateRandomMatrix(_options.B1, _options.B2);

            await WriteToFile(A, "Matrix1.txt");
            await WriteToFile(B, "Matrix2.txt");
        }

        private async static Task WriteToFile(int[,] a, string name)
        {
            string path = $@".\{name}";
            File.Create(path).Dispose();

            using (StreamWriter sw = new StreamWriter(path))
            {
                for(int i = 0; i < a.GetLength(0); ++i)
                {
                    StringBuilder sb = new StringBuilder();

                    for (int j = 0; j < a.GetLength(1); ++j)
                    {
                        sb.Append($"{a[i, j]} ");
                    }

                    await sw.WriteLineAsync(sb.ToString());
                }
            }
        }

        private static int[,] CreateRandomMatrix(int a1, int a2)
        {
            int[,] A = new int[a1, a2];

            for (int i = 0; i < a1; ++i)
            {
                for (int j = 0; j < a2; ++j)
                {
                    A[i, j] = _rand.Next(MinValue, MaxValue);
                }
            }

            return A;
        }

        private static void ReadArgs(string[] args) =>
            Parser.Default.ParseArguments<CmdOptions>(args)
                   .WithParsed(o =>
                   {
                       _options = o;
                   });

    }
}

