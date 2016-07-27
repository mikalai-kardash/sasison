using sasison.Expressions;

namespace sasison.Parsers
{
    public class GlobalParser : ParserBase
    {
        public GlobalParser(SassParser context) : base(context)
        {
        }

        public override IExpression GetExpression()
        {
            return new GlobalExpression().Add(Expressions);
        }

        public override void Parse(char next)
        {
            if (next == Grammar.VarChar)
            {
                Context.SetParser(new VariableParser(Context));
                Context.Proceed(next);
                return;
            }

            if (next == Grammar.AtChar)
            {
                Context.SetParser(new AtParser(Context));
                Context.Proceed(next);
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

            Context.SetParser(new RuleParser(Context));
            Context.Proceed(next);
        }
    }
}