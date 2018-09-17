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
            Agents = new Dictionary<string, Func<IAgent>>();
            Agents["OpCreateFile"] = () => new FileAgent();
            Agents["OpUpdateFile"] = () => new FileAgent();
            Agents["OpDeleteFile"] = () => new FileAgent();
            Agents["OpSpawn"] = () => new ProcessAgent();
        }

        public IEnumerable<Result> Execute(string filename) => Execute(File.ReadAllLines(filename));


        public IEnumerable<Result> Execute(IEnumerable<string> script)
        {
            var operations = Translator.Translate(script);
            foreach (var op in operations)
            {
                var agentFactory = Agents[op.GetType().Name];
                var agent = agentFactory != null ? agentFactory() : NullAgent.Instance;
                yield return agent.Execute(op);
            }
            yield break;
        }
    }

}
