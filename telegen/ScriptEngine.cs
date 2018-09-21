﻿using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using telegen.Agents;
using telegen.Agents.Interfaces;
using telegen.Interfaces;
using telegen.Messages;
using telegen.Results;

namespace telegen
{
    public class ScriptEngine : IScriptEngine
    {
        protected IDictionary<string, Func<IAgent>> AgentFactories;

        public ScriptEngine()
        {
            AgentFactories = new Dictionary<string, Func<IAgent>>
            {
                ["File"] = () => new FileAgent(),
                ["Process"] = () => new ProcessAgent(),
                ["Network"] = () => new NetworkAgent(),
                ["System"] =() => new SystemAgent()
            };
        }

        public IEnumerable<Result> ParseAndExecute(string jsonScript) => Execute(JsonConvert.DeserializeObject<IEnumerable<Operation>>(jsonScript));

        public IEnumerable<Result> Execute(string filename) => ParseAndExecute(File.ReadAllText(filename));

        public IEnumerable<Result> Execute(IEnumerable<Operation> script)
        {
            var opCount = 0;
            foreach (var op in script) {
                opCount++;
                if (AgentFactories.ContainsKey(op.Domain)) {
                    var agentFactory = AgentFactories[op.Domain];
                    var agent = agentFactory();
                    var result = agent.Execute(op);
                    if (result != null) 
                        yield return result;
                }
                else
                {
                    var ex = new Exception($"ScriptEngine.Execute:: Unknown operation domain '{op.Domain}' in requested action '{op.Action}', operation #{opCount}.");
                    ex.Data["Source"] = op.ToString();
                    throw ex;
                }

            }
            yield break;
        }

    }

}
