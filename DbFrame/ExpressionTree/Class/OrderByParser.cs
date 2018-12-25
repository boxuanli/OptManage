using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace DbFrame.ExpressionTree.Class
{
    using System.Linq.Expressions;
    using DbFrame.Class;
    public class OrderByParser
    {

        public OrderByParser(LambdaExpression _LambdaExpression, StringBuilder Code, Dictionary<string, string> Alias)
        {
            if (_LambdaExpression.Body is ConstantExpression)//如果是字符串
            {
                var value = (_LambdaExpression.Body as ConstantExpression).Value.ToStr();
                if (string.IsNullOrEmpty(value)) throw new DbFrameException(" ORDER BY 参数不能为空字符串或者Null ");
                Code.Append(value);
            }
            else if (_LambdaExpression.Body.Type == typeof(string))//如果是字符串
            {
                var value = Parser.Eval(_LambdaExpression.Body).ToStr();
                if (string.IsNullOrEmpty(value)) throw new DbFrameException(" ORDER BY 参数不能为空字符串或者Null ");
                Code.Append(value);
            }
            else if (_LambdaExpression.Body is NewExpression)//如果是匿名对象
            {
                var body = (_LambdaExpression.Body as NewExpression);
                var values = body.Arguments;
                var member = body.Members;

                var column = new List<string>();
                var list_member = member.ToList();
                foreach (var item in values)
                {
                    if (item is MemberExpression)
                    {
                        var it = item as MemberExpression;
                        //检查是否有别名
                        var DisplayName = list_member[values.IndexOf(item)].Name;

                        var TabObj = _LambdaExpression.Parameters.Count > 1 ? (it.Expression as ParameterExpression).Name + "." : "";

                        if (DisplayName == it.Member.Name)
                            column.Add(TabObj + it.Member.Name);
                        else
                        {
                            if (!DisplayName.ToLower().Contains("asc") && !DisplayName.ToLower().Contains("desc"))
                                throw new DbFrameException("ORDER BY 语法错误 请使用 asc 或者 desc 关键字");
                            column.Add(TabObj + it.Member.Name + " " + (DisplayName.Contains("desc") ? "desc" : "asc"));
                        }
                    }
                    else if (item is ConstantExpression)
                    {
                        var it = item as ConstantExpression;
                        var val = it.Value.ToStr();
                        //检查是否有别名 ''
                        var DisplayName = list_member[values.IndexOf(item)].Name;
                        if (!string.IsNullOrEmpty(DisplayName) && DisplayName.StartsWith("SqlString"))//判断别名是否 有 SqlString 关键字
                        {
                            column.Add(val);
                        }
                    }
                    else if (item.Type == typeof(string))
                    {
                        //检查是否有别名 ''
                        var value = Parser.Eval(item).ToStr();
                        var DisplayName = list_member[values.IndexOf(item)].Name;
                        if (!string.IsNullOrEmpty(DisplayName) && DisplayName.StartsWith("SqlString"))
                        {
                            column.Add(value);
                        }
                    }
                }
                Code.Append(string.Join(",", column));
            }
            else
            {
                throw new DbFrameException("无法解析的表达式！");
            }
        }


























    }
}
