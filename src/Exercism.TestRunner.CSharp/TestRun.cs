using System.Text.Json.Serialization;

namespace Exercism.TestRunner.CSharp
{
    internal class TestRun
    {
        [JsonPropertyName("status")]
        public TestStatus Status { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; }

        [JsonPropertyName("tests")]
        public TestResult[] Tests { get; set; }
    }
}