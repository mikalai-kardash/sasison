namespace sasison.Expressions
{
    public class SelectorExpression : IExpression
    {
        public string Selector { get; }

        public SelectorExpression(string selector)
        {
            Selector = selector;
        }

        public void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}