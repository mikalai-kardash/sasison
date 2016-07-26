using sasison.Expressions;

namespace sasison.Parsers
{
    public class BackReferenceParser : ParserBase
    {
        public BackReferenceParser(SassParser context) : base(context)
        {
        }

        public override IExpression GetExpression()
        {
            return new BackReferenceExpression(Context.GetValueAndClearToken());
        }

        public override void Parse(char next)
        {
            if (IsSpaceOrTabOrNewLineOrReturn(next) ||
                next == Grammar.OpeningCurlyBraceChar ||
                next == Grammar.CommaChar)
            {
                Context.ReturnToParentParser(this);
                Context.Proceed(next);
                return;
            }

            if (next == Grammar.BackReferenceChar)
            {
                return;
            }

            Context.Token.Accumulate(next);
        }
    }
}