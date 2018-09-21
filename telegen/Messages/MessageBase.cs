using System;
using System.Collections.Generic;
using System.Dynamic;
using Newtonsoft.Json;

namespace telegen.Messages
{
    public abstract class MessageBase : DynamicObject
    {
        public MessageBase(bool caseSensitive = false, bool readOnly = true)
        {
            CaseSensitive = caseSensitive;
            ReadOnly = readOnly;
        }

        #region Other Properties

        [JsonIgnore]
        public dynamic AsDynamic => this as dynamic;

        [JsonIgnore]
        public bool CaseSensitive { get; }

        [JsonIgnore]
        public bool ReadOnly { get; }

        private readonly IDictionary<string, object> _props = new Dictionary<string, object>();

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            var name = binder.Name.ToLowerInvariant();
            _props[name] = value;
            return _props[name] == value;
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            return GetPropertyByName(binder.Name.ToLowerInvariant(), out result) || true; // Just return null, if property doesn't exist.
        }

        public virtual bool SetPropertyByName(string name, object value)
        {
            name = CaseSensitive ? name : name.ToLowerInvariant();
            if (_props.ContainsKey(name) && ReadOnly)
            {
                throw new Exception("Attempt to update an existing property of a read-only message.");
            }
            else
            {
                _props[name] = value;
                return true;
            }
        }

        public virtual bool GetPropertyByName(string name, out object result)
        {
            name = CaseSensitive ? name : name.ToLowerInvariant();
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

        /// <summary>
        /// Removes the properties specified so that they may be reassigned in a
        /// "readonly" message. This should be used with care, and only by the
        /// method creating the message.
        /// </summary>
        /// <param name="propertyNames">Property names.</param>
        public void Clear(params string[] propertyNames)
        {
            foreach (var n in propertyNames) {
                var name = CaseSensitive ? n : n.ToLowerInvariant();
                if (_props.ContainsKey(name)) _props.Remove(name);
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
                throw new Exception($"{AsDynamic.domain}.{AsDynamic.action}: Required element missing");
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
