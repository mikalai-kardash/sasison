using System.Collections.Generic;
using sasison.Expressions;

namespace sasison
{
    public static class ListExtensions
    {
        public static T Add<T>(this T expression, IEnumerable<IExpression> expressions)
            where T: List<IExpression>, IExpression
        {
            expression.AddRange(expressions);
            return expression;
        }
    }
}