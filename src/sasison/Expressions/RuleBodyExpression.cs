using System;
using System.Collections.Generic;
using System.Text;

namespace sasison.Expressions
{
    public class RuleBodyExpression : List<IExpression>, IExpression
    {
        public void PrintOut(StringBuilder sb)
        {
            sb.Append(Grammar.OpeningCurlyBraceChar);
            sb.Append(Grammar.NewLineChar);

            foreach (var expression in this)
            {
                expression.PrintOut(sb);
            }

            sb.Append(Grammar.ClosingCurlyBraceChar);
            sb.Append(Grammar.NewLineChar);
        }
    }
}