using System;
using System.Collections.Generic;
using System.Text;

namespace sasison.Expressions
{
    public class GlobalExpression : List<IExpression>, IExpression
    {
        public void PrintOut(StringBuilder sb)
        {
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
        }

        public void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}