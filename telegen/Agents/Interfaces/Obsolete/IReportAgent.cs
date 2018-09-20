using System;
using telegen.Results;

namespace telegen.Agents.Interfaces
{
    /// <summary>
    /// Receives and formats <see cref="Result" /> data.
    /// </summary>
    /// <seealso cref="System.IDisposable" />
    public interface IReportAgent : IDisposable
    {
        /// <summary>
        /// Gets a value indicating whether the client must call the <see cref="EmitHeader"/>
        /// function before sending data to this Report Agent.
        /// <para>Some output formats, including some Xml and Json formats, require special formatting
        /// before and after the body of the report. This property indicates whether this agent's output
        /// requires such a header.</para>
        /// <para>Alternately, the report agent may need to do some one-time initialization, and may
        /// do so in this method.</para>
        /// </summary>
        /// <value>
        ///   <c>true</c> if the call is required; otherwise, <c>false</c>.
        /// </value>
        bool HeadersAreRequired { get; }

        /// <summary>
        /// Gets a value indicating whether the client must call the <see cref="EmitFooter"/> method after sending
        /// all of the data to the report agent. This may be required to include a closing footer, or to do some
        /// report engine cleanup.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the call is required; otherwise, <c>false</c>.
        /// </value>
        bool FootersAreRequired { get; }

        /// <summary>
        /// Performs any required engine initialization and emits the report header (if any).
        /// </summary>
        /// <param name="header">The header data.</param>
        void EmitHeader(dynamic header = null);

        /// <summary>
        /// Emits a detail line.
        /// </summary>
        /// <param name="evt">The data for the report line.</param>
        void EmitDetailLine(Result evt);

        /// <summary>
        /// Emits the footer and cleans up the report engine.
        /// </summary>
        void EmitFooter();
    }


}