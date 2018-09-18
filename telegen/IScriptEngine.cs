using System.Collections.Generic;
using telegen.Results;

namespace telegen
{
    public interface IScriptEngine
    {
        IEnumerable<Result> Execute(string filename);
        IEnumerable<Result> Execute(IEnumerable<string> script);
    }
}