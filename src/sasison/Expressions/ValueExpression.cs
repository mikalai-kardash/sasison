using System.Collections.Generic;
using System.Text;

namespace sasison.Expressions
{
    public class ValueExpression : List<IExpression>, IExpression
    {
        public void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }

        // public IExpression Parent { get; }

        public void PrintOut(StringBuilder sb)
        {
            throw new System.NotImplementedException();
        }
    }
}