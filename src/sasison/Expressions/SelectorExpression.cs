using System.Text;

namespace sasison.Expressions
{
    public class SelectorExpression : IExpression
    {
        public string Selector { get; }

        public SelectorExpression(string selector)
        {
            Selector = selector;
        }

        public void PrintOut(StringBuilder sb)
        {
            sb.Append(Selector);
        }
    }
}