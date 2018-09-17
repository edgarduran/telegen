using System.Collections.Generic;
using telegen.Operations;

namespace telegen
{
    public interface IScriptTranslator
    {
        IEnumerable<Operation> Translate(string filename);
        IEnumerable<Operation> Translate(IEnumerable<string> script);
    }
}