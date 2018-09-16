using telegen.Messages.Log;

namespace telegen.Agents
{
    public interface IReportAgent
    {
        void AddReportLine(LogEvent evt);
    }
}