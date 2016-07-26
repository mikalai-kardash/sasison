using sasison.Expressions;

namespace sasison.Parsers
{
    public class VariableParser : ParserBase
    {
        public VariableParser(SassParser context) : base(context)
        {
        }

        public override IExpression GetExpression()
        {
            return new VariableExpression(
                GetExpression<NameExpression>(),
                GetExpression<ValueExpression>());
        }

        public override void Parse(char next)
        {
            if (next == Grammar.VarChar)
            {
                return;
            }

            if (next == Grammar.EndDeclarationChar)
            {
                Context.ReturnToParentParser(this);
                return;
            }

            if (next == Grammar.ColonChar)
            {
                Context.SetParser(new ValueParser(Context));
                return;
            }

            Context.SetParser(new NameParser(Context));
            Context.Proceed(next);
        }
    }
}