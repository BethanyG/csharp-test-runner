using System.Text.Json.Serialization;

namespace Exercism.TestRunner.CSharp
{
    internal class TestResult
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("status")]
        public TestStatus Status { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; }

        [JsonPropertyName("output")]
        public string Output { get; set; }
    }
}