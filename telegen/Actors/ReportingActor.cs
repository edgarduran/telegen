using System;
using telegen.Agents;
using telegen.Operations.Results;

namespace telegen.Actors
{
    public class ReportingActor : TelegenActor
    {
        public ReportingActor(IReportAgent agent = null)
        {
            Agent = agent ?? new ReportAgent();
            Receive<Result>(m => Agent.AddReportLine(m));
        }

        protected IReportAgent Agent { get; }
    }
}
