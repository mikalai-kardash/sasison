using System;
using System.Collections.Generic;
using System.Linq;
using sasison.Expressions;
using sasison.Expressions.Arithmethics;

namespace sasison
{
    public class Tranclucator : IVisitor, IDisposable
    {
        private GlobalExpression _global;
        private Stack<RuleExpression> _scope;
        private IDictionary<string, string> _globalVariables;

        public void Visit(GlobalExpression globalExpression)
        {
            foreach (var expression in globalExpression)
            {
                expression.Accept(this);
            }
        }

        public void Visit(PropertyExpression property)
        {
            property.Name.Accept(this);
            property.Value.Accept(this);
        }

        public void Visit(RuleExpression rule)
        {
            AddRule(rule);

            _scope.Push(rule);

            rule.Selectors?.Accept(this);
            rule.Body?.Accept(this);

            _scope.Pop();
        }

        private void AddRule(RuleExpression rule)
        {
            var selectors = new SelectorListExpression();
            var body = new RuleBodyExpression();

            var sb = new SelectorsBuilder(rule.Selectors.ToArray());
            foreach (var ruleExpression in _scope)
            {
                sb.AddParentSelectors(ruleExpression.Selectors.ToArray());
            }

            selectors.AddRange(sb.Get());

            foreach (var expression in rule.Body)
            {
                if (expression.GetType() == typeof(RuleExpression))
                {
                    continue;
                }
                body.Add(expression);
            }

            Func<IExpression, bool> viableContent = e =>
            {
                var t = e.GetType();

                return t == typeof(PropertyExpression) || 
                       t == typeof(CommentsExpression);
            };

            if (body.Any(viableContent))
            {
                var indentation = _scope.Count(e => e.Body.Any(viableContent));
                var re = new RuleExpression(selectors, body) {Indentation = indentation};
                body.Indentation = indentation;
                _global.Add(re);
            }
        }

        public void Visit(RuleBodyExpression ruleBody)
        {
            foreach (var expression in ruleBody)
            {
                expression.Accept(this);
            }
        }

        public void Visit(SelectorListExpression ruleSelectors)
        {
            foreach (var expression in ruleSelectors)
            {
                expression.Accept(this);
            }
        }

        public void Visit(SelectorExpression selector)
        {
        }

        public void Visit(VariableValueExpression variableValue)
        {
        }

        public void Visit(VariableExpression variable)
        {
            variable.Name.Accept(this);
            variable.Value.Accept(this);

            _global.Add(variable);
        }

        public void Visit(NameExpression name)
        {
            foreach (var expression in name)
            {
                expression.Accept(this);
            }
        }

        public void Visit(ValueExpression value)
        {
            foreach (var expression in value)
            {
                expression.Accept(this);
            }
        }

        public void Visit(StringExpression str)
        {
        }

        public void Visit(CommentsExpression comments)
        {
            if (_scope.Count == 0)
            {
                _global.Add(comments);
            }
        }

        public void Visit(BackReferenceExpression backReference)
        {
            throw new NotImplementedException();
        }

        public void Visit(AtExpression at)
        {
            foreach (var expression in at)
            {
                expression.Accept(this);
            }
        }

        public void Visit(ImportExpression import)
        {
            
        }

        public void Visit(ArithmethicExpression arithmethicExpression)
        {
        }

        public IExpression Process(IExpression expression)
        {
            _scope = new Stack<RuleExpression>();
            _global = new GlobalExpression();
            _globalVariables = new Dictionary<string, string>();

            expression.Accept(this);

            return _global;
        }

        public void Dispose()
        {
            _scope?.Clear();
            _globalVariables?.Clear();

            _global = null;
            _scope = null;
            _globalVariables = null;
        }
    }
}