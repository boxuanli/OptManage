using System.Collections.Generic;
using System.Text;
namespace DbFrame.ExpressionTree
{

    using System.Linq.Expressions;
    using DbFrame.Class;
    using DbFrame.ExpressionTree.Class;

    public class ExpressionParser : IExpressionParser
    {

        public void Where(Expression _Expression, ParserArgs _ParserArgs)
        {
            new WhereParser(_Expression, _ParserArgs);
        }

        public void Select(LambdaExpression _LambdaExpression, StringBuilder Code, Dictionary<string, string> Alias)
        {
            new SelectParser(_LambdaExpression, Code, Alias);
        }

        public void JoinTable(LambdaExpression _LambdaExpression, StringBuilder Code, Dictionary<string, string> Alias, ParserArgs Param, string JoinStr, string JoinTabName)
        {
            new JoinTableParser(_LambdaExpression, Code, Alias, Param, JoinStr, JoinTabName);
        }

        public void OrderBy(LambdaExpression _LambdaExpression, StringBuilder Code, Dictionary<string, string> Alias)
        {
            new OrderByParser(_LambdaExpression, Code, Alias);
        }

        public void GroupBy(LambdaExpression _LambdaExpression, StringBuilder Code, Dictionary<string, string> Alias)
        {
            new GroupByParser(_LambdaExpression, Code, Alias);
        }

        public void UpdateSet(LambdaExpression _LambdaExpression, StringBuilder Code, ParserArgs _ParserArgs)
        {
            new UpdateSetParser(_LambdaExpression, Code, _ParserArgs);
        }

    }








}
