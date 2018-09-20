using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NLog;
using NLog.Layouts;
using telegen.Agents.Interfaces;
using telegen.Results;
using telegen.Util;

namespace telegen.Agents
{

    public abstract class NLogReportAgent : IReportAgent
    {
        protected ReportLayout ReportLayout { get; set; }
        protected ILogger log = new NullLogger(new LogFactory());

        public bool HeadersAreRequired => false;

        public bool FootersAreRequired => true;

        public virtual void EmitHeader(dynamic header = null) {
            if (ReportLayout == null) return;
            if (!ReportLayout.HeaderFields.Any()) return;
            var evt = new LogEventInfo(LogLevel.Info, log.Name, "Header");
            var hf = ReportLayout.HeaderFields;
            foreach (var k in hf.Keys) evt.Properties[k] = hf[k];
            log.Info(evt);
        }

        public void EmitDetailLine(Result logEntry)
        {
            var evt = new LogEventInfo(LogLevel.Info, log.Name, logEntry.GetType().Name);
            logEntry.CopyToDictionary(evt.Properties);
            log.Info(evt);
        }

        public void EmitFooter()
        {
            LogManager.Flush();
        }

        protected abstract ILogger ConfigureNLog(string filename, string customLayout);

        /// <summary>
        /// This method is responsible for parsing the layout string, and translating it
        /// into the format required by the reporting engine... in this case, NLog. The
        /// method is declared <c>virtual</c> so that other implementations can use the
        /// existing NLog tooling, but with a different layout string dialect.
        /// </summary>
        /// <param name="def">The generic layout definition.</param>
        /// <returns>An NLog <see cref="Layout"/> object.</returns>
        /// <exception cref="Exception">
        /// </exception>
        protected virtual Layout BuildLayout(string def)
        {
            var layout = string.Empty;
            var name = string.Empty;
            const char startName = '{';
            const char endName = '}';

            bool inName = false;
            var pos = 0;
            foreach (var c in def.ToCharArray())
            {
                pos++;
                switch (c)
                {
                    case startName:
                        if (inName)
                        {
                            throw new Exception($"Malformed custom layout near position {pos} : {def.Substring(0, pos)}");
                        }
                        else
                        {
                            inName = true;
                        }
                        break;
                    case endName:
                        if (inName)
                        {
                            inName = false;
                            layout += $"${{event-properties:item={name}}}";
                            name = String.Empty;
                        }
                        else
                        {
                            throw new Exception($"Malformed custom layout near position {pos} : {def.Substring(0, pos)}");
                        }
                        break;
                    default:
                        if (inName)
                        {
                            name += c;
                        }
                        else
                        {
                            layout += c;
                        }
                        break;

                }
            }
            if (inName) throw new Exception($"Malformed custom layout in name '{name}'.");
            if (string.IsNullOrWhiteSpace(layout)) throw new Exception($"Malformed custom layout: no layout found.");
            return layout;
        }

        public void Dispose()
        {
        }
    }


}
