using sasison.Expressions;

namespace sasison.Parsers
{
    public class VariableValueParser : ParserBase
    {
        public VariableValueParser(SassParser context) : base(context)
        {
        }

        public override IExpression GetExpression()
        {
            return new VariableValueExpression(Context.GetValueAndClearToken());
        }

        public override void Parse(char next)
        {
            if (next == Grammar.VarChar)
            {
                return;
            }

            if (next == Grammar.EndDeclarationChar || next == Grammar.SpaceChar)
            {
                Context.ReturnToParentParser(this);
                Context.Proceed(next);
                return;
            }

            Context.Token.Accumulate(next);
        }
    }
}