using System;
using NLog;
using telegen.Messages.Log;

namespace telegen.Agents
{
    public class ReportAgent 
    {
        protected ILogger Report { get; }
        protected const string LoggerName = "Report";

        public ReportAgent()
        {
            Report = LogManager.GetLogger(LoggerName);
        }

        public virtual void AddReportLine(LogEvent evt)
        {
            var payload = evt.ToString();
            Report.Info(payload);
        }

    }

    public class CustomizableReportAgent : ReportAgent
    {
        public override void AddReportLine(LogEvent evt)
        {
            var e = new LogEventInfo(LogLevel.Info, LoggerName, evt.GetType().Name);
            evt.CopyToDictionary(e.Properties);
            Report.Info(e);

        }

    }
}
