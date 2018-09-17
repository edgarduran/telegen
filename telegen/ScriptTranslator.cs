using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using telegen.Operations;
using telegen.Util;

namespace telegen
{
    public class ScriptTranslator : IScriptTranslator
    {
        protected IDictionary<string, Func<IParsedCommand, Operation>> Translators;

        public ScriptTranslator()
        {
            Translators = new Dictionary<string, Func<IParsedCommand, Operation>>();

            Translators["FILE.CREATE"] = cmd => new OpCreateFile(cmd.Parms[0]);
            Translators["FILE.APPEND"] = cmd => new OpUpdateFile(cmd.Parms[0], cmd.Parms[1]);
            Translators["FILE.DELETE"] = cmd => new OpDeleteFile(cmd.Parms[0]);
            Translators["NET.GET"] = cmd => new OpNetGet(cmd.Parms[0]);
            Translators["EXEC"] = cmd => new OpSpawn(cmd.Parms[0], cmd.Parms.Skip(1));

        }

        public IEnumerable<Operation> Translate(string filename) => Translate(File.ReadAllLines(filename));


        public IEnumerable<Operation> Translate(IEnumerable<string> script)
        {
            var parser = new CommandParser();
            foreach (var line in script)
            {
                var test = line.Trim();
                if (string.IsNullOrWhiteSpace(line) || line.Trim().StartsWith("#")) continue;

                var cmd = parser.Parse(line);
                var xlator = Translators[cmd.Command];
                var results = xlator != null ? xlator(cmd) : null; //todo replace this.;
                yield return results;
            }
            yield break;
        }
    }

}
