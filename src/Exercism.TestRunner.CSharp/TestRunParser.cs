using System;
using System.Collections.Generic;
using System.Linq;

namespace Exercism.TestRunner.CSharp
{
    internal class TestRunParser
    {
        internal static TestRun FromLogs(Options options)
        {
            var errors = BuildLogParser.ParseErrors(options);
            if (errors.Any())
            {
                return FromErrors(errors);
            }

            var testResults = TestsLogParser.ParseTestResults(options);
            return FromTestResults(testResults);
        }

        private static TestRun FromTestResults(TestResult[] testResults) =>
            new TestRun
            {
                Status = TestRunStatus.FromTestResults(testResults),
                Tests = testResults
            };

        private static TestRun FromErrors(IEnumerable<string> errors) =>
            new TestRun
            {
                Status = TestStatus.Error,
                Tests = Array.Empty<TestResult>(),
                Message = TestRunMessage.FromErrors(errors)
            };
    }
}