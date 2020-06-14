namespace Exercism.TestRunner.CSharp.IntegrationTests
{
    internal static class TestSolutionRunner
    {
        public static TestRun Run(string testSolutionDirectoryName)
        {
            var testSolution = CreateTestSolution(testSolutionDirectoryName);
            RunTestRunner(testSolution);
            return CreateTestRun(testSolution);
        }

        private static TestSolution CreateTestSolution(string testSolutionDirectoryName)
        {
            var solutionDirectory = TestSolutionDirectory.CopySolution(testSolutionDirectoryName);
            return new TestSolution("fake", solutionDirectory.FullName);
        }

        private static void RunTestRunner(TestSolution testSolution) =>
            Program.Main(new[] { testSolution.Slug, testSolution.Directory, testSolution.Directory });

        private static TestRun CreateTestRun(TestSolution solution)
        {
            var actualTestRunResult = TestRunResultReader.ReadActual(solution);
            var expectedTestRunResult = TestRunResultReader.ReadExpected(solution);

            return new TestRun(expectedTestRunResult, actualTestRunResult);
        }
    }
}