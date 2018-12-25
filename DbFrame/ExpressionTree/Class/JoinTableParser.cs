using System;
using System.Collections.Generic;
using System.Text;
namespace DbFrame.ExpressionTree.Class
{
    using System.Linq.Expressions;
    using DbFrame.Class;

    public class JoinTableParser
    {


        public JoinTableParser(LambdaExpression _LambdaExpression, StringBuilder Code, Dictionary<string, string> Alias, ParserArgs _ParserArgs, string JoinStr, string JoinTabName)
        {
            if (!Alias.ContainsKey(JoinTabName)) throw new DbFrameException("链接表 别名未找到！");

            if (_LambdaExpression.Body is BinaryExpression)
            {
                var body = (_LambdaExpression.Body as BinaryExpression);
                Code.Append(" " + JoinStr + " ");
                var ByName = JoinTabName;
                var TabName = Alias[ByName] + " AS " + ByName;
                Code.Append(" " + TabName + " ON ");
                Parser.Where(_LambdaExpression, _ParserArgs);
                Code.Append(_ParserArgs.Builder);
            }
        }









    }
}
