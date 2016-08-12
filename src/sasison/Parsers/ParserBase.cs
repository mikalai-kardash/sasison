using System;
using System.Collections.Generic;
using sasison.Expressions;
using System.Linq;

namespace sasison.Parsers
{
    public abstract class ParserBase : IDisposable
    {
        private readonly List<IExpression> _expressions = new List<IExpression>();

        protected SassParser Context { get; }

        protected IEnumerable<IExpression> Expressions => _expressions;

        protected ParserBase(SassParser context)
        {
            Context = context;
        }

        public abstract IExpression GetExpression();

        public abstract void Parse(char next);

        public void AddChildExpression(IExpression expression)
        {
            _expressions.Add(expression);
        }

        public bool IsSpaceOrTabOrNewLineOrReturn(char next)
        {
            return next == Grammar.SpaceChar ||
                   next == Grammar.NewLineChar ||
                   next == Grammar.ReturnChar ||
                   next == Grammar.TabChar;
        }

        public bool IsArithmethicOperation(char next)
        {
            return next == Grammar.PlusChar ||
                   next == Grammar.MinusChar ||
                   next == Grammar.MultiplyChar ||
                   next == Grammar.DivideChar ||
                   next == Grammar.OpeningBraceChar ||
                   next == Grammar.ClosingBraceChar;
        }

        public T GetExpression<T>() where T : IExpression
        {
            return Expressions.OfType<T>().FirstOrDefault();
        }

        public void Dispose()
        {
            _expressions.Clear();
        }
    }
}