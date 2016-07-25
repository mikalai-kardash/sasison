using System.Text;

namespace sasison.Expressions
{
    public class PropertyNameExpression : IExpression
    {
        public string Value { get; }

        public PropertyNameExpression(string value)
        {
            Value = value;
        }

        public void PrintOut(StringBuilder sb)
        {
            sb.Append(Value);
        }

        public void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}