using System.Diagnostics;

namespace Exercism.TestRunner.CSharp
{
    internal static class TestsRunner
    {
        internal static void Run(Options options)
        {
            var processStartInfo = new ProcessStartInfo("dotnet", "test --verbosity=quiet --logger \"trx;LogFileName=tests.trx\" /flp:v=q")
            {
                WorkingDirectory = options.InputDirectory,
                RedirectStandardError = true,
                RedirectStandardOutput = true
            };
            
            Process.Start(processStartInfo)?.WaitForExit();
        }
    }
}