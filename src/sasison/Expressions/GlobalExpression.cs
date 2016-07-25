using System;
using System.Collections.Generic;
using System.Text;

namespace sasison.Expressions
{
    public class GlobalExpression : List<IExpression>, IExpression
    {
        public void PrintOut(StringBuilder sb)
        {
            foreach (var expression in this)
            {
                expression.PrintOut(sb);
            }
        }

        public void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}