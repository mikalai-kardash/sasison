using System.Collections.Generic;
using sasison.Expressions;
using System.Linq;

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
            foreach (var parent in parentSelectors)
            {
                foreach (var selector in _selectors)
                {
                    var ns = new SelectorExpression();

                    var addedBackReference = false;
                    foreach (var expression in selector)
                    {
                        var br = expression as BackReferenceExpression;
                        if (br != null && br.ParentExpression == null)
                        {
                            addedBackReference = true;
                            ns.Add(new BackReferenceExpression(br.Rest)
                            {
                                ParentExpression = new SelectorExpression().Add(parent.ToList())
                            });
                            continue;
                        }

                        ns.Add(expression);
                    }

                    if (!addedBackReference)
                    {
                        ns.Insert(0, new BackReferenceExpression(string.Empty)
                        {
                            ParentExpression = new SelectorExpression().Add(parent.ToList())
                        });
                    }

                    newSelectors.Add(ns);
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