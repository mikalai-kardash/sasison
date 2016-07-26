using sasison.Expressions;

namespace sasison.Parsers
{
    public class SelectorParser : ParserBase
    {
        public SelectorParser(SassParser context) : base(context)
        {
        }

        public override IExpression GetExpression()
        {
            return new SelectorExpression(Context.GetValueAndClearToken());
        }

        public override void Parse(char next)
        {
            if (next == Grammar.CommaChar)
            {
                Context.ReturnToParentParser(this);
                Context.SetParser(new SelectorParser(Context));
                return;
            }

            if (next == Grammar.OpeningCurlyBraceChar)
            {
                Context.ReturnToParentParser(this);
                Context.Proceed(next);
                return;
            }

            Context.Token.Accumulate(next);
        }
    }
}