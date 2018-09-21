using System;
using System.Collections.Generic;
using System.Dynamic;
using Newtonsoft.Json;

namespace telegen.Messages
{

    /// <summary>
    /// Operation.
    /// </summary>
    public class Operation : MessageBase
    {
        public Operation () : this (false, true)
        {

        }

        public Operation(bool caseSensitive = false, bool readOnly = true) : base(caseSensitive, readOnly)
        {
        }

        public Operation(string domain, string action, bool caseSensitive = false, bool readOnly = true) : base(caseSensitive, readOnly)
        {
            Domain = domain;
            Action = action;
        }

        [JsonProperty("domain")]
        public string Domain
        {
            get
            {
                GetPropertyByName(nameof(Domain), out object value);
                return (string)value;
            }
            set
            {
                SetPropertyByName(nameof(Domain), value);
            }
        }

        [JsonProperty("action")]
        public string Action
        {
            get
            {
                GetPropertyByName(nameof(Action), out object value);
                return (string)value;
            }
            set
            {
                SetPropertyByName(nameof(Action), value);
            }
        }
    }

}
