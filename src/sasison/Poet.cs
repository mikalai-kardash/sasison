using System;
using System.Text;
using sasison.Expressions;

namespace sasison
{
    public class Poet : IVisitor, IDisposable
    {
        private StringBuilder _poem;
        private bool _isPropertyScope;
        private ScopeMan _sm;

        public void Visit(GlobalExpression globalExpression)
        {
            using (_sm.NewScope(globalExpression))
            {
                var several = false;
                foreach (var expression in globalExpression)
                {
                    if (several && expression.GetType() != typeof(VariableExpression))
                    {
                        _poem.Append(Grammar.NewLineChar);
                    }

                    expression.Accept(this);
                    several = several || (expression.GetType() != typeof(VariableExpression));
                }
            }
        }

        public void Visit(PropertyExpression property)
        {
            _isPropertyScope = true;

            _poem.Append(Grammar.SpaceChar);
            _poem.Append(Grammar.SpaceChar);

            property.Name.Accept(this);

            _poem.Append(Grammar.ColonChar);
            _poem.Append(Grammar.SpaceChar);

            property.Value.Accept(this);

            _poem.Append(Grammar.EndDeclarationChar);

            _isPropertyScope = false;
        }

        public void Visit(RuleExpression rule)
        {
            rule.Selectors.Accept(this);

            _poem.Append(Grammar.SpaceChar);

            rule.Body.Accept(this);
        }

        public void Visit(RuleBodyExpression ruleBody)
        {
            _poem.Append(Grammar.OpeningCurlyBraceChar);
            _poem.Append(Grammar.NewLineChar);

            using (_sm.NewScope(ruleBody))
            {
                var several = false;
                foreach (var expression in ruleBody)
                {
                    if (several)
                    {
                        _poem.Append(Grammar.NewLineChar);
                    }

                    expression.Accept(this);
                    several = true;
                }
            }

            _poem.Append(Grammar.SpaceChar);
            _poem.Append(Grammar.ClosingCurlyBraceChar);
            _poem.Append(Grammar.NewLineChar);
        }

        public void Visit(SelectorListExpression ruleSelectors)
        {
            var several = false;
            foreach (var selector in ruleSelectors)
            {
                if (several)
                {
                    _poem.Append(Grammar.CommaChar);
                    _poem.Append(Grammar.SpaceChar);
                }

                selector.Accept(this);
                several = true;
            }
        }

        public void Visit(SelectorExpression ruleSelector)
        {
            _poem.Append(ruleSelector.Selector);
        }

        public void Visit(VariableValueExpression variableValue)
        {
            if (_isPropertyScope)
            {
                _poem.Append(_sm.GetVariableValue(variableValue.VariableName));
            }
        }

        public void Visit(VariableExpression variable)
        {
            variable.Name.Accept(this);
            variable.Value.Accept(this);

            _sm.AddVariable(
                GetVariableName(variable.Name), 
                GetVariableValue(variable.Value));
        }

        public string GetVariableName(NameExpression name)
        {
            var sb = new StringBuilder();
            foreach (var expression in name)
            {
                var e = expression as StringExpression;
                if (e != null)
                {
                    sb.Append(e.Value);
                }
            }
            return sb.ToString();
        }

        public string GetVariableValue(ValueExpression value)
        {
            var sb = new StringBuilder();
            var several = false;
            foreach (var expression in value)
            {
                var str = expression as StringExpression;
                if (str != null)
                {
                    if (several)
                    {
                        sb.Append(Grammar.SpaceChar);
                    }

                    sb.Append(str.Value);
                    several = true;
                    continue;
                }

                var v = expression as VariableValueExpression;
                if (v != null)
                {
                    if (several)
                    {
                        sb.Append(Grammar.SpaceChar);
                    }

                    sb.Append(_sm.GetVariableValue(v.VariableName));
                    several = true;
                }
            }
            return sb.ToString();
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
            var several = false;
            foreach (var expression in value)
            {
                if (several && _isPropertyScope)
                {
                    _poem.Append(Grammar.SpaceChar);
                }

                expression.Accept(this);
                several = true;
            }
        }

        public void Visit(StringExpression str)
        {
            if (_isPropertyScope)
            {
                _poem.Append(str.Value);
            }
        }

        public string Write(IExpression expression)
        {
            _poem = new StringBuilder();
            _sm = new ScopeMan();

            expression.Accept(this);
            return _poem.ToString();
        }

        public void Dispose()
        {
            _sm?.Dispose();
            _poem?.Clear();
        }
    }
}