using System.Collections.Generic;

namespace Exercism.TestRunner.CSharp
{
    internal static class TestRunMessage
    {
        internal static string FromErrors(IEnumerable<string> errors) =>
            string.Join("\n", errors);
    }
}