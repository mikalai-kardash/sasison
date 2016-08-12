using sasison.Expressions;
using sasison.Expressions.Arithmethics;

namespace sasison
{
    public interface IVisitor
    {
        void Visit(GlobalExpression globalExpression);
        void Visit(PropertyExpression property);
        void Visit(RuleExpression rule);
        void Visit(RuleBodyExpression ruleBody);
        void Visit(SelectorListExpression ruleSelectors);
        void Visit(SelectorExpression selector);
        void Visit(VariableValueExpression variableValue);
        void Visit(VariableExpression variable);
        void Visit(NameExpression name);
        void Visit(ValueExpression value);
        void Visit(StringExpression str);
        void Visit(CommentsExpression comments);
        void Visit(BackReferenceExpression backReference);
        void Visit(AtExpression at);
        void Visit(ImportExpression import);
        void Visit(ArithmethicExpression arithmethicExpression);
    }
}