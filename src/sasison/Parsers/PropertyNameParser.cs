using sasison.Expressions;

namespace sasison.Parsers
{
    public class PropertyNameParser : ParserBase
    {
        public PropertyNameParser(SassParser context) : base(context)
        {
        }

        public override IExpression GetExpression()
        {
            return new PropertyNameExpression(Context.GetValueAndClearToken());
        }

        public override void Parse(char next)
        {
            if (next == Grammar.ColonChar)
            {
                Context.ReturnToParentParser(this);
                Context.Proceed(next);
                return;
            }

            Context.Token.Accumulate(next);
        }
    }
}