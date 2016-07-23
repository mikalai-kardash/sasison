using sasison.Expressions;
using System.Linq;

namespace sasison.Parsers
{
    public class PropertyDeclarationParser : ParserBase
    {
        public PropertyDeclarationParser(SassParser context) : base(context)
        {
        }

        public override IExpression GetExpression()
        {
            return new PropertyDeclarationExpression(
                Expressions.OfType<PropertyNameExpression>().FirstOrDefault(),
                Expressions.OfType<PropertyValueExpression>().FirstOrDefault()
            );
        }

        public override void Parse(char next)
        {
            if (next == Grammar.ColonChar)
            {
                Context.SetParser(new PropertyValueParser(Context));
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

            Context.SetParser(new PropertyNameParser(Context));
            Context.Proceed(next);
        }
    }
}