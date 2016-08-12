using sasison.Expressions;
using sasison.Parsers.Arithmethics;

namespace sasison.Parsers
{
    public class ValueParser : ParserBase
    {
        public ValueParser(SassParser context) : base(context)
        {
        }

        public override IExpression GetExpression()
        {
            return new ValueExpression().Add(Expressions);
        }

        public override void Parse(char next)
        {
            if (next == Grammar.EndDeclarationChar)
            {
                Context.ReturnToParentParser(this);
                Context.Proceed(next);
                return;
            }

            if (IsSpaceOrTabOrNewLineOrReturn(next) || 
                next == Grammar.CommaChar)
            {
                return;
            }

            if (next == Grammar.VarChar)
            {
                Context.SetParser(new VariableValueParser(Context));
                Context.Proceed(next);
                return;
            }

            if (IsArithmethicOperation(next))
            {
                Context.SetParser(new ArithmethicParser(Context));
                Context.Proceed(next);
                return;
            }

            Context.SetParser(new StringParser(Context));
            Context.Proceed(next);
        }
    }
}