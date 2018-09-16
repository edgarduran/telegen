using System.Collections.Generic;
using NLog;
using NLog.Config;
using NLog.Targets;
using telegen.Agents;
using telegen.Messages.Log;
using Xunit;
using Xunit.Abstractions;

namespace telegen.Tests
{
    public class TestReports
    {
        private readonly ITestOutputHelper output;

        protected MemoryTarget EmittedLogs { get; set; }

        protected const string BasicNLogLayout = "${message}";
        protected const string CustomNLogLayout = @"${event-properties:item=_Type},${event-properties:item=UTCStart},""${event-properties:item=ProcessName}"",${event-properties:item=ProcessId},${event-properties:item=UserName},${event-properties:item=FileEventType}";
        protected const string TestNLogLayout = "[${event-properties:item=UTCStart}]";

        public TestReports(ITestOutputHelper output)
        {
            this.output = output;
        }



        protected void InitNLogContext(string layout)
        {
            //init with empty configuration. Add one target and one rule
            var configuration = new NLog.Config.LoggingConfiguration();
            EmittedLogs = new MemoryTarget { Name = "mem", Layout = layout};

            configuration.AddTarget(EmittedLogs);
            configuration.LoggingRules.Add(new LoggingRule("*", LogLevel.Trace, EmittedLogs));
            LogManager.Configuration = configuration;
        }

        [Fact]
        public void TestNLogReportsFromReportAgent()
        {
            InitNLogContext(BasicNLogLayout);
            IReportAgent agent = new ReportAgent();
            foreach (var e in GetLogEventSet001()) agent.AddReportLine(e);

            var expected = new List<string>
            {
                @"{""FileName"":""Test.File"",""FileEventType"":""Create"",""UserName"":""Tester"",""CommandLine"":null,""ProcessName"":""dotnet"",""UTCStart"":""2000-01-01T01:00:00"",""TimeString"":""2000-01-01 01:00:00Z"",""ProcessId"":18966}",
                @"{""CommandLine"":""commandline"",""UserName"":""Tester"",""ProcessName"":""Process"",""UTCStart"":""2000-01-01T01:00:00"",""TimeString"":""2000-01-01 01:00:00Z"",""ProcessId"":1}"
            };

            //read the logs here
            LogManager.Flush();
            var logs = EmittedLogs.Logs;
            foreach (var log in logs)
            {
                output.WriteLine(log);
            }
            Assert.All(logs, s => expected.Contains(s));
        }



        [Fact]
        public void TestNLogReportsFromCustomReportAgent()
        {
            InitNLogContext(CustomNLogLayout);
            IReportAgent agent = new CustomizableReportAgent();
            foreach (var e in GetLogEventSet001()) agent.AddReportLine(e);

            var expected = new List<string>
            {
                @"{""FileName"":""Test.File"",""FileEventType"":""Create"",""UserName"":""Tester"",""CommandLine"":null,""ProcessName"":""dotnet"",""UTCStart"":""2000-01-01T01:00:00"",""TimeString"":""2000-01-01 01:00:00Z"",""ProcessId"":18966}",
                @"{""CommandLine"":""commandline"",""UserName"":""Tester"",""ProcessName"":""Process"",""UTCStart"":""2000-01-01T01:00:00"",""TimeString"":""2000-01-01 01:00:00Z"",""ProcessId"":1}"
            };

            //read the logs here
            LogManager.Flush();
            var logs = EmittedLogs.Logs;
            foreach (var log in logs)
            {
                output.WriteLine(log);
            }
            //Assert.All(logs, s => expected.Contains(s));
        }

        public IEnumerable<LogEvent> GetLogEventSet001()
        {
            LogEvent evt = new FileActivity(
                new System.DateTime(2000,01,01,01,00,00,00,00),
                "Test.File",
                FileEventType.Create,
                "Tester"
                );
            yield return evt;

            evt = new Spawn(
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
