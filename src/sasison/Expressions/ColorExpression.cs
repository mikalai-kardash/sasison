using System.Text;

namespace sasison.Expressions
{
    public class ColorExpression : IExpression
    {
        public short R { get; }
        public short G { get; }
        public short B { get; }
        public void PrintOut(StringBuilder sb)
        {
            throw new System.NotImplementedException();
        }
    }
}