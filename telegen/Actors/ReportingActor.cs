using System;
using telegen.Agents;
using telegen.Messages.Log;

namespace telegen.Actors
{
    public class ReportingActor : TelegenActor
    {
        public ReportingActor(IReportAgent agent = null)
        {
            Agent = agent ?? new ReportAgent();
            Receive<LogEvent>(m => Agent.AddReportLine(m));
        }

        protected IReportAgent Agent { get; }
    }
}
