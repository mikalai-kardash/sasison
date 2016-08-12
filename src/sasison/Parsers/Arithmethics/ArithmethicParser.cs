using sasison.Expressions;
using sasison.Expressions.Arithmethics;

namespace sasison.Parsers.Arithmethics
{
    public class ArithmethicParser : ParserBase
    {
        public ArithmethicParser(SassParser context) : base(context)
        {
        }

        public override IExpression GetExpression()
        {
            return new ArithmethicExpression(Context.GetValueAndClearToken());
        }

        public override void Parse(char next)
        {
            Context.Token.Accumulate(next);
            Context.ReturnToParentParser(this);
        }
    }
}