using System.Text;

namespace sasison.Expressions
{
    public class RuleExpression : IExpression
    {
        public RuleExpression(SelectorListExpression selectors, RuleBodyExpression body)
        {
            Selectors = selectors;
            Body = body;
        }

        public SelectorListExpression Selectors { get; }
        public RuleBodyExpression Body { get; }

        public void PrintOut(StringBuilder sb)
        {
            Selectors.PrintOut(sb);
            Body.PrintOut(sb);
        }
    }
}