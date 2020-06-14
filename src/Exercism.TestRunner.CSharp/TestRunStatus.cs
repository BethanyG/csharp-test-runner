using System.Linq;

namespace Exercism.TestRunner.CSharp
{
    internal static class TestRunStatus
    {
        internal static TestStatus FromTestResults(TestResult[] testResults)
        {
            if (testResults.Any())
            {
                return testResults.Any(testResult => testResult.Status == TestStatus.Fail) ? TestStatus.Fail : TestStatus.Pass;
            }
            
            return TestStatus.Error;
        }
    }
}