using CommandLine;

namespace InputGenerator
{
    public class CmdOptions
    {
        [Option('m', "first-matrix-first-dimention", Required = false, HelpText = "First Dimention of first matrix")]
        public int A1 { get; set; }

        [Option('n', "second-matrix-first-dimention", Required = false, HelpText = "Second Dimention of first matrix")]
        public int A2 { get; set; }

        [Option('k', "second-matrix-first-dimention", Required = false, HelpText = "First Dimention of second matrix")]
        public int B1 { get; set; }

        [Option('l', "second-matrix-second-dimention", Required = false, HelpText = "Second Dimention of Second matrix")]
        public int B2 { get; set; }
    }
}
