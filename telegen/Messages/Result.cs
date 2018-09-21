using System;
using System.Collections.Generic;
using System.Dynamic;
using Newtonsoft.Json;
using telegen.Messages;

namespace telegen.Results
{

    public class Result : MessageBase
    {
        public Result()
        {
            AsDynamic.timeStampUtc = DateTime.UtcNow;
        }

        public Result(Operation op)
        {
            Clear("timeStampUtc");
            Domain = op.Domain;
            Action = op.Action;
            this.AsDynamic.timeStampUtc = DateTime.UtcNow;
            ProcessInfo.Instance.Stamp(this.AsDynamic);
        }

        [JsonIgnore]
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

        [JsonIgnore]
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
