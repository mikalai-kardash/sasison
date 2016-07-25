using sasison.Expressions;

namespace sasison.Parsers
{
    public class RuleBodyParser : ParserBase
    {
        public RuleBodyParser(SassParser context) : base(context)
        {
        }

        public override IExpression GetExpression()
        {
            return new RuleBodyExpression().Add(Expressions);
        }

        public override void Parse(char next)
        {
            if (next == Grammar.ClosingCurlyBraceChar)
            {
                Context.ReturnToParentParser(this);
                Context.Proceed(next);
                return;
            }

            if (next == Grammar.VarChar)
            {
                Context.SetParser(new VariableDeclarationParser(Context));
                return;
            }

            if (IsSpaceOrTabOrNewLineOrReturn(next))
            {
                // skip
                return;
            }

            Context.SetParser(new PropertyDeclarationParser(Context));
            Context.Proceed(next);
        }
    }
}