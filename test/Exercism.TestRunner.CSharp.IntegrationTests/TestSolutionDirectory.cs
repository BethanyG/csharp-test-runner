using System.IO;

namespace Exercism.TestRunner.CSharp.IntegrationTests
{
    internal static class TestSolutionDirectory
    {
        internal static DirectoryInfo CopySolution(string testSolutionDirectoryName)
        {
            var targetDirectory = new DirectoryInfo("TestSolution");
            var sourceDirectory = new DirectoryInfo(Path.Combine("Solutions", testSolutionDirectoryName));

            if (targetDirectory.Exists)
            {
                targetDirectory.Delete(recursive: true);
            }

            targetDirectory.Create();

            foreach (var sourceFile in sourceDirectory.EnumerateFiles())
            {
                sourceFile.CopyTo(Path.Combine(targetDirectory.FullName, sourceFile.Name));
            }

            return targetDirectory;
        }
    }
}