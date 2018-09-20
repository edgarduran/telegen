using System;
using System.Collections.Generic;
using System.Dynamic;
using Newtonsoft.Json;

namespace telegen.Operations
{

    /// <summary>
    /// Operation.
    /// </summary>
    public class Operation : DynamicBase
    {
        public Operation()
        {

        }

        [JsonProperty("domain")]
        public string Domain { get; set; }

        [JsonProperty("action")]
        public string Action { get; set; }
    }

}
