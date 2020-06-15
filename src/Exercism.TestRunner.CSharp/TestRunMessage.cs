using System;
using System.Collections.Generic;
using System.Linq;

namespace Exercism.TestRunner.CSharp
{
    internal static class TestRunMessage
    {
        internal static string FromErrors(IEnumerable<string> errors) =>
            string.Join("\n", errors.Select(FormatError));

        private static string FormatError(string error) =>
            error.RemoveFileName();
        
        private static string RemoveFileName(this string error) =>
            error.Substring(0, error.LastIndexOf(" [", StringComparison.OrdinalIgnoreCase));
    }
}