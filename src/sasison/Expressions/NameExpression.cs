using System.Collections.Generic;

namespace sasison.Expressions
{
    public class NameExpression : List<IExpression>, IExpression
    {
        public void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}