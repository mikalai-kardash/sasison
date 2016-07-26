using System;
using System.Collections.Generic;
using System.Linq;
using sasison.Expressions;

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

            //if (property.Value != null && property.Value.IsVariable)
            //{
            //    string v;
            //    if (_globalVariables.TryGetValue(property.Value.Value.Substring(1), out v))
            //    {
            //        property.SetValue(v);
            //    }
            //}
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

            if (body.Any(e => e.GetType() == typeof(PropertyExpression)))
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

        public void Visit(VariableValueExpression variableValue)
        {
        }

        public void Visit(VariableExpression variable)
        {
            variable.Name.Accept(this);
            variable.Value.Accept(this);

            _global.Add(variable);

            //if (variable.Name != null && variable.Value != null)
            //{
            //    string oldValue;
            //    if (_globalVariables.TryGetValue(variable.Name.Name, out oldValue))
            //    {
            //        _globalVariables[variable.Name.Name] = variable.Value.Value;
            //    }
            //    else
            //    {
            //        _globalVariables.Add(variable.Name.Name, variable.Value.Value);
            //    }
            //}
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
            _scope.Clear();
            _globalVariables.Clear();

            _global = null;
            _scope = null;
            _globalVariables = null;
        }
    }
}