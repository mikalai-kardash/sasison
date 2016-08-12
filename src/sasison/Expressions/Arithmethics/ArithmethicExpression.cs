namespace sasison.Expressions.Arithmethics
{
    public class ArithmethicExpression : IExpression
    {
        public string Expression { get; }

        public ArithmethicExpression(string expression)
        {
            Expression = expression;
        }

        public void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}