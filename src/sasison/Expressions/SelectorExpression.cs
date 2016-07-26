using System.Collections.Generic;

namespace sasison.Expressions
{
    public class SelectorExpression : List<IExpression>, IExpression
    {
        public void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}