using System;
using telegen.Results;

namespace telegen.Agents.Interfaces
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