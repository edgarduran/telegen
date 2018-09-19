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

        /// <summary>
        /// Gets or sets the header data, in the format <code>Header.&lt;propertyName&gt; = "Column Name";</code>
        /// </summary>
        /// <value>
        /// The header.
        /// </value>
        [JsonProperty("Header")]
        public dynamic Header { get; set; }

        /// <summary>
        /// Gets the header fields. Provides enumerable access to all of the column names in the <see cref="Header"/> dynamic object.
        /// </summary>
        /// <value>
        /// The header fields.
        /// </value>
        [JsonIgnore]
        public IDictionary<string, object> HeaderFields => Header as IDictionary<string, object>;


        /// <summary>
        /// Gets or sets the layout. Report properties are placed in the layout
        /// by surrounding the property name with braces:
        /// <para><c>{propertyName1}, {propertyName2}...</c></para>
        /// </summary>
        /// <value>
        /// The layout string.
        /// </value>
        [JsonProperty("Layout")]
        public string Layout {
            get => KeepCrLfs ? _layout : _layout.Replace("\n", string.Empty).Replace("\r", string.Empty);
            set { _layout = _layout ?? value; }
        }

        /// <summary>
        /// Gets a value indicating whether line breaks in the layout are significant. If this value
        /// is false, then line breaks are removed from the layout string before it is applied.
        /// </summary>
        /// <value>
        ///   <c>true</c> if line breaks are significant; otherwise, <c>false</c>.
        /// </value>
        [JsonProperty("KeepCrLf")]
        public bool KeepCrLfs { get; }

    }
}
