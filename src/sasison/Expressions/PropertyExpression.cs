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