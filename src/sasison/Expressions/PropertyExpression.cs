using System.Text;

namespace sasison.Expressions
{
    public class PropertyExpression : IExpression
    {
        public PropertyExpression(NameExpression name, ValueExpression value)
        {
            Name = name;
            Value = value;
        }

        public NameExpression Name { get; }
        public ValueExpression Value { get; private set; }

        public void PrintOut(StringBuilder sb)
        {
            sb.Append(Grammar.SpaceChar);
            sb.Append(Grammar.SpaceChar);

            Name?.PrintOut(sb);

            sb.Append(Grammar.ColonChar);
            sb.Append(Grammar.SpaceChar);

            Value?.PrintOut(sb);

            sb.Append(Grammar.EndDeclarationChar);
            sb.Append(Grammar.SpaceChar);
        }

        public void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }

        public void SetValue(string value)
        {
            Value = new ValueExpression {new StringExpression(value)};
        }
    }
}