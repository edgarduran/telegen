using NLog;
using NLog.Config;
using NLog.Targets;
using telegen.Util;

namespace telegen.Agents
{
    public class CustomReportAgent : NLogReportAgent
    {
        public CustomReportAgent(string filename, ReportLayout layout) {
            ReportLayout = layout;
            log = ConfigureNLog(filename, layout.Layout);
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
            var tgt = new FileTarget("fileWriter")
            {
                Name = "FileWriter",
                FileName = filename,
                Layout = BuildLayout(customLayout)
            };
            cfg.AddTarget(tgt);
            cfg.LoggingRules.Add(new LoggingRule("*", LogLevel.Trace, tgt));
            LogManager.Configuration = cfg;

            return LogManager.GetLogger("Reporter");
        }
    }


}
