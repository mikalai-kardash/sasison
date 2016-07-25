namespace sasison.Expressions
{
    public interface IVisitable
    {
        void Accept(IVisitor visitor);
    }
}