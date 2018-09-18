using NLog;
using NLog.Config;
using NLog.Targets;

namespace telegen.Agents
{
    public class CustomReportAgent : NLogReportAgent
    {
        public CustomReportAgent(string filename, string layout)
        {
            log = ConfigureNLog(filename, layout);
        }

        protected override ILogger ConfigureNLog(string filename, string customLayout)
        {
            var cfg = new LoggingConfiguration();
            var tgt = new FileTarget("fileWriter") { FileName = filename, Layout = BuildLayout(customLayout) };
            cfg.LoggingRules.Add(new LoggingRule("*", LogLevel.Trace, tgt));
            LogManager.Configuration = cfg;

            return LogManager.GetLogger("Reporter");
        }
    }


}
