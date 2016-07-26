using System.Text;

namespace sasison.Expressions
{
    public interface IExpression : IVisitable
    {
        // IExpression Parent { get; }

        void PrintOut(StringBuilder sb);
    }
}