using sasison.Expressions;

namespace sasison.Parsers
{
    public class NameParser : ParserBase
    {
        public NameParser(SassParser context) : base(context)
        {
        }

        public override IExpression GetExpression()
        {
            return new NameExpression
            {
                new StringExpression(Context.GetValueAndClearToken())
            };
        }

        public override void Parse(char next)
        {
            if (next == Grammar.ColonChar)
            {
                Context.ReturnToParentParser(this);
                Context.Proceed(next);
                return;
            }

            if (next == Grammar.OpeningCurlyBraceChar)
            {
                Context.SwitchParser(new RuleParser(Context));
                Context.Proceed(next);
                return;
            }

            if (next == Grammar.EndDeclarationChar)
            {
                Context.ReturnToParentParser(this);
                Context.Proceed(next);
                return;
            }

            Context.Token.Accumulate(next);
        }
    }
}