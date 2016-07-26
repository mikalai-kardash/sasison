using System;
using System.Collections.Generic;
using System.Text;

namespace sasison.Expressions
{
    public class RuleBodyExpression : List<IExpression>, IExpression
    {
        public int Indentation { get; set; }

        public void PrintOut(StringBuilder sb)
        {
            sb.Append(Grammar.OpeningCurlyBraceChar);
            sb.Append(Grammar.NewLineChar);

            var several = false;
            foreach (var expression in this)
            {
                if (several)
                {
                    sb.Append(Grammar.NewLineChar);
                }
                expression.PrintOut(sb);
                several = true;
            }

            sb.Append(Grammar.ClosingCurlyBraceChar);
            sb.Append(Grammar.NewLineChar);
        }

        public void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}