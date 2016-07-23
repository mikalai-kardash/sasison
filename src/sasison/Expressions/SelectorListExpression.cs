using System;
using System.Collections.Generic;
using System.Text;

namespace sasison.Expressions
{
    public class SelectorListExpression : List<SelectorExpression>, IExpression
    {
        public void PrintOut(StringBuilder sb)
        {
            foreach (var expression in this)
            {
                expression.PrintOut(sb);
            }
        }
    }
}