using CommandLine;
using System.Collections.Generic;

namespace ChangeDT
{
    // Define a class to receive parsed values
    class ParsedOptions
    {
        //[Option('r', "read", Required = false, HelpText = "Path to be processed.")]
        //public IEnumerable<string> InputFiles { get; set; }

        [Option('p', "path", Required = true, HelpText = "Path to be processed.")]
        public string Path { get; set; }

        [Option('d', "day", Required = true, HelpText = "Day")]
        public int Day { get; set; }

        [Option('m', "month", Required = true, HelpText = "Month")]
        public int Month { get; set; }

        [Option('y', "year", Required = true, HelpText = "Year")]
        public int Year { get; set; }

        // Omitting long name, defaults to name of property, ie "--verbose"
        [Option('v', "verbose", Default = false, HelpText = "Prints all messages to standard output.")]
        public bool Verbose { get; set; }
    }
}
