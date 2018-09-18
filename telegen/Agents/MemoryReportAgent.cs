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
            log = ConfigureNLog(null, layout);
        }

        protected override ILogger ConfigureNLog(string filename, string customLayout)
        {
            var cfg = new LoggingConfiguration();

#if DEBUG
            // Setup the logging view for Sentinel - http://sentinel.codeplex.com
            var sentinelTarget = new NLogViewerTarget()
            {
                Name = "sentinel",
                Address = "udp://127.0.0.1:9999",
                IncludeNLogData = true,
                Layout = BuildLayout(customLayout)
            };
            var sentinelRule = new LoggingRule("*", LogLevel.Trace, sentinelTarget);
            cfg.AddTarget("sentinel", sentinelTarget);
            cfg.LoggingRules.Add(sentinelRule);

#endif


            EmittedLogs = new MemoryTarget { Name = "mem", Layout = BuildLayout(customLayout) };

            cfg.AddTarget(EmittedLogs);
            cfg.LoggingRules.Add(new LoggingRule("*", LogLevel.Trace, EmittedLogs));
            LogManager.Configuration = cfg;

            return LogManager.GetLogger("Reporter");
        }

        public override string ToString()
        {
            return string.Join("\n", EmittedLogs.Logs);
        }
    }


}
