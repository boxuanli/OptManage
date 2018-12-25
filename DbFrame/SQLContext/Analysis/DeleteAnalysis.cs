using System;
using System.Collections.Generic;
using System.Text;
namespace DbFrame.SqlContext.Analysis
{
    //
    using System.Linq.Expressions;
    using DbFrame.Class;
    using ExpressionTree;

    public class DeleteAnalysis : Abstract.AbstractDelete
    {
        public DeleteAnalysis()
        { }


        public override bool Delete<T>(Expression<Func<T, bool>> Where)
        {
            return Execute<T>(Where);
        }

        public override bool Delete<T>(Expression<Func<T, bool>> Where, List<SQL> li)
        {
            return Execute<T>(Where, li);
        }

        public override bool DeleteById<T>(object Id)
        {
            return ExecuteById<T>(Id);
        }

        public override bool DeleteById<T>(object Id, List<SQL> li)
        {
            return ExecuteById<T>(Id, li);
        }

        public override bool Delete<T>(string WhereStr, object Param)
        {
            _Code = new StringBuilder();
            var _TableInfo = TableInfo.Get(typeof(T));
            var SqlPar = new Dictionary<string, object>();
            if (Param != null) SqlPar = Param.ToDictionary();
            this._Code.Append("DELETE FROM " + _TableInfo.TableName + " WHERE 1=1 " + WhereStr + "; ");

            return _DbHelper.Commit(new List<SQL>() { new SQL(_Code.ToString(), SqlPar) });
        }

        public override bool Delete<T>(string WhereStr, object Param, List<SQL> li)
        {
            _Code = new StringBuilder();
            var _TableInfo = TableInfo.Get(typeof(T));
            var SqlPar = new Dictionary<string, object>();
            if (Param != null) SqlPar = Param.ToDictionary();
            this._Code.Append("DELETE FROM " + _TableInfo.TableName + " WHERE 1=1 " + WhereStr + "; ");
            li.Add(new SQL(_Code.ToString(), SqlPar));
            return true;
        }

        private bool Execute<T>(Expression<Func<T, bool>> Where, List<SQL> li = null) where T : class, new()
        {
            var sql = this.SqlString<T>(Where);
            if (li == null)
            {
                if (!_DbHelper.Commit(new List<SQL>() { sql }))
                    return false;
            }
            else
            {
                li.Add(sql);
            }
            return true;
        }

        private bool ExecuteById<T>(object Id, List<SQL> li = null) where T : class, new()
        {
            var sql = this.SqlStringById<T>(Id);
            if (li == null)
            {
                if (!_DbHelper.Commit(new List<SQL>() { sql }))
                    return false;
            }
            else
            {
                li.Add(sql);
            }
            return true;
        }

        private SQL SqlString<T>(Expression<Func<T, bool>> Where) where T : class, new()
        {
            return this.Analysis<T>((_ParserArgs, Model) =>
            {
                if (Where != null)
                {
                    _ParserArgs.Builder.Append("AND ");
                    Parser.Where(Where, _ParserArgs);
                }
            });
        }

        private SQL SqlStringById<T>(object Id) where T : class, new()
        {
            return this.Analysis<T>((_ParserArgs, _TableInfo) =>
            {
                var _Key = _TableInfo.KeyFieldInfo; if (_Key == null) throw new ArgumentNullException("找不到 实体 中的 主键！");
                _ParserArgs.Builder.Append(" AND " + _Key.FieldName + "=@" + _Key.FieldName + "");
                _ParserArgs.SqlParameters.Add("@" + _Key.FieldName, Id);
            });
        }

        private SQL Analysis<T>(Action<ParserArgs, Entity_Table> CallBack) where T : class, new()
        {
            _Code = new StringBuilder();
            var _TableInfo = TableInfo.Get(typeof(T));
            string TabName = _TableInfo.TableName;
            var pa = new ParserArgs();
            pa.TabIsAlias = false;

            CallBack(pa, _TableInfo);

            this._Code.Append("DELETE FROM " + TabName + " WHERE 1=1 " + pa.Builder.ToString() + ";");
            return new SQL(_Code.ToString(), pa.SqlParameters);
        }












    }
}
