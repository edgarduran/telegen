using System;
using System.Collections.Generic;
using System.IO;
using telegen.Agents;
using telegen.Operations.Results;

namespace telegen
{
    public class ScriptEngine : IScriptEngine
    {
        protected IScriptTranslator Translator { get; }
        protected IDictionary<string, Func<IAgent>> Agents;

        public ScriptEngine(IScriptTranslator translator = null)
        {
            Translator = translator ?? new ScriptTranslator();
            Agents = new Dictionary<string, Func<IAgent>>
            {
                ["OpCreateFile"] = () => new FileAgent(),
                ["OpUpdateFile"] = () => new FileAgent(),
                ["OpDeleteFile"] = () => new FileAgent(),
                ["OpSpawn"] = () => new ProcessAgent(),
                ["OpNetGet"] = () => new NetworkAgent(),
                ["OpWait"] =() => new WaitAgent(),
                ["OpNop"] = () => new ExceptionAgent()
            };
        }

        public IEnumerable<Result> Execute(string filename) => Execute(File.ReadAllLines(filename));


        public IEnumerable<Result> Execute(IEnumerable<string> script)
        {
            var operations = Translator.Translate(script);
            foreach (var op in operations) {
                IAgent agent = NullAgent.Instance;
                if (Agents.ContainsKey(op.GetType().Name)) {
                    var agentFactory = Agents[op.GetType().Name];
                    agent = agentFactory != null ? agentFactory() : NullAgent.Instance;
                }
                yield return agent.Execute(op);
            }
            yield break;
        }
    }

}
