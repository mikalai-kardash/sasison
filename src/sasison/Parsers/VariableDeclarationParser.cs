using sasison.Expressions;

namespace sasison.Parsers
{
    public class VariableDeclarationParser : ParserBase
    {
        public VariableDeclarationParser(SassParser context) : base(context)
        {
        }

        public override IExpression GetExpression()
        {
            throw new System.NotImplementedException();
        }

        public override void Parse(char next)
        {
            if (next == Grammar.VarChar)
            {
                // skip
                return;
            }

            if (next == Grammar.EndDeclarationChar)
            {
                Context.ReturnToParentParser(this);
            }


        }
    }
}