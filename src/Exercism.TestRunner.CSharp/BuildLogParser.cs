using System.IO;
using System.Linq;

namespace Exercism.TestRunner.CSharp
{
    internal static class BuildLogParser
    {
        internal static string[] ParseErrors(Options options) =>
            File.ReadLines(options.MsBuildLogFilePath).ToArray();
    }
}