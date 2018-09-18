using System.Collections.Generic;
using Newtonsoft.Json;
using NLog;
using NLog.Config;
using NLog.Targets;
using telegen.Agents;
using telegen.Agents.Interfaces;
using telegen.Results;
using telegen.Util;
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
        [Theory]
        [InlineData("Scripts\\_Layouts\\CSV.Layout")]
        [InlineData("Scripts\\_Layouts\\TSV.Layout")]
        public void TestLayout(string filename) {
            var layout = ReportLayout.Open(filename);
            foreach (var f in layout.HeaderFields) {
                output.WriteLine($"{f.Key} = {f.Value}");
            }
            output.WriteLine(layout.Layout);


        }

        [Fact]
        public void TestNLogReportsFromCustomReportAgent() {

            var expected = "FileActivity,Create\nFileActivity,Delete";

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

            evt = new FileActivityResult(
                new System.DateTime(2000, 01, 01, 01, 00, 00, 00, 00),
                "Test.File",
                FileEventType.Delete,
                "Tester"
            );
            yield return evt;
        }
    }

}
