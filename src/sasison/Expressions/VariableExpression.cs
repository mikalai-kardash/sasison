using System.Text;

namespace sasison.Expressions
{
    public class VariableExpression : IExpression
    {
        public NameExpression Name { get; }
        public ValueExpression Value { get; }

        public VariableExpression(NameExpression name, ValueExpression value)
        {
            Name = name;
            Value = value;
        }

        public void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }

        public void PrintOut(StringBuilder sb)
        {
        }
    }
}