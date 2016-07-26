using System;
using System.Text;

namespace sasison.Expressions
{
    [Obsolete]
    public class VariableNameExpression : IExpression
    {
        public string Name { get; }

        public VariableNameExpression(string name)
        {
            Name = name;
        }

        public void Accept(IVisitor visitor)
        {
        }

        public void PrintOut(StringBuilder sb)
        {
        }
    }
}