using System;
using System.Collections.Generic;
using sasison.Expressions;
using sasison.Parsers;

namespace sasison
{
    public class SassParser : ISassParser, IDisposable
    {
        private readonly IFileLoader _fileLoader;
        private readonly Stack<ParserBase> _scopes;
        private ParserBase _currentParser;

        public SassParser(IFileLoader fileLoader)
        {
            _fileLoader = fileLoader;
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
                SetParser(new GlobalParser(this));
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
            var expression = child.GetExpression();

            _currentParser = _scopes.Pop();
            _currentParser.AddChildExpression(expression);

            var import = expression as ImportExpression;
            if (import != null)
            {
                LoadFile(import.FileName);
            }
        }

        private void LoadFile(string fileName)
        {
            var contents = _fileLoader.LoadFile(fileName);
            //foreach (var next in contents)
            //{
            //    Proceed(next);
            //}
        }

        public void Dispose()
        {
            _scopes.Clear();
            _currentParser = null;
        }
    }
}