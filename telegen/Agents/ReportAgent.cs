using System;
using NLog;
using telegen.Operations.Results;

namespace telegen.Agents
{
    public class ReportAgent : IReportAgent
    {
        protected ILogger Report { get; }
        protected const string LoggerName = "Report";

        public ReportAgent()
        {
            Report = LogManager.GetLogger(LoggerName);
        }

        public virtual void AddReportLine(Result evt)
        {
            var payload = evt.ToString();
            Report.Info(payload);
        }

    }

    public class CustomizableReportAgent : ReportAgent
    {
        public override void AddReportLine(Result evt)
        {
            var e = new LogEventInfo(LogLevel.Info, LoggerName, evt.GetType().Name);
            evt.CopyToDictionary(e.Properties);
            Report.Info(e);
        }

    }
}
