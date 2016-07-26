namespace sasison.Expressions
{
    public class ColorExpression : IExpression
    {
        public short R { get; }
        public short G { get; }
        public short B { get; }

        public void Accept(IVisitor visitor)
        {
            throw new System.NotImplementedException();
        }
    }
}