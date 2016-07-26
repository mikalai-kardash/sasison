using System;
using System.Collections.Generic;
using sasison.Expressions;
using sasison.Parsers;

namespace sasison
{
    public class SassParser : ISassParser, IDisposable
    {
        private readonly Stack<ParserBase> _scopes;
        private ParserBase _currentParser;

        public SassParser()
        {
            Token = new Token();
            _scopes = new Stack<ParserBase>();
        }

        public void SetParser(ParserBase parser)
        {
            if (_currentParser != null)
            {
                _scopes.Push(_currentParser);
            }
            _currentParser = parser;
        }

        public void SwitchParser(ParserBase parser) {
            _scopes.Pop();
            _currentParser = parser;

            var pending = GetValueAndClearToken();
            foreach (char next in pending)
            {
                Proceed(next);
            }
        }

        public void Proceed(char next)
        {
            _currentParser.Parse(next);
        }

        public Token Token { get; }

        public string GetValueAndClearToken()
        {
            var s = Token.ToString();
            Token.Clear();
            return s.Trim(Grammar.SpaceChar, Grammar.NewLineChar, Grammar.ReturnChar, Grammar.TabChar);
        }

        public IExpression Parse(string input)
        {
            try
            {
                SetParser(new GlobalScopeParser(this));
                foreach (var next in input)
                {
                    Proceed(next);
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Oops!", ex);
            }

            return _currentParser.GetExpression();
        }

        public void ReturnToParentParser(ParserBase child)
        {
            _currentParser = _scopes.Pop();
            _currentParser.AddChildExpression(child.GetExpression());
        }

        public void Dispose()
        {
            _scopes.Clear();
            _currentParser = null;
        }
    }
}