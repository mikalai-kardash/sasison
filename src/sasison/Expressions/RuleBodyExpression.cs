using System;
using System.Collections.Generic;
using System.Text;

namespace sasison.Expressions
{
    public class RuleBodyExpression : List<IExpression>, IExpression
    {
        public int Indentation { get; set; }

        public void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}