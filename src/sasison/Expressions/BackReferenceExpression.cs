namespace sasison.Expressions
{
    public class BackReferenceExpression : IExpression
    {
        public string Rest { get; }

        public BackReferenceExpression(string rest)
        {
            Rest = rest;
        }

        public SelectorExpression ParentExpression { get; set; }

        public void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}