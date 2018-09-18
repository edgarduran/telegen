using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace telegen.Util
{
    public class ReportLayout {
        private string _layout = null;

        public static ReportLayout Open(string filename) {
            var def = File.ReadAllText(filename);
            var rpt = JsonConvert.DeserializeObject<ReportLayout>(def);
            return rpt;
        }

        public ReportLayout() {
            Header = new ExpandoObject();
        }

        [JsonProperty("Header")]
        public dynamic Header { get; set; }

        [JsonIgnore]
        public IDictionary<string, object> HeaderFields => Header as IDictionary<string, object>;

        [JsonProperty("Layout")]
        public string Layout {
            get => KeepCrLfs ? _layout : _layout.Replace("\n", string.Empty).Replace("\r", string.Empty);
            set { _layout = _layout ?? value; }
        }

        [JsonProperty("KeepCrLfs")]
        public bool KeepCrLfs { get; }

    }
}
