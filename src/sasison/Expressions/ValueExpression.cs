using System.Collections.Generic;

namespace sasison.Expressions
{
    public class ValueExpression : List<IExpression>, IExpression
    {
        public void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}