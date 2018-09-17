using telegen.Operations.Results;

namespace telegen.Agents
{
    public interface IReportAgent
    {
        void AddReportLine(Result evt);
    }
}