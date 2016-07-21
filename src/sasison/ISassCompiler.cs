using System.Collections.Generic;

namespace sasison
{
    // todo: compilation on-the-fly
    // todo: variable overrides, additional chunks, etc.
    public interface ISassCompiler
    {
        string Compile(string input);
    }
}