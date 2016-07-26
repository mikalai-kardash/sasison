using System.Collections.Generic;

namespace sasison.Expressions
{
    public class SelectorListExpression : List<SelectorExpression>, IExpression
    {
        public void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}