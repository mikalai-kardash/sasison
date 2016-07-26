using System;
using System.Collections.Generic;
using sasison.Expressions;

namespace sasison
{
    public class ScopeMan : IDisposable
    {
        private static readonly Scope Null = new Scope(null);
        
        private readonly Stack<Scope> _scopes = new Stack<Scope>();

        public IDisposable NewScope(IExpression expression)
        {
            _scopes.Push(new Scope(expression));
            return new Scopera(this);
        }

        public Scope GetCurrent()
        {
            return _scopes.Count > 0 ? _scopes.Peek() : Null;
        }

        public void AddVariable(string name, string value)
        {
            var scope = GetCurrent();
            scope.AddOrSetVariable(name, value);
        }

        public string GetVariableValue(string variableName)
        {
            var scopes = _scopes.ToArray();
            for (var i = scopes.Length - 1; i >= 0; i--)
            {
                string v;
                if (scopes[i].TryGet(variableName, out v))
                {
                    return v;
                }
            }

            return string.Empty;
        }

        class Scopera : IDisposable
        {
            private readonly ScopeMan _scopeMan;

            public Scopera(ScopeMan scopeMan)
            {
                _scopeMan = scopeMan;
            }

            public void Dispose()
            {
                _scopeMan._scopes.Pop();
            }
        }

        public void Dispose()
        {
            _scopes.Clear();
        }
    }
}