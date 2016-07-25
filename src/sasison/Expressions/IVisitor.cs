namespace sasison.Expressions
{
    public interface IVisitor
    {
        void Visit(GlobalExpression globalExpression);

        void Visit(PropertyDeclarationExpression property);
        void Visit(PropertyNameExpression propertyName);
        void Visit(PropertyValueExpression propertyValue);

        void Visit(RuleExpression rule);
        void Visit(RuleBodyExpression ruleBody);
        void Visit(SelectorListExpression ruleSelectors);
        void Visit(SelectorExpression ruleSelector);
    }
}