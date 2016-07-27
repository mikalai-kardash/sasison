using sasison.Expressions;

namespace sasison.Parsers
{
    public class ImportParser : ParserBase
    {
        public ImportParser(SassParser context) : base(context)
        {
        }

        public override IExpression GetExpression()
        {
            return new ImportExpression(Context.GetValueAndClearToken());
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
                next == Grammar.DoubelQuoteChar)
            {
                return;
            }

            Context.Token.Accumulate(next);
        }
    }
}