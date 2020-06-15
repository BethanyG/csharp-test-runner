using Xunit;

namespace Exercism.TestRunner.CSharp.IntegrationTests
{
    public class TestRunnerTests
    {
        [Fact]
        public void MultipleCompileErrors() =>
            AssertTestResultsAreCorrect("MultipleCompileErrors");

        [Fact]
        public void MultipleTestClassesWithAllPasses() =>
            AssertTestResultsAreCorrect("MultipleTestClassesWithAllPasses");

        [Fact]
        public void MultipleTestsWithAllPasses() =>
            AssertTestResultsAreCorrect("MultipleTestsWithAllPasses");

        [Fact]
        public void MultipleTestsWithMultipleFails() =>
            AssertTestResultsAreCorrect("MultipleTestsWithMultipleFails");

        [Fact]
        public void MultipleTestsWithSingleFail() =>
            AssertTestResultsAreCorrect("MultipleTestsWithSingleFail");

        [Fact]
        public void MultipleTestsWithTestOutput() =>
            AssertTestResultsAreCorrect("MultipleTestsWithTestOutput");

        [Fact]
        public void MultipleTestsWithTestOutputExceedingLimit() =>
            AssertTestResultsAreCorrect("MultipleTestsWithTestOutputExceedingLimit");

        [Fact]
        public void NetCoreApp2_1() =>
            AssertTestResultsAreCorrect("NetCoreApp2.1");

        [Fact]
        public void NetCoreApp2_2() =>
            AssertTestResultsAreCorrect("NetCoreApp2.2");

        [Fact]
        public void NetCoreApp3_0() =>
            AssertTestResultsAreCorrect("NetCoreApp3.0");

        [Fact]
        public void NetCoreApp3_1() =>
            AssertTestResultsAreCorrect("NetCoreApp3.1");

        [Fact]
        public void NoTests() =>
            AssertTestResultsAreCorrect("NoTests");

        [Fact]
        public void NotImplemented() =>
            AssertTestResultsAreCorrect("NotImplemented");

        [Fact]
        public void SingleCompileError() =>
            AssertTestResultsAreCorrect("SingleCompileError");

        [Fact]
        public void SingleTestThatFails() =>
            AssertTestResultsAreCorrect("SingleTestThatFails");

        [Fact]
        public void SingleTestThatPasses() =>
            AssertTestResultsAreCorrect("SingleTestThatPasses");

        private static void AssertTestResultsAreCorrect(string solutionDirectoryName)
        {
            var testRun = TestSolutionRunner.Run(solutionDirectoryName);
            Assert.Equal(testRun.Expected, testRun.Actual);
        }
    }
}