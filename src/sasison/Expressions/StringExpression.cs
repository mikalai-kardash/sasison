using System.Text;

namespace sasison.Expressions
{
    public class StringExpression : IExpression
    {
        public StringExpression(string value)
        {
            Value = value;
        }

        public string Value { get; }

        public void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }

        public void PrintOut(StringBuilder sb)
        {
            throw new System.NotImplementedException();
        }
    }
}