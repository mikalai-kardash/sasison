using System.Collections.Generic;

namespace sasison.Expressions
{
    public class AtExpression: List<IExpression>, IExpression
    {
        public void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}