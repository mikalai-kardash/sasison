using System.Collections.Generic;
using sasison.Expressions;

namespace sasison
{
    public class Scope
    {
        private readonly IDictionary<string, string> _variables = new Dictionary<string, string>();

        public Scope(IExpression expression)
        {
            Expression = expression;
        }

        public bool TryGet(string key, out string value)
        {
            return _variables.TryGetValue(key, out value);
        }

        public void AddOrSetVariable(string variableName, string value)
        {
            string v;
            if (TryGet(variableName, out v))
            {
                _variables[variableName] = value;
            }
            else
            {
                _variables.Add(variableName, value);
            }
        }

        public IExpression Expression { get; }
    }
}