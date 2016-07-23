using sasison.Expressions;
using System.Linq;

namespace sasison.Parsers
{
    public class RuleParser : ParserBase
    {
        public RuleParser(SassParser context) : base(context)
        {
        }

        public override IExpression GetExpression()
        {
            return new RuleExpression(
                Expressions.OfType<SelectorListExpression>().FirstOrDefault(),
                Expressions.OfType<RuleBodyExpression>().FirstOrDefault()
            );
        }

        public override void Parse(char next)
        {
            if (next == Grammar.OpeningCurlyBraceChar)
            {
                Context.SetParser(new RuleBodyParser(Context));
                return;
            }

            if (next == Grammar.ClosingCurlyBraceChar)
            {
                Context.ReturnToParentParser(this);
                return;
            }

            Context.SetParser(new SelectorListParser(Context));
            Context.Proceed(next);
        }
    }
}