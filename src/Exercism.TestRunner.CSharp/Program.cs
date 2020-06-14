using System;
using CommandLine;

namespace Exercism.TestRunner.CSharp
{
    public static class Program
    {   
        public static void Main(string[] args) =>
            Parser.Default
                .ParseArguments<Options>(args)
                .WithParsed(CreateTestResults);

        private static void CreateTestResults(Options options)
        {
            Console.WriteLine($"Running test runner for {options.Slug} solution in directory {options.InputDirectory}");

            TestsFileRewriter.Rewrite(options);
            TestsRunner.Run(options);

            var testRun = TestRunParser.FromLogs(options);
            TestRunWriter.WriteToFile(options, testRun);

            Console.WriteLine($"Ran test runner for {options.Slug} solution in directory {options.OutputDirectory}");
        }
    }
}