using System.Collections.Generic;

namespace sasison
{
    public class Boundary
    {
        private readonly IDictionary<string, string> _boundary = new Dictionary<string, string>
        {
            { "{", "}" },
            { "#{", "}" },
        };
    }
}