using System.IO;
using Newtonsoft.Json;
using telegen.Operations.Results;

namespace telegen.Agents
{
    public class JSONReportAgent : IReportAgent
    {
        public JSONReportAgent(string filename)
        {
            Filename = filename;
        }

        public bool HeadersAreRequired => true;

        public bool FootersAreRequired => true;

        protected string Filename { get; }

        public void EmitHeader(dynamic header = null)
        {
            var text = "[\n";
            if (header != null)
                text += JsonConvert.SerializeObject(header);
            File.WriteAllText(Filename, text);
        }

        public void EmitDetailLine(Result evt)
        {
            var text = evt.ToString();
            File.AppendAllText(Filename, ",\n" + text);
        }

        public void EmitFooter()
        {
            File.AppendAllText(Filename, "\n]");
        }

        public void Dispose()
        {
        }
    }
}
