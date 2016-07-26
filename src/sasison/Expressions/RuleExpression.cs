namespace sasison.Expressions
{
    public class RuleExpression : IExpression
    {
        public RuleExpression(SelectorListExpression selectors, RuleBodyExpression body)
        {
            Selectors = selectors;
            Body = body;
        }

        public int Indentation { get; set; }

        public SelectorListExpression Selectors { get; }
        public RuleBodyExpression Body { get; }
        
        public void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}