using System.Collections.Generic;
using NLog;
using NLog.Config;
using NLog.Targets;
using telegen.Agents;
using telegen.Agents.Interfaces;
using telegen.Results;
using Xunit;
using Xunit.Abstractions;
using Xunit.Sdk;

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
        public void TestNLogReportsFromCustomReportAgent() {

            var expected = "FileActivity,Create\nSpawns,";

            IReportAgent agent = new MemoryReportAgent("{Type},{FileEventType}");
            foreach (var e in GetLogEventSet001())
                agent.EmitDetailLine(e);

            //read the logs here
            agent.EmitFooter(); // Flush the buffer

            var logs = agent.ToString();
            output.WriteLine(logs);

            Assert.Equal(expected, logs);
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
