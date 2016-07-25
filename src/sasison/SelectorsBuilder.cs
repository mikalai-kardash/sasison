using System.Collections.Generic;
using sasison.Expressions;

namespace sasison
{
    public class SelectorsBuilder
    {
        private List<SelectorExpression> _selectors = new List<SelectorExpression>();

        public SelectorsBuilder(params SelectorExpression[] selectors)
        {
            _selectors.AddRange(selectors);
        }

        public void AddParentSelectors(params SelectorExpression[] parentSelectors)
        {
            if (parentSelectors.Length == 0)
            {
                return;
            }

            var newSelectors = new List<SelectorExpression>();
            var space = Grammar.SpaceChar.ToString();

            foreach (var parent in parentSelectors)
            {
                foreach (var selector in _selectors)
                {
                    newSelectors.Add(new SelectorExpression(
                        string.Join(space, parent.Selector, selector.Selector)
                    ));
                }
            }

            _selectors = newSelectors;
        }

        public IEnumerable<SelectorExpression> Get()
        {
            return _selectors;
        }
    }
}