using System.Collections.Generic;

namespace sasison.Expressions
{
    public class CommentsExpression : List<IExpression>, IExpression
    {
        public CommentsExpression(bool isSingleLine)
        {
            IsSingleLine = isSingleLine;
        }

        public bool IsSingleLine { get; }

        public void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}