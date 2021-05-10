using CommandLine;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Strassen3x3
{
    class Program
    {
        private const string DEFAULT_MATRIX1_PATRH = "./Matrix1.txt";
        private const string DEFAULT_MATRIX2_PATRH = "./Matrix2.txt";
        private static CmdOptions _options;

        static async Task Main(string[] args)
        {
            ReadArgs(args);

            int[,] A = await FileHelper.ReadFile(string.IsNullOrWhiteSpace(_options.Matrix1) ? DEFAULT_MATRIX1_PATRH : _options.Matrix1);
            int[,] B = await FileHelper.ReadFile(string.IsNullOrWhiteSpace(_options.Matrix2) ? DEFAULT_MATRIX2_PATRH : _options.Matrix2);

            //int[,] A = new int[,]
            //{
            //    { 1, 1 },
            //    { 1, 1 },
            //    { 1, 1 },
            //};

            //int[,] B = new int[,]
            //{
            //    { 2, 2, 2, 2 },
            //    { 2, 2, 2, 2 },
            //};

            //int[,] A = new int[,]
            //{
            //    { 1, 1 },
            //    { 1, 1 },
            //};

            //int[,] B = new int[,]
            //{
            //    { 2, 2 },
            //    { 2, 2 },
            //};


            //int[,] A = new int[,]
            //{
            //    { 1, 1, 1, 1, 1, 1, 1, 1, 1 },
            //    { 1, 1, 1, 1, 1, 1, 1, 1, 1 },
            //    { 1, 1, 1, 1, 165, 1, 1, 1, 13 },
            //    { 1, 1, 1, 1, 1, 1, 1, 1, 1 },
            //    { 1, 1, 1, 1, 1, 6, 1, 1, 1 },
            //    { 1, 1, 1, 78, 1, 1, 1, 1, 1 },
            //    { 1, 1, 1, 1, 1, 1, 1, 1, 1 },
            //    { 1, 1, 1, 1, 567, 1, 1, 1, 1 },
            //    { 1, 1, 1, 1, 1, 1, 1432, 1, 1 },
            //};

            //int[,] B = new int[,]
            //{
            //    { 1, 1, 1, 1, 1, 1, 1, 1, 1 },
            //    { 1, 1, 561, 1, 1, 1, 1, 1, 1 },
            //    { 1, 1, 1, 1, 1, 1, 1, 1, 1 },
            //    { 1, 1, 1, 1, 871, 1, 1, 1, 1 },
            //    { 1, 1, 1, 1, 1, 1, 1, 1, 1 },
            //    { 1, 1, 198, 1, 1, 1, 1, 1, 1 },
            //    { 1, 1, 1, 1, 1, 1, 1, 1, 1 },
            //    { 1, 1, 1, 1, 987, 1, 1, 1, 1 },
            //    { 1, 1, 1, 1, 1, 22, 1, 1, 1 },
            //};


            //            int[,] A = new int[,]
            //{
            //                { 3, 1, 1, 1, 1, 1, 1, 1, 1 },
            //                { 1, 1, 1, 1, 1, 1, 1, 1, 1 },
            //                { 1, 1, 1, 1, 1, 1, 1, 1, 1 },
            //                { 1, 1, 1, 1, 1, 1, 1, 1, 1 },
            //                { 1, 1, 1, 1, 1, 1, 1, 1, 1 },
            //                { 1, 1, 1, 1, 1, 1, 1, 1, 1 },
            //                { 1, 1, 1, 1, 1, 1, 1, 1, 1 },
            //                { 1, 1, 1, 1, 1, 1, 1, 1, 1 },
            //                { 1, 1, 1, 1, 1, 1, 1, 1, 1 },
            //            };

            //            int[,] B = new int[,]
            //            {
            //                { 2, 2, 2, 2, 2, 2, 2, 2, 2 },
            //                { 2, 2, 2, 2, 2, 2, 2, 2, 2 },
            //                { 2, 2, 2, 2, 2, 2, 2, 2, 2 },
            //                { 2, 2, 2, 2, 2, 2, 2, 2, 2 },
            //                { 2, 2, 2, 2, 2, 2, 2, 2, 2 },
            //                { 2, 2, 2, 2, 2, 2, 2, 2, 2 },
            //                { 2, 2, 2, 2, 2, 2, 2, 2, 2 },
            //                { 2, 2, 2, 2, 2, 2, 2, 2, 2 },
            //                { 2, 2, 2, 2, 2, 2, 2, 2, 2 },
            //            };




            Console.WriteLine("Matrices:");
            Console.WriteLine();
            A.Print();
            Console.WriteLine();
            B.Print();



            int[,] C = Multiplicator.Multiplicate(A, B);

            Console.WriteLine();
            Console.WriteLine("Result:");
            Console.WriteLine();
            C.Print();
            Console.WriteLine();

            await FileHelper.WriteOutput(C);

            Console.ReadKey();
        }

        private static void ReadArgs(string[] args) =>
            Parser.Default.ParseArguments<CmdOptions>(args)
                   .WithParsed(o =>
                   {
                       _options = o;
                   });
    }
}
