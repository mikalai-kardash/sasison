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

            if (next == Grammar.AtChar)
            {
                Context.SetParser(new AtParser(Context));
                Context.Proceed(next);
                return;
            }

            if (next == Grammar.VarChar)
            {
                Context.SetParser(new VariableParser(Context));
                return;
            }

            if (next == Grammar.ForwardSlashChar)
            {
                Context.SetParser(new CommentsParser(Context));
                Context.Proceed(next);
                return;
            }

            if (IsSpaceOrTabOrNewLineOrReturn(next))
            {
                // skip
                return;
            }

            Context.SetParser(new PropertyParser(Context));
            Context.Proceed(next);
        }
    }
}