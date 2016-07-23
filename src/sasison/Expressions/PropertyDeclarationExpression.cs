using System.Text;

namespace sasison.Expressions
{
    public class PropertyDeclarationExpression : IExpression
    {
        public PropertyNameExpression Name { get; }
        public PropertyValueExpression Value { get; }

        public PropertyDeclarationExpression(PropertyNameExpression name, PropertyValueExpression value)
        {
            Name = name;
            Value = value;
        }

        public void PrintOut(StringBuilder sb)
        {
            sb.Append(Grammar.SpaceChar);
            sb.Append(Grammar.SpaceChar);

            Name.PrintOut(sb);

            sb.Append(Grammar.ColonChar);
            sb.Append(Grammar.SpaceChar);

            Value.PrintOut(sb);
            sb.Append(Grammar.EndDeclarationChar);
            sb.Append(Grammar.SpaceChar);
        }
    }
}