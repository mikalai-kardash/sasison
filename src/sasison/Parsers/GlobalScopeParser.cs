using sasison.Expressions;

namespace sasison.Parsers
{
    public class GlobalScopeParser : ParserBase
    {
        public GlobalScopeParser(SassParser context) : base(context)
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
                Context.SetParser(new VariableDeclarationParser(Context));
                Context.Proceed(next);
                return;
            }

            if (next == Grammar.AtChar)
            {
                // import, media query, etc.
                return;
            }

            if (next == Grammar.NewLineChar 
                || next == Grammar.ReturnChar 
                || next == Grammar.SpaceChar 
                || next == Grammar.TabChar)
            {
                // skip
                return;
            }

            Context.SetParser(new RuleParser(Context));
            Context.Proceed(next);
        }
    }
}