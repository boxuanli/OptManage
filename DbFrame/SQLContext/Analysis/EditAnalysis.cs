using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DbFrame.SqlContext.Analysis
{
    //
    using System.Linq.Expressions;
    using DbFrame.Class;
    using ExpressionTree;

    public class EditAnalysis : Abstract.AbstractEdit
    {
        public EditAnalysis()
        { }

        public override bool Edit<T>(T Set, Expression<Func<T, bool>> Where)
        {
            var list = new List<MemberBinding>();
            var fileds = ReflexHelper.GetPropertyInfos(typeof(T));//.Where(w => w.Name != Set.GetKey().FieldName);
            var _TableInfo = TableInfo.Get(Set);
            foreach (var item in fileds)
            {
                //检测有无忽略字段
                if (_TableInfo.Fields.Where(w => w.IsIgnore == true && w.FieldName == item.Name).FirstOrDefault() != null) continue;
                list.Add(Expression.Bind(item, Expression.Constant(item.GetValue(Set), item.PropertyType)));
            }

            return Execute<T>(Expression.MemberInit(Expression.New(typeof(T)), list), Where);
        }

        public override bool Edit<T>(Expression<Func<T>> Set, Expression<Func<T, bool>> Where)
        {
            return Execute<T>(Set.Body as MemberInitExpression, Where);
        }

        public override bool Edit<T>(T Set, Expression<Func<T, bool>> Where, List<SQL> li)
        {
            var list = new List<MemberBinding>();
            var fileds = ReflexHelper.GetPropertyInfos(typeof(T));//.Where(w => w.Name != Set.GetKey().FieldName);
            var _TableInfo = TableInfo.Get(Set);
            foreach (var item in fileds)
            {
                //检测有无忽略字段
                if (_TableInfo.Fields.Where(w => w.IsIgnore == true && w.FieldName == item.Name).FirstOrDefault() != null) continue;
                list.Add(Expression.Bind(item, Expression.Constant(item.GetValue(Set), item.PropertyType)));
            }

            return Execute<T>(Expression.MemberInit(Expression.New(typeof(T)), list), Where, li);
        }

        public override bool Edit<T>(Expression<Func<T>> Set, Expression<Func<T, bool>> Where, List<SQL> li)
        {
            return Execute<T>(Set.Body as MemberInitExpression, Where, li);
        }



        public override bool EditById<T>(T Set)
        {
            var list = new List<MemberBinding>();
            var fileds = ReflexHelper.GetPropertyInfos(typeof(T));//.Where(w => w.Name != Set.GetKey().FieldName);
            var _TableInfo = TableInfo.Get(Set);
            foreach (var item in fileds)
            {
                //检测有无忽略字段
                if (_TableInfo.Fields.Where(w => w.IsIgnore == true && w.FieldName == item.Name).FirstOrDefault() != null) continue;
                list.Add(Expression.Bind(item, Expression.Constant(item.GetValue(Set), item.PropertyType)));
            }

            return ExecuteById<T>(Expression.MemberInit(Expression.New(typeof(T)), list));
        }

        public override bool EditById<T>(Expression<Func<T>> Set)
        {
            return ExecuteById<T>((Set.Body as MemberInitExpression));
        }

        public override bool EditById<T>(T Set, List<SQL> li)
        {
            var list = new List<MemberBinding>();
            var fileds = ReflexHelper.GetPropertyInfos(typeof(T));//.Where(w => w.Name != Set.GetKey().FieldName);
            var _TableInfo = TableInfo.Get(Set);
            foreach (var item in fileds)
            {
                //检测有无忽略字段
                if (_TableInfo.Fields.Where(w => w.IsIgnore == true && w.FieldName == item.Name).FirstOrDefault() != null) continue;
                list.Add(Expression.Bind(item, Expression.Constant(item.GetValue(Set), item.PropertyType)));
            }

            return ExecuteById<T>(Expression.MemberInit(Expression.New(typeof(T)), list), li);
        }

        public override bool EditById<T>(Expression<Func<T>> Set, List<SQL> li)
        {
            return ExecuteById<T>(Set.Body as MemberInitExpression, li);
        }


        public override bool EditCustomSet<T>(Expression<Func<T, object>> Set, Expression<Func<T, bool>> Where)
        {
            return this.CustomSet<T>(Set, Where);
        }

        public override bool EditCustomSet<T>(Expression<Func<T, object>> Set, Expression<Func<T, bool>> Where, List<SQL> li)
        {
            return this.CustomSet<T>(Set, Where, li);
        }






        private bool Execute<T>(MemberInitExpression Set, Expression<Func<T, bool>> Where, List<SQL> li = null)
            where T : class, new()
        {
            var sql = this.SqlString(Set, Where);
            if (li == null)
            {
                if (!_DbHelper.Commit(new List<SQL>() { sql })) return false;
            }
            else
            {
                li.Add(sql);
            }
            return true;
        }

        private bool ExecuteById<T>(MemberInitExpression Set, List<SQL> li = null)
            where T : class, new()
        {
            var sql = this.SqlStringById<T>(Set);
            if (li == null)
            {
                if (!_DbHelper.Commit(new List<SQL>() { sql })) return false;
            }
            else
            {
                li.Add(sql);
            }
            return true;
        }

        private SQL Analysis<T>(MemberInitExpression Set, Action<ParserArgs, Entity_Table> CallBack)
            where T : class, new()
        {
            _Code = new StringBuilder();
            var _TableInfo = TableInfo.Get(typeof(T));
            string TabName = _TableInfo.TableName;
            var set = new List<string>();

            _Code.Append("UPDATE " + TabName + " SET ");

            //获取 Where 语句
            var pa = new ParserArgs();
            pa.TabIsAlias = false;

            CallBack(pa, _TableInfo);

            var _Where = pa.Builder.ToStr();

            foreach (MemberAssignment item in Set.Bindings)
            {
                //检测有无忽略字段
                if (_TableInfo.Fields.Where(w => w.IsIgnore == true && w.FieldName == item.Member.Name).FirstOrDefault() != null ||
                    item.Member.Name == _TableInfo.KeyFieldName)
                    continue;
                var value = Parser.Eval(item.Expression);
                var name = item.Member.Name;
                var len = pa.SqlParameters.Count;

                set.Add(name + "=@" + name + "_" + len);
                pa.SqlParameters.Add(name + "_" + len, value);
            }

            _Code.Append(string.Join(",", set) + " WHERE 1=1 " + _Where + ";");

            return new SQL(_Code.ToString(), pa.SqlParameters);
        }

        private SQL SqlString<T>(MemberInitExpression Set, Expression<Func<T, bool>> Where)
            where T : class, new()
        {
            return Analysis<T>(Set, (_ParserArgs, _TableInfo) =>
            {
                if (Where != null)
                {
                    _ParserArgs.Builder.Append("AND ");
                    Parser.Where(Where, _ParserArgs);
                }
            });
        }

        private SQL SqlStringById<T>(MemberInitExpression Set)
            where T : class, new()
        {
            return Analysis<T>(Set, (_ParserArgs, _TableInfo) =>
            {
                var _Key = _TableInfo.KeyFieldInfo; if (_Key == null) throw new ArgumentNullException("找不到 实体 中的 主键！");
                var IdValue = Parser.Eval((Set.Bindings.Where(w => w.Member.Name == _Key.FieldName).FirstOrDefault() as MemberAssignment).Expression);
                _ParserArgs.Builder.Append(" AND " + _Key.FieldName + "=@" + _Key.FieldName + "");
                _ParserArgs.SqlParameters.Add("@" + _Key.FieldName, IdValue);
            });
        }

        /// <summary>
        /// 自定义 Set
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="body"></param>
        /// <param name="Where"></param>
        /// <param name="li"></param>
        /// <returns></returns>
        private bool CustomSet<T>(LambdaExpression body, Expression<Func<T, bool>> Where, List<SQL> li = null)
            where T : class, new()
        {
            _Code = new StringBuilder();
            var _TableInfo = TableInfo.Get(typeof(T));
            string TabName = _TableInfo.TableName;
            _Code.Append("UPDATE " + TabName + " SET ");

            //获取 Where 语句
            var _ParserArgs = new ParserArgs();
            _ParserArgs.TabIsAlias = false;
            Parser.UpdateSet(body, _Code, _ParserArgs);
            _ParserArgs.Builder.Append("AND ");
            Parser.Where(Where, _ParserArgs);

            _Code.Append(" WHERE 1=1 " + _ParserArgs.Builder.ToStr() + ";");
            var sql = new SQL(_Code.ToString(), _ParserArgs.SqlParameters);

            if (li == null)
            {
                if (!_DbHelper.Commit(new List<SQL>() { sql })) return false;
            }
            else
            {
                li.Add(sql);
            }
            return true;
        }



    }
}
