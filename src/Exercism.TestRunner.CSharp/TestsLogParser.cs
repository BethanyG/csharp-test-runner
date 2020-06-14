using System;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;

namespace Exercism.TestRunner.CSharp
{
    internal static class TestsLogParser
    {
        internal static TestResult[] ParseTestResults(Options options)
        {
            if (!File.Exists(options.TestsXmlLogFilePath))
            {
                return Array.Empty<TestResult>();
            }
        
            using var fileStream = File.OpenRead(options.TestsXmlLogFilePath);
            using var xmlReader = XmlReader.Create(fileStream);  
            
            var root = XElement.Load(xmlReader);
            var namespaceManager = new XmlNamespaceManager(xmlReader.NameTable);
            namespaceManager.AddNamespace("ns", root.Name.NamespaceName);

            var xmlUnitTestResults = root.XPathSelectElements("./ns:Results/ns:UnitTestResult", namespaceManager);
            return xmlUnitTestResults
                .Select(xmlUnitTestResult => XmlUnitTestResultToTestResult(xmlUnitTestResult, namespaceManager))
                .ToArray();
        }

        private static TestResult XmlUnitTestResultToTestResult(XElement xmlUnitTestResult, IXmlNamespaceResolver resolver) =>
            new TestResult
            {
                Name = xmlUnitTestResult.XPathString("@testName", resolver),
                Status = xmlUnitTestResult.XPathString("@outcome", resolver) == "Passed" ? TestStatus.Pass : TestStatus.Fail,
                Message = xmlUnitTestResult.XPathString("./ns:Output/ns:ErrorInfo/ns:Message", resolver),
                Output = xmlUnitTestResult.XPathString("./ns:Output/ns:StdOut", resolver)
            };
        
        private static string XPathString(this XElement xElement, string expression, IXmlNamespaceResolver resolver)
        {
            var str = xElement.XPathEvaluate($"string({expression})", resolver).ToString();
            return string.IsNullOrWhiteSpace(str) ? null : str.Trim().Replace("\r\n", "\n");
        }
    }
}