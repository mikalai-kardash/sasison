namespace sasison.Expressions
{
    public class VariableValueExpression : IExpression
    {
        public string VariableName { get; }

        public VariableValueExpression(string variableName)
        {
            VariableName = variableName;
        }

        public void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}