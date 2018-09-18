using System;
using NLog;
using NLog.Layouts;
using telegen.Operations.Results;

namespace telegen.Agents
{

    public abstract class NLogReportAgent : IReportAgent
    {
        protected ILogger log = new NullLogger(new LogFactory());

        public bool HeadersAreRequired => false;

        public bool FootersAreRequired => true;

        public void EmitHeader(dynamic header = null)
        {
            // Do Nothing
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
