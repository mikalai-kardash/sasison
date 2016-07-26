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
        private bool _lastRuleWasIndented;

        public void Visit(GlobalExpression globalExpression)
        {
            using (_sm.NewScope(globalExpression))
            {
                foreach (var expression in globalExpression)
                {
                    expression.Accept(this);
                }
            }
        }

        public void Visit(PropertyExpression property)
        {
            _isPropertyScope = true;

            _poem.Append(Grammar.NewLineChar);

            var body = _sm.GetCurrent().Expression as RuleBodyExpression;
            if (body != null)
            {
                AddIndentation(body.Indentation);
            }

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
            if (rule.Indentation == 0)
            {
                _poem.Append(Grammar.NewLineChar);
            }
            else
            {
                AddIndentation(rule.Indentation);
            }
            
            rule.Selectors.Accept(this);
            rule.Body.Accept(this);

            _lastRuleWasIndented = rule.Indentation > 0;
        }

        public void Visit(RuleBodyExpression ruleBody)
        {
            _poem.Append(Grammar.OpeningCurlyBraceChar);

            using (_sm.NewScope(ruleBody))
            {
                foreach (var expression in ruleBody)
                {
                    expression.Accept(this);
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

            _poem.Append(Grammar.SpaceChar);
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

        private string GetVariableName(NameExpression name)
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

        private string GetVariableValue(ValueExpression value)
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

        public void Visit(CommentsExpression comments)
        {
            if (comments.IsSingleLine)
            {
                return;
            }

            if (_sm.GetCurrent().Expression.GetType() == typeof(GlobalExpression))
            {
                if (_lastRuleWasIndented)
                {
                    _poem.Append(Grammar.NewLineChar);
                }
            }
            else
            {
                _poem.Append(Grammar.NewLineChar);
            }

            var body = _sm.GetCurrent().Expression as RuleBodyExpression;
            if (body != null)
            {
                AddIndentation(body.Indentation + 1);
            }

            _poem.Append(Grammar.ForwardSlashChar);
            _poem.Append(Grammar.StarChar);

            _poem.Append(GetCommentsValue(comments));

            _poem.Append(Grammar.StarChar);
            _poem.Append(Grammar.ForwardSlashChar);
        }

        private static string GetCommentsValue(CommentsExpression comments)
        {
            var c = new StringBuilder();
            foreach (var comment in comments)
            {
                var str = comment as StringExpression;
                if (str != null)
                {
                    c.Append(str.Value);
                }
            }
            return c.ToString();
        }

        public string Write(IExpression expression)
        {
            _poem = new StringBuilder();
            _sm = new ScopeMan();

            expression.Accept(this);
            var text = _poem.ToString();

            return text.TrimStart(Grammar.NewLineChar);
        }

        public void Dispose()
        {
            _sm?.Dispose();
            _poem?.Clear();
        }

        private void AddIndentation(int i)
        {
            for (var j = 0; j < i * 2; j++)
            {
                _poem.Append(Grammar.SpaceChar);
            }
        }
    }
}