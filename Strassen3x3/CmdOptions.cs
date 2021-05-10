using CommandLine;

namespace Strassen3x3
{
    public class CmdOptions
    {
        [Option('a', "first-matrix", Required = false, HelpText = "Input file with first matrix")]
        public string Matrix1 { get; set; }

        [Option('b', "second-matrix", Required = false, HelpText = "Input file with seecond matrix")]
        public string Matrix2 { get; set; }
    }
}
