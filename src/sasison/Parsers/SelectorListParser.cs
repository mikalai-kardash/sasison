using System.Linq;
using sasison.Expressions;

namespace sasison.Parsers
{
    public class SelectorListParser : ParserBase
    {
        public SelectorListParser(SassParser context) : base(context)
        {
        }

        public override IExpression GetExpression()
        {
            var list = new SelectorListExpression();
            list.AddRange(Expressions.OfType<SelectorExpression>());
            return list;
        }

        public override void Parse(char next)
        {
            if (IsSpaceOrTabOrNewLineOrReturn(next))
            {
                // skip all irrelevant chars
                return;
            }

            if (next == Grammar.OpeningCurlyBraceChar)
            {
                Context.ReturnToParentParser(this);
                Context.Proceed(next);
                return;
            }


            Context.SetParser(new SelectorParser(Context));
            Context.Proceed(next);
        }
    }
}