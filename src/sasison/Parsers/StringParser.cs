using sasison.Expressions;

namespace sasison.Parsers
{
    public class StringParser : ParserBase
    {
        public StringParser(SassParser context) : base(context)
        {
        }

        public override IExpression GetExpression()
        {
            return new StringExpression(Context.GetValueAndClearToken());
        }

        public override void Parse(char next)
        {
            if (IsSpaceOrTabOrNewLineOrReturn(next) || 
                next == Grammar.EndDeclarationChar || 
                next == Grammar.CommaChar ||
                next == Grammar.OpeningCurlyBraceChar)
            {
                Context.ReturnToParentParser(this);
                Context.Proceed(next);
                return;
            }

            Context.Token.Accumulate(next);
        }
    }
}