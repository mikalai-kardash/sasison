using System.Collections.Generic;
using System.Linq;

namespace sasison.Expressions
{
    public class Tranclucator : IVisitor
    {
        private GlobalExpression _global;
        private Stack<RuleExpression> _scope = new Stack<RuleExpression>();

        public void Visit(GlobalExpression globalExpression)
        {
            foreach (var expression in globalExpression)
            {
                expression.Accept(this);
            }
        }

        public void Visit(PropertyDeclarationExpression property)
        {
        }

        public void Visit(PropertyNameExpression propertyName)
        {
        }

        public void Visit(PropertyValueExpression propertyValue)
        {
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

            if (body.Any(e => e.GetType() == typeof(PropertyDeclarationExpression)))
            {
                _global.Add(new RuleExpression(selectors, body));
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

        public void Visit(SelectorExpression ruleSelector)
        {
        }

        public IExpression Process(IExpression expression)
        {
            _global = new GlobalExpression();
            expression.Accept(this);
            return _global;
        }
    }
}