namespace sasison.Expressions
{
    public class ImportExpression : IExpression
    {
        public string FileName { get; }

        public ImportExpression(string fileName)
        {
            FileName = fileName;
        }

        public void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}