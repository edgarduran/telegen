using System.Collections.Generic;
using NLog;
using NLog.Config;
using NLog.Targets;
using telegen.Agents;
using telegen.Operations.Results;
using Xunit;
using Xunit.Abstractions;

namespace telegen.Tests
{
    public class TestReports
    {
        private readonly ITestOutputHelper output;

        protected MemoryTarget EmittedLogs { get; set; }


        public TestReports(ITestOutputHelper output)
        {
            this.output = output;
        }


        [Fact]
        public void TestNLogReportsFromReportAgent()
        {
            IReportAgent agent = new MemoryReportAgent("{Type},{FileEventType");
            foreach (var e in GetLogEventSet001()) agent.EmitDetailLine(e);

            var expected = new List<string>
            {
                @"{""FileName"":""Test.File"",""FileEventType"":""Create"",""UserName"":""Tester"",""CommandLine"":null,""ProcessName"":""dotnet"",""UTCStart"":""2000-01-01T01:00:00"",""TimeString"":""2000-01-01 01:00:00Z"",""ProcessId"":18966}",
                @"{""CommandLine"":""commandline"",""UserName"":""Tester"",""ProcessName"":""Process"",""UTCStart"":""2000-01-01T01:00:00"",""TimeString"":""2000-01-01 01:00:00Z"",""ProcessId"":1}"
            };

            //read the logs here
            agent.EmitFooter();
            var logs = agent.ToString(); 
            output.WriteLine(logs);

            Assert.True(expected.Contains(logs));
        }



        [Fact]
        public void TestNLogReportsFromCustomReportAgent()
        {
            IReportAgent agent = new MemoryReportAgent("{Type},{FileEventType");
            foreach (var e in GetLogEventSet001()) agent.EmitDetailLine(e);

            var expected = new List<string>
            {
                @"{""FileName"":""Test.File"",""FileEventType"":""Create"",""UserName"":""Tester"",""CommandLine"":null,""ProcessName"":""dotnet"",""UTCStart"":""2000-01-01T01:00:00"",""TimeString"":""2000-01-01 01:00:00Z"",""ProcessId"":18966}",
                @"{""CommandLine"":""commandline"",""UserName"":""Tester"",""ProcessName"":""Process"",""UTCStart"":""2000-01-01T01:00:00"",""TimeString"":""2000-01-01 01:00:00Z"",""ProcessId"":1}"
            };

            //read the logs here
            agent.EmitFooter();
            var logs = agent.ToString();
            output.WriteLine(logs);

            Assert.True(expected.Contains(logs));
        }

        public IEnumerable<Result> GetLogEventSet001()
        {
            Result evt = new FileActivityResult(
                new System.DateTime(2000,01,01,01,00,00,00,00),
                "Test.File",
                FileEventType.Create,
                "Tester"
                );
            yield return evt;

            evt = new SpawnResults(
                "Process",
                new System.DateTime(2000, 01, 01, 01, 00, 00, 00, 00),
                001,
                "Tester",
                "commandline"
            );
            yield return evt;
        }
    }

}
