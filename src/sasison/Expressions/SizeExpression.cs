namespace sasison.Expressions
{
    public class SizeExpression : IExpression
    {
        public float Size { get; }
        public SizeUnits Unit { get; }

        public SizeExpression(float size, SizeUnits unit)
        {
            Size = size;
            Unit = unit;
        }

        public void Accept(IVisitor visitor)
        {
            throw new System.NotImplementedException();
        }
    }
}