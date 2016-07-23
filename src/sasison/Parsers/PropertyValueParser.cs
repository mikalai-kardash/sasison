using sasison.Expressions;

namespace sasison.Parsers
{
    public class PropertyValueParser : ParserBase
    {
        public PropertyValueParser(SassParser context) : base(context)
        {
        }

        public override IExpression GetExpression()
        {
            return new PropertyValueExpression(Context.GetValueAndClearToken());
        }

        public override void Parse(char next)
        {
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