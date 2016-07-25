using System;
using System.Collections.Generic;
using System.Text;

namespace sasison.Expressions
{
    public class SelectorListExpression : List<SelectorExpression>, IExpression
    {
        public void PrintOut(StringBuilder sb)
        {
            var several = false;
            foreach (var expression in this)
            {
                if (several)
                {
                    sb.Append(Grammar.CommaChar);
                    sb.Append(Grammar.SpaceChar);
                }

                expression.PrintOut(sb);
                several = true;
            }
            sb.Append(Grammar.SpaceChar);
        }

        public void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}