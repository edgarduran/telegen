using System;
using telegen.Operations.Results;

namespace telegen.Agents
{
    public interface IReportAgent : IDisposable
    {
        bool HeadersAreRequired { get; }
        bool FootersAreRequired { get; }
        void EmitHeader(dynamic header = null);
        void EmitDetailLine(Result evt);
        void EmitFooter();
    }


}