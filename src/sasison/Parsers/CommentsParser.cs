using sasison.Expressions;

namespace sasison.Parsers
{
    public class CommentsParser: ParserBase
    {
        private char _prevChar;
        private bool _hasBegun;
        private bool _isSingleLine;

        public CommentsParser(SassParser context) : base(context)
        {
        }

        public override IExpression GetExpression()
        {
            return new CommentsExpression(_isSingleLine)
            {
                new StringExpression(Context.GetValueAndClearToken().Trim(Grammar.StarChar))
            };
        }

        public override void Parse(char next)
        {
            if (!_hasBegun)
            {
                _hasBegun = _prevChar == Grammar.ForwardSlashChar &&
                           (next == Grammar.StarChar || next == Grammar.ForwardSlashChar);

                if (_hasBegun)
                {
                    _isSingleLine = next == Grammar.ForwardSlashChar;
                }
            }

            var ended = false;
            if (_hasBegun)
            {
                ended = (_prevChar == Grammar.StarChar && next == Grammar.ForwardSlashChar && !_isSingleLine) ||
                        (_isSingleLine && next == Grammar.NewLineChar);
            }
            
            if (ended)
            {
                Context.ReturnToParentParser(this);
                return;
            }

            if (_hasBegun)
            {
                Context.Token.Accumulate(next);
            }

            _prevChar = next;
        }
    }
}