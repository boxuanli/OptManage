using System;
using System.Collections.Generic;
using System.Text;
namespace DbFrame.ExpressionTree.Class
{
    using System.Linq.Expressions;
    using DbFrame.Class;
    using System.Collections;
    public class WhereParser
    {

        public WhereParser(Expression _Expression, ParserArgs _ParserArgs)
        {
            if (_Expression is LambdaExpression)
            {
                new WhereParser((_Expression as LambdaExpression).Body, _ParserArgs);
            }
            else if (_Expression is BinaryExpression)
            {
                this.DealBinaryExpression(_Expression as BinaryExpression, _ParserArgs);
            }
            else if (_Expression is MemberExpression)
            {
                this.DealMemberExpression(_Expression as MemberExpression, _ParserArgs);
            }
            else if (_Expression is ConstantExpression)
            {
                this.DealConstantExpression(_Expression as ConstantExpression, _ParserArgs);
            }
            else if (_Expression is UnaryExpression)
            {
                this.DealUnaryExpression(_Expression as UnaryExpression, _ParserArgs);
            }
            else if (_Expression is ParameterExpression)
            {
                this.DealParameterExpression(_Expression as ParameterExpression, _ParserArgs);
            }
            else if (_Expression is NewArrayExpression)
            {
                this.DealNewArrayExpression(_Expression as NewArrayExpression, _ParserArgs);
            }
            else if (_Expression is MethodCallExpression)
            {
                this.DealMethodCallExpression(_Expression as MethodCallExpression, _ParserArgs);
            }
            else
            {
                throw new DbFrameException("无法解析的表达式！");
            }
        }

        private void DealBinaryExpression(BinaryExpression _Expression, ParserArgs _ParserArgs)
        {
            //检查括号
            this.CheckBrackets(_Expression.ToString(), _ParserArgs, (_AddBrackets) =>
            {
                //左边
                this.CheckBrackets(_Expression.Left.ToString(), _ParserArgs, (_AddBrackets_Left) =>
                {
                    new WhereParser(_Expression.Left, _ParserArgs);
                });

                _ParserArgs.Builder.Append(this.GetOperatorStr(_Expression.NodeType));

                //右边
                this.CheckBrackets(_Expression.Right.ToString(), _ParserArgs, (_AddBrackets_Right) =>
                {
                    new WhereParser(_Expression.Right, _ParserArgs);
                });
            });
        }

        private void DealMemberExpression(MemberExpression _Expression, ParserArgs _ParserArgs)
        {
            if (_Expression.Expression is ParameterExpression)
            {
                if (_ParserArgs.TabIsAlias)
                    new WhereParser(_Expression.Expression, _ParserArgs);
                _ParserArgs.Builder.Append(_Expression.Member.Name);
            }
            else
            {
                if (_Expression.Expression == null)
                {
                    Eval(_Expression, _ParserArgs);
                }
                else
                {
                    var typeName = _Expression.Expression.GetType().Name;
                    if (typeName == "TypedParameterExpression")
                        _ParserArgs.Builder.Append(_Expression.Member.Name);
                    else
                        Eval(_Expression, _ParserArgs);
                }

            }

        }

        private void DealConstantExpression(ConstantExpression _Expression, ParserArgs _ParserArgs)
        {
            this.Eval(_Expression, _ParserArgs);
        }

        private void DealUnaryExpression(UnaryExpression _Expression, ParserArgs _ParserArgs)
        {
            new WhereParser(_Expression.Operand, _ParserArgs);
        }

        private void DealParameterExpression(ParameterExpression _Expression, ParserArgs _ParserArgs)
        {
            _ParserArgs.Builder.Append((_Expression as ParameterExpression).Name + ".");
        }

        private void DealNewArrayExpression(NewArrayExpression _Expression, ParserArgs _ParserArgs)
        {
            StringBuilder tmpstr = new StringBuilder();
            foreach (Expression ex in _Expression.Expressions)
            {
                tmpstr.Append("'" + this.Eval(ex) + "'");
                tmpstr.Append(",");
            }
            _ParserArgs.AddParameter(tmpstr.ToString(0, tmpstr.Length - 1));
        }

        private void DealMethodCallExpression(MethodCallExpression _Expression, ParserArgs _ParserArgs)
        {
            if (_Expression.Arguments.Count > 0)
            {
                if (_Expression.Object == null)
                {
                    var _Member = (_Expression.Arguments[0] as MemberExpression);
                    switch (_Expression.Method.Name)
                    {
                        case "Like":
                            var _Alias = (_Member.Expression as ParameterExpression).Name;
                            _ParserArgs.Builder.Append(_Alias + "." + _Member.Member.Name);
                            _ParserArgs.Builder.Append(" LIKE ");
                            new WhereParser(_Expression.Arguments[1], _ParserArgs);
                            break;
                        case "In":
                            _Alias = (_Member.Expression as ParameterExpression).Name;
                            _ParserArgs.Builder.Append(_Alias + "." + _Member.Member.Name);
                            _ParserArgs.Builder.Append(" IN ( ");
                            new WhereParser(_Expression.Arguments[1], _ParserArgs);
                            _ParserArgs.Builder.Append(" ) ");
                            break;
                        case "NotIn":
                            _Alias = (_Member.Expression as ParameterExpression).Name;
                            _ParserArgs.Builder.Append(_Alias + "." + _Member.Member.Name);
                            _ParserArgs.Builder.Append(" NOT IN ( ");
                            new WhereParser(_Expression.Arguments[1], _ParserArgs);
                            _ParserArgs.Builder.Append(" ) ");
                            break;
                        case "SqlString":
                        case "SqlStringCompare":
                            _Alias = (_Member.Expression as ParameterExpression).Name;
                            if (_Expression.Arguments[1] is ConstantExpression)
                            {
                                var _Code = (_Expression.Arguments[1] as ConstantExpression).Value.ToStr();
                                if (!string.IsNullOrEmpty(_Code))
                                {
                                    _Code = _Code.Replace("@F", _Alias + "." + _Member.Member.Name);
                                }
                                _ParserArgs.Builder.Append(_Code);
                            }
                            else if (_Expression.Arguments[1] is BinaryExpression)
                            {
                                var _BinaryExpression = _Expression.Arguments[1] as BinaryExpression;
                                _ParserArgs.Builder.Append(" ");
                                //这里将左右两边拼接
                                this.SplitJoint(_BinaryExpression, _ParserArgs, _Alias + "." + _Member.Member.Name);
                            }
                            break;
                        default:
                            Eval(_Expression, _ParserArgs);
                            break;
                    }
                }
                else if (_Expression.Object != null)
                {
                    dynamic _Member = _Expression.Object;
                    var _Alias = "";
                    switch (_Expression.Method.Name)
                    {
                        case "StartsWith":
                            _Alias = _Member.Expression.Name;
                            _ParserArgs.Builder.Append(_Alias + "." + _Member.Member.Name);
                            _ParserArgs.Builder.Append(" LIKE ");
                            new WhereParser(_Expression.Arguments[0], _ParserArgs);
                            _ParserArgs.Builder.Append(" + '%' ");
                            break;
                        case "Contains":
                            _Alias = _Member.Expression.Name;
                            _ParserArgs.Builder.Append(_Alias + "." + _Member.Member.Name);
                            _ParserArgs.Builder.Append(" LIKE '%' + ");
                            new WhereParser(_Expression.Arguments[0], _ParserArgs);
                            _ParserArgs.Builder.Append(" + '%' ");
                            break;
                        case "EndsWith":
                            _Alias = _Member.Expression.Name;
                            _ParserArgs.Builder.Append(_Alias + "." + _Member.Member.Name);
                            _ParserArgs.Builder.Append(" LIKE '%' + ");
                            new WhereParser(_Expression.Arguments[0], _ParserArgs);
                            break;
                        case "Equals":
                            _Alias = _Member.Expression.Name;
                            _ParserArgs.Builder.Append(_Alias + "." + _Member.Member.Name);
                            _ParserArgs.Builder.Append(" = ");
                            new WhereParser(_Expression.Arguments[0], _ParserArgs);
                            break;
                        default:
                            Eval(_Expression, _ParserArgs);
                            break;
                    }
                }
            }
            else
            {
                Eval(_Expression, _ParserArgs);
            }

        }

        private string GetOperatorStr(ExpressionType _ExpressionType)
        {
            switch (_ExpressionType)
            {
                case ExpressionType.Or:
                case ExpressionType.OrElse: return " OR ";
                case ExpressionType.And:
                case ExpressionType.AndAlso: return " AND ";
                case ExpressionType.GreaterThan: return " > ";
                case ExpressionType.GreaterThanOrEqual: return " >= ";
                case ExpressionType.LessThan: return " < ";
                case ExpressionType.LessThanOrEqual: return " <= ";
                case ExpressionType.Equal: return " = ";
                case ExpressionType.NotEqual: return " <> ";
                case ExpressionType.Add: return " + ";
                case ExpressionType.Subtract: return " - ";
                case ExpressionType.Multiply: return " * ";
                case ExpressionType.Divide: return " / ";
                case ExpressionType.Modulo: return " % ";
                default: throw new DbFrameException("无法解析的表达式！");
            }
        }

        private void Eval(Expression _Expression, ParserArgs _ParserArgs)
        {
            UnaryExpression cast = Expression.Convert(_Expression, typeof(object));
            var obj = Expression.Lambda<Func<object>>(cast).Compile().Invoke();
            if (obj == null)
            {
                _ParserArgs.AddParameter(obj);
            }
            else
            {
                var type = obj.GetType();
                if (type.Name == "List`1") //list集合
                {
                    var list = obj as IEnumerable;
                    var index = 0;
                    foreach (var item in list)
                    {
                        _ParserArgs.AddParameter(item.ToStr());
                        index = _ParserArgs.Builder.Length;
                        _ParserArgs.Builder.Append(",");
                    }
                    _ParserArgs.Builder.Remove(index, 1);
                }
                else
                    _ParserArgs.AddParameter(obj);
            }
        }

        /// <summary>
        /// 计算值
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public object Eval(Expression expression)
        {
            UnaryExpression cast = Expression.Convert(expression, typeof(object));
            var obj = Expression.Lambda<Func<object>>(cast).Compile().Invoke();
            return obj != null ? GetValueFormat(obj) : obj;
        }


        /// <summary>
        /// 对某些值 特殊处理
        /// </summary>
        /// <param name="obj"></param>
        private object GetValueFormat(object obj)
        {
            var type = obj.GetType();
            if (type.Name == "List`1") //list集合
            {
                var data = new List<string>();
                var list = obj as IEnumerable;
                string sql = string.Empty;
                foreach (var item in list)
                {
                    data.Add(item.ToStr());
                }
                sql = string.Join(",", data);
                return sql;
            }
            return obj;
        }

        /// <summary>
        /// 字符串拼接
        /// </summary>
        /// <param name="_BinaryExpression"></param>
        /// <param name="_ParserArgs"></param>
        /// <param name="_Name"></param>
        private void SplitJoint(BinaryExpression _BinaryExpression, ParserArgs _ParserArgs, string _Name)
        {
            var _Left = _BinaryExpression.Left;
            var _Right = _BinaryExpression.Right;

            if (_BinaryExpression.NodeType != ExpressionType.Add) throw new DbFrameException("无法解析的表达式！");

            if (_Left is ConstantExpression && _Left.Type == typeof(string))
            {
                var _ConstantExpression = _Left as ConstantExpression;
                _ParserArgs.Builder.Append(_ConstantExpression.Value.ToStr().Replace("@F", _Name));
            }

            //如果右边还有 BinaryExpression 树类型 递归下去
            if (_Right is BinaryExpression)
            {
                this.SplitJoint(_Right as BinaryExpression, _ParserArgs, _Name);
            }
            else if (_Right is ConstantExpression)
            {
                var _ConstantExpression = _Right as ConstantExpression;
                _ParserArgs.Builder.Append(_ConstantExpression.Value.ToStr().Replace("@F", _Name));
            }
            else
            {
                this.Eval(_Right, _ParserArgs);
            }
        }

        /// <summary>
        /// 检查是否有 括号
        /// </summary>
        /// <param name="_Expression"></param>
        /// <param name="_ParserArgs"></param>
        /// <param name="_Action"></param>
        private void CheckBrackets(string _Str, ParserArgs _ParserArgs, Action<bool> _Action)
        {
            //检查是否有括号
            var _AddBrackets = !string.IsNullOrEmpty(_Str) && _Str.Length > 5 && _Str.StartsWith("((") && _Str.EndsWith("))");

            if (_AddBrackets) _ParserArgs.Builder.Append(" (");

            _Action(_AddBrackets);

            if (_AddBrackets) _ParserArgs.Builder.Append(" )");
        }



    }
}
