using System;
using System.Collections.Generic;
using System.Dynamic;
using Newtonsoft.Json;

namespace telegen.Operations
{

    /// <summary>
    /// Operation.
    /// </summary>
    public class Operation : DynamicObject
    {
        public Operation()
        {

        }

        [JsonProperty("domain")]
        public string Domain { get; set; }

        [JsonProperty("action")]
        public string Action { get; set; }

        #region Other Properties
        private readonly IDictionary<string, object> _props = new Dictionary<string, object>();

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            _props[binder.Name] = value;
            return _props[binder.Name] == value;
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            return GetPropertyByName(binder.Name.ToLowerInvariant(), out result);
        }

        public virtual bool GetPropertyByName(string name, out object result)
        {
            name = name.ToLowerInvariant();
            if (_props.ContainsKey(name))
            {
                result = _props[name];
                return true;
            }
            else
            {
                result = null;
                return false;
            }
        }

        public override IEnumerable<string> GetDynamicMemberNames()
        {
            return _props.Keys;
        }
        #endregion

        #region Helpers

        public T Require<T>(string name)
        {
            if (!GetPropertyByName(name, out object result))
            {
                throw new Exception($"{Domain}.{Action}: Required parameter missing");
            }
            return (T)result;
        }

        public (T1, T2) Require<T1, T2>(string n1, string n2) => (Require<T1>(n1), Require<T2>(n2));

        public (T1, T2, T3) Require<T1, T2, T3>(string n1, string n2, string n3) => (Require<T1>(n1), Require<T2>(n2), Require<T3>(n3));

        public T Optional<T>(string name, T defValue = default(T))
        {
            if (!GetPropertyByName(name, out object result))
            {
                result = defValue;
            }
            return (T)result;
        }

        public (T1, T2) Optional<T1, T2>((string name, T1 defValue) q1, (string name, T2 defValue) q2)
        {
            return (Optional(q1.name, q1.defValue), Optional(q2.name, q2.defValue));
        }

        #endregion

        /// <summary>
        /// Returns a JSON-formatted <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString() => JsonConvert.SerializeObject(this);
    }

}
