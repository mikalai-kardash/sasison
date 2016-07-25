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
            var selectors = Expressions.OfType<SelectorListExpression>().FirstOrDefault();
            var body = Expressions.OfType<RuleBodyExpression>().FirstOrDefault();
            return new RuleExpression(selectors, body);
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