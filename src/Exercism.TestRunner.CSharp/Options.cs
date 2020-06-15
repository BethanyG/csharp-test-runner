using System.IO;
using CommandLine;
using Humanizer;

namespace Exercism.TestRunner.CSharp
{
    internal class Options
    {
        [Value(0, Required = true, HelpText = "The solution's exercise")]
        public string Slug { get; }

        [Value(1, Required = true, HelpText = "The directory containing the solution")]
        public string InputDirectory { get; }

        [Value(2, Required = true, HelpText = "The directory to which the results will be written")]
        public string OutputDirectory { get; }

        public string TestsFilePath => Path.Combine(InputDirectory, $"{Slug.Dehumanize().Pascalize()}Tests.cs");
        public string MsBuildLogFilePath => Path.Combine(InputDirectory, "msbuild.log");
        public string TestsXmlLogFilePath => Path.Combine(InputDirectory, "TestResults", "tests.trx");
        public string ResultsJsonFilePath => Path.Combine(OutputDirectory, "results.json");

        public Options(string slug, string inputDirectory, string outputDirectory) =>
            (Slug, InputDirectory, OutputDirectory) = (slug, inputDirectory, outputDirectory);
    }
}