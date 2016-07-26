using System.Collections.Generic;
using System.Text;

namespace sasison.Expressions
{
    public class NameExpression : List<IExpression>, IExpression
    {
        public void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }

        public void PrintOut(StringBuilder sb)
        {
        }
    }
}