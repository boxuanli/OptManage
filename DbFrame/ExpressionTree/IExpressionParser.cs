namespace DbFrame.ExpressionTree
{
    using DbFrame.Class;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Text;
    public interface IExpressionParser
    {

        void Where(Expression _Expression, ParserArgs _ParserArgs);

        void Select(LambdaExpression _LambdaExpression, StringBuilder Code, Dictionary<string, string> Alias);

        void JoinTable(LambdaExpression _LambdaExpression, StringBuilder Code, Dictionary<string, string> Alias, ParserArgs _ParserArgs, string JoinStr, string JoinTabName);

        void OrderBy(LambdaExpression _LambdaExpression, StringBuilder Code, Dictionary<string, string> Alias);

        void GroupBy(LambdaExpression _LambdaExpression, StringBuilder Code, Dictionary<string, string> Alias);

        void UpdateSet(LambdaExpression _LambdaExpression, StringBuilder Code, ParserArgs _ParserArgs);


    }
}
