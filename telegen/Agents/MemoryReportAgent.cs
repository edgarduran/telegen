using NLog;
using NLog.Config;
using NLog.Targets;

namespace telegen.Agents
{
    public class MemoryReportAgent : NLogReportAgent
    {
        MemoryTarget EmittedLogs = null;

        public MemoryReportAgent(string layout)
        {
            ConfigureNLog(null, layout);
        }

        protected override ILogger ConfigureNLog(string filename, string customLayout)
        {
            var configuration = new LoggingConfiguration();
            EmittedLogs = new MemoryTarget { Name = "mem", Layout = BuildLayout(customLayout) };

            configuration.AddTarget(EmittedLogs);
            configuration.LoggingRules.Add(new LoggingRule("*", LogLevel.Trace, EmittedLogs));
            LogManager.Configuration = configuration;

            return LogManager.GetLogger("Reporter");
        }

        public override string ToString()
        {
            return string.Join("\n", EmittedLogs.Logs);
        }
    }


}
