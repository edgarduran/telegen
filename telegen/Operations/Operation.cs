using Newtonsoft.Json;

namespace telegen.Operations
{
    /// <summary>
    /// Operations describe the events requested of the telegen framework. They
    /// contain all of the data required to perform the corresponding task. The target
    /// <c>IAgent</c> extracts this data and performs the action, if possible. It returns
    /// a <c>Result</c> object describing the completed task. 
    /// </summary>
    /// <seealso cref="telegen.Agents.Interfaces.IAgent"/>
    public abstract class Operation
    {
        protected Operation()
        {
        }

        /// <summary>
        /// Returns a JSON-formatted <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString() => JsonConvert.SerializeObject(this);
    }
}
