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
            var selector = Context.Token.ToString();
            Context.Token.Clear();
            return new SelectorExpression(selector);
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