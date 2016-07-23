using System.Text;

namespace sasison.Expressions
{
    public class PropertyValueExpression : IExpression
    {
        public string Value { get; }

        public PropertyValueExpression(string value)
        {
            Value = value;
        }

        public void PrintOut(StringBuilder sb)
        {
            sb.Append(Value);
        }
    }
}