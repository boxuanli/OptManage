using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbFrame.ExpressionTree.Class
{
    using System.Linq.Expressions;
    using DbFrame.Class;
    public class UpdateSetParser
    {

        public UpdateSetParser(LambdaExpression _NewExpression, StringBuilder Code, ParserArgs _ParserArgs)
        {
            if (_NewExpression.Body is NewExpression)
            {
                var _Set = new List<object>();
                var body = _NewExpression.Body as NewExpression;
                var _Members = body.Members;
                for (int i = 0; i < body.Members.Count; i++)
                {
                    var _Member = body.Members[i];
                    var _Arguments = body.Arguments[i];
                    var _Count = _ParserArgs.SqlParameters.Count;
                    if (_Arguments is ConstantExpression)
                    {
                        if (_Member.Name.StartsWith("SqlString"))
                        {
                            var _ConstantExpression = _Arguments as ConstantExpression;
                            _Set.Add(_ConstantExpression.Value);
                        }
                        else
                        {
                            var _ConstantExpression = _Arguments as ConstantExpression;
                            var _Name = _Member.Name;
                            var _Value = _ConstantExpression.Value;
                            _Set.Add(_Name + "=@" + _Name + "_" + _Count);
                            _ParserArgs.SqlParameters.Add(_Name + "_" + _Count, _Value);
                        }
                    }
                    else if (_Arguments is MemberExpression)
                    {
                        var _MemberExpression = _Arguments as MemberExpression;
                        var _Name = _Member.Name;
                        var _Value = Parser.Eval(_MemberExpression).ToStr();
                        _Set.Add(_Name + "=@" + _Name + "_" + _Count);
                        _ParserArgs.SqlParameters.Add(_Name + "_" + _Count, _Value);
                    }
                    else if (_Arguments is BinaryExpression)
                    {
                        var _BinaryExpression = _Arguments as BinaryExpression;
                        if (_BinaryExpression.NodeType == ExpressionType.Add)
                        {
                            if (_BinaryExpression.Left is MemberExpression)
                            {
                                var _Left = _BinaryExpression.Left as MemberExpression;
                                var _Left_Name = _Left.Member.Name;
                                var _Name = _Member.Name;
                                var _Value = _Left_Name + "+" + Parser.Eval(_BinaryExpression.Right).ToStr();
                                _Set.Add(_Name + "=" + _Value);
                            }
                            else if (
                                _BinaryExpression.Left is ConstantExpression &&
                                _BinaryExpression.Left.Type == typeof(string) &&
                                _Member.Name.StartsWith("SqlString"))
                            {
                                var _Left = _BinaryExpression.Left as ConstantExpression;
                                var _Right_Value = Parser.Eval(_BinaryExpression.Right).ToStr();
                                var _Sql = _Left.Value.ToStr() + _Right_Value;
                                _Set.Add(_Sql);
                            }
                        }
                    }
                }
                Code.Append(string.Join(",", _Set));
            }
            else
            {
                throw new DbFrameException("无法解析的表达式！");
            }
        }






    }
}
