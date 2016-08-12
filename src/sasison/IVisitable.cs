namespace sasison
{
    public interface IVisitable
    {
        void Accept(IVisitor visitor);
    }
}