using System;
using System.Text;

namespace sasison.Expressions
{
    [Obsolete]
    public class PropertyValueExpression : IExpression
    {
        public string Value { get; }

        public bool IsVariable => Value[0] == Grammar.VarChar;

        public PropertyValueExpression(string value)
        {
            Value = value;
        }

        public void PrintOut(StringBuilder sb)
        {
            sb.Append(Value);
        }

        public void Accept(IVisitor visitor)
        {

        }
    }
}