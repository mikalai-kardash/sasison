using sasison.Expressions;
using System.Linq;

namespace sasison.Parsers
{
    public class SelectorParser : ParserBase
    {
        public SelectorParser(SassParser context) : base(context)
        {
        }

        public override IExpression GetExpression()
        {
            //var value = Context.GetValueAndClearToken()
            //    .Replace("  ", Grammar.SpaceChar.ToString())
            //    .Replace("  ", Grammar.SpaceChar.ToString())
            //    .Replace(" =", Grammar.EqChar.ToString())
            //    .Replace(" =", Grammar.EqChar.ToString())
            //    .Replace("= ", Grammar.EqChar.ToString())
            //    .Replace("= ", Grammar.EqChar.ToString())
            //    .Replace("*= ", "*=")
            //    .Replace(" *=", "*=")
            //    .Replace(" ]", "]")
            //    .Replace("[ ", "[")
            //    ;

            //return new SelectorExpression(value);


            var selector = new SelectorExpression();
            //if (Expressions.All(e => e.GetType() != typeof(BackReferenceExpression)))
            //{
            //    selector.Add(new BackReferenceExpression(string.Empty));
            //}
            return selector.Add(Expressions);
        }

        public override void Parse(char next)
        {
            if (next == Grammar.CommaChar)
            {
                Context.ReturnToParentParser(this);
                //Context.SetParser(new SelectorParser(Context));
                return;
            }

            if (next == Grammar.OpeningCurlyBraceChar)
            {
                Context.ReturnToParentParser(this);
                Context.Proceed(next);
                return;
            }

            if (next == Grammar.BackReferenceChar)
            {
                Context.SetParser(new BackReferenceParser(Context));
                Context.Proceed(next);
                return;
            }

            if (IsSpaceOrTabOrNewLineOrReturn(next))
            {
                return;
            }

            Context.SetParser(new StringParser(Context));
            Context.Proceed(next);
        }
    }
}