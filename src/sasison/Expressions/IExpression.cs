using System.Text;

namespace sasison.Expressions
{
    public interface IExpression : IVisitable
    {
        void PrintOut(StringBuilder sb);
    }
}