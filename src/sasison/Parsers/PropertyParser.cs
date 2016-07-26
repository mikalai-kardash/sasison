using sasison.Expressions;
using System.Linq;

namespace sasison.Parsers
{
    public class PropertyParser : ParserBase
    {
        public PropertyParser(SassParser context) : base(context)
        {
        }

        public override IExpression GetExpression()
        {
            return new PropertyExpression(
                Expressions.OfType<NameExpression>().FirstOrDefault(),
                Expressions.OfType<ValueExpression>().FirstOrDefault()
            );
        }

        public override void Parse(char next)
        {
            if (next == Grammar.ColonChar)
            {
                Context.SetParser(new ValueParser(Context));
                return;
            }

            if (next == Grammar.EndDeclarationChar)
            {
                Context.ReturnToParentParser(this);
                return;
            }

            if (IsSpaceOrTabOrNewLineOrReturn(next))
            {
                // skip all irrelevant chars
                return;
            }

            Context.SetParser(new NameParser(Context));
            Context.Proceed(next);
        }
    }
}