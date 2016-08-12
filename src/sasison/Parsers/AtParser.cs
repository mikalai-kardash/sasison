using System;
using sasison.Expressions;

namespace sasison.Parsers
{
    public class AtParser : ParserBase
    {
        private const string ImportStatement = "import";

        public AtParser(SassParser context) : base(context)
        {
        }

        public override IExpression GetExpression()
        {
            return new AtExpression().Add(Expressions);
        }

        public override void Parse(char next)
        {
            if (next == Grammar.AtChar)
            {
                return;
            }

            if (next == Grammar.EndDeclarationChar)
            {
                Context.ReturnToParentParser(this);
                return;
            }

            if (ImportStatement.Equals(Context.Token.ToString(), StringComparison.OrdinalIgnoreCase))
            {
                Context.Token.Clear();
                Context.SetParser(new ImportParser(Context));
                return;
            }

            Context.Token.Accumulate(next);
        }
    }
}