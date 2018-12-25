using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbFrame.SqlContext.Analysis
{
    using System.Data;
    using System.Linq.Expressions;
    using Abstract;
    using Interface;
    using Class;
    using ExpressionTree;
    public class FindAnalysis : AbstractFind
    {
        public FindAnalysis() { }

        public IQuery<T1> Query<T1>(Expression<Func<T1, object>> Select)
            where T1 : class, new()
        {
            return new QueryAnalysis<T1>().Select(Select);
        }

        public IQuery<T1, T2> Query<T1, T2>(Expression<Func<T1, T2, object>> Select)
            where T1 : class, new()
            where T2 : class, new()
        {
            return new QueryAnalysis<T1, T2>().Select(Select);
        }

        public IQuery<T1, T2, T3> Query<T1, T2, T3>(Expression<Func<T1, T2, T3, object>> Select)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
        {
            return new QueryAnalysis<T1, T2, T3>().Select(Select);
        }

        public IQuery<T1, T2, T3, T4> Query<T1, T2, T3, T4>(Expression<Func<T1, T2, T3, T4, object>> Select)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where T4 : class, new()
        {
            return new QueryAnalysis<T1, T2, T3, T4>().Select(Select);
        }

        public IQuery<T1, T2, T3, T4, T5> Query<T1, T2, T3, T4, T5>(Expression<Func<T1, T2, T3, T4, T5, object>> Select)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where T4 : class, new()
            where T5 : class, new()
        {
            return new QueryAnalysis<T1, T2, T3, T4, T5>().Select(Select);
        }

        public IQuery<T1, T2, T3, T4, T5, T6> Query<T1, T2, T3, T4, T5, T6>(Expression<Func<T1, T2, T3, T4, T5, T6, object>> Select)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where T4 : class, new()
            where T5 : class, new()
            where T6 : class, new()
        {
            return new QueryAnalysis<T1, T2, T3, T4, T5, T6>().Select(Select);
        }

        public IQuery<T1, T2, T3, T4, T5, T6, T7> Query<T1, T2, T3, T4, T5, T6, T7>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, object>> Select)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where T4 : class, new()
            where T5 : class, new()
            where T6 : class, new()
            where T7 : class, new()
        {
            return new QueryAnalysis<T1, T2, T3, T4, T5, T6, T7>().Select(Select);
        }

        public IQuery<T1, T2, T3, T4, T5, T6, T7, T8> Query<T1, T2, T3, T4, T5, T6, T7, T8>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, object>> Select)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where T4 : class, new()
            where T5 : class, new()
            where T6 : class, new()
            where T7 : class, new()
            where T8 : class, new()
        {
            return new QueryAnalysis<T1, T2, T3, T4, T5, T6, T7, T8>().Select(Select);
        }

        public IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9> Query<T1, T2, T3, T4, T5, T6, T7, T8, T9>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, object>> Select)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where T4 : class, new()
            where T5 : class, new()
            where T6 : class, new()
            where T7 : class, new()
            where T8 : class, new()
            where T9 : class, new()
        {
            return new QueryAnalysis<T1, T2, T3, T4, T5, T6, T7, T8, T9>().Select(Select);
        }

        public IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> Query<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, object>> Select)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where T4 : class, new()
            where T5 : class, new()
            where T6 : class, new()
            where T7 : class, new()
            where T8 : class, new()
            where T9 : class, new()
            where T10 : class, new()
        {
            return new QueryAnalysis<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>().Select(Select);
        }

        public IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> Query<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, object>> Select)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where T4 : class, new()
            where T5 : class, new()
            where T6 : class, new()
            where T7 : class, new()
            where T8 : class, new()
            where T9 : class, new()
            where T10 : class, new()
            where T11 : class, new()
        {
            return new QueryAnalysis<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>().Select(Select);
        }

        public IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> Query<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, object>> Select)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where T4 : class, new()
            where T5 : class, new()
            where T6 : class, new()
            where T7 : class, new()
            where T8 : class, new()
            where T9 : class, new()
            where T10 : class, new()
            where T11 : class, new()
            where T12 : class, new()
        {
            return new QueryAnalysis<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>().Select(Select);
        }

        public IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> Query<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, object>> Select)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where T4 : class, new()
            where T5 : class, new()
            where T6 : class, new()
            where T7 : class, new()
            where T8 : class, new()
            where T9 : class, new()
            where T10 : class, new()
            where T11 : class, new()
            where T12 : class, new()
            where T13 : class, new()
        {
            return new QueryAnalysis<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>().Select(Select);
        }

        public IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> Query<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, object>> Select)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where T4 : class, new()
            where T5 : class, new()
            where T6 : class, new()
            where T7 : class, new()
            where T8 : class, new()
            where T9 : class, new()
            where T10 : class, new()
            where T11 : class, new()
            where T12 : class, new()
            where T13 : class, new()
            where T14 : class, new()
        {
            return new QueryAnalysis<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>().Select(Select);
        }

        public IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> Query<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, object>> Select)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where T4 : class, new()
            where T5 : class, new()
            where T6 : class, new()
            where T7 : class, new()
            where T8 : class, new()
            where T9 : class, new()
            where T10 : class, new()
            where T11 : class, new()
            where T12 : class, new()
            where T13 : class, new()
            where T14 : class, new()
            where T15 : class, new()
        {
            return new QueryAnalysis<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>().Select(Select);
        }


        /************************* 基础 函数***************************/
        public override T FindSingle<T>(string SqlStr, object Param)
        {
            return this._DbHelper.QuerySingleOrDefault<T>(SqlStr, Param);
        }

        public override DataTable FindTable(string SqlStr, object Param)
        {
            return this._DbHelper.QueryDataTable(SqlStr, Param);
        }

        public override T Find<T>(string SqlStr, object Param)
        {
            var _Model = this._DbHelper.QueryFirstOrDefault<T>(SqlStr, Param);
            if (_Model == null)
                return ReflexHelper.CreateInstance<T>();
            return _Model;
        }

        public override IEnumerable<T> FindList<T>(string SqlStr, object Param)
        {
            return this._DbHelper.Query<T>(SqlStr, Param);
        }

        public override Paging FindPaging(string Sql, int Page, int PageSize, object Param)
        {
            return this._DbHelper.QueryPaging(Sql, Page, PageSize, Param);
        }

        public override int FindMaxNumber(string TabName, string FieldNum, string Where, object Param)
        {
            var SqlStr = this._DbHelper.FindMaxNumber(TabName, FieldNum, Where, Param);
            return this.FindSingle<int>(SqlStr, Param);
        }

        ///************************* 表达式树 函数***************************/
        public override DataTable FindTable<T>(Expression<Func<T, bool>> Where, Expression<Func<T, object>> OrderBy)
        {
            var _SQL = this.GetSqlStr(null, Where, OrderBy);
            return this._DbHelper.QueryDataTable(_SQL.Sql_Parameter, _SQL.Parameter);
        }

        public override T Find<T>(Expression<Func<T, bool>> Where)
        {
            var _SQL = this.GetSqlStr(null, Where, null);
            var _Model = this._DbHelper.QueryFirstOrDefault<T>(_SQL.Sql_Parameter, _SQL.Parameter);
            if (_Model == null)
                return ReflexHelper.CreateInstance<T>();
            return _Model;
        }

        public override T FindById<T>(object Id)
        {
            var _SQL = this.GetSqlStrById<T>(null, Id, null);
            var _Model = this._DbHelper.QueryFirstOrDefault<T>(_SQL.Sql_Parameter, _SQL.Parameter);
            if (_Model == null)
                return ReflexHelper.CreateInstance<T>();
            return _Model;
        }

        public override IEnumerable<T> FindList<T>(Expression<Func<T, bool>> Where, Expression<Func<T, object>> OrderBy)
        {
            var _SQL = this.GetSqlStr(null, Where, OrderBy);
            return this._DbHelper.Query<T>(_SQL.Sql_Parameter, _SQL.Parameter);
        }

        ///************************* 表达式树 函数 自定义 Select ***************************/
        public override DataTable FindTable<T>(Expression<Func<T, object>> Select, Expression<Func<T, bool>> Where, Expression<Func<T, object>> OrderBy)
        {
            var _SQL = this.GetSqlStr(Select, Where, OrderBy);
            return this._DbHelper.QueryDataTable(_SQL.Sql_Parameter, _SQL.Parameter);
        }

        public override TResult FindSingle<T, TResult>(Expression<Func<T, object>> Select, Expression<Func<T, bool>> Where)
        {
            var _SQL = this.GetSqlStr(Select, Where, null);
            return this._DbHelper.QuerySingleOrDefault<TResult>(_SQL.Sql_Parameter, _SQL.Parameter);
        }

        public override T Find<T>(Expression<Func<T, object>> Select, Expression<Func<T, bool>> Where, Expression<Func<T, object>> OrderBy)
        {
            var _SQL = this.GetSqlStr(Select, Where, OrderBy);
            var _Model = this._DbHelper.QueryFirstOrDefault<T>(_SQL.Sql_Parameter, _SQL.Parameter);
            if (_Model == null)
                return ReflexHelper.CreateInstance<T>();
            return _Model;
        }

        public override T FindById<T>(Expression<Func<T, object>> Select, object Id)
        {
            var _SQL = this.GetSqlStrById(Select, Id, null);
            var _Model = this._DbHelper.QueryFirstOrDefault<T>(_SQL.Sql_Parameter, _SQL.Parameter);
            if (_Model == null)
                return ReflexHelper.CreateInstance<T>();
            return _Model;
        }

        public override IEnumerable<T> FindList<T>(Expression<Func<T, object>> Select, Expression<Func<T, bool>> Where, Expression<Func<T, object>> OrderBy)
        {
            var _SQL = this.GetSqlStr(Select, Where, OrderBy);
            return this._DbHelper.Query<T>(_SQL.Sql_Parameter, _SQL.Parameter);
        }

        public override IEnumerable<TResult> FindList<T, TResult>(Expression<Func<T, object>> Select, Expression<Func<T, bool>> Where, Expression<Func<T, object>> OrderBy)
        {
            var _SQL = this.GetSqlStr(Select, Where, OrderBy);
            return this._DbHelper.Query<TResult>(_SQL.Sql_Parameter, _SQL.Parameter);
        }


        private SQL GetSqlStr<T>(Expression<Func<T, object>> Select, Expression<Func<T, bool>> Where, Expression<Func<T, object>> OrderBy)
            where T : class, new()
        {
            var _TableInfo = TableInfo.Get(typeof(T));

            var Code = new StringBuilder();
            var Parma = new Dictionary<string, object>();
            var Alias = new Dictionary<string, string>();

            //Select
            Code.Append(" SELECT ");

            if (Select == null)
            {
                Code.Append("* ");
            }
            else
            {
                Parser.Select(Select, Code, Alias);
            }

            Code.Append(" FROM " + _TableInfo.TableName + " ");

            //Where
            if (Where != null)
            {
                var _ParserArgs = new ParserArgs();

                Code.Append("AS " + Where.Parameters[0].Name + " WHERE 1=1 ");

                Parser.Where(Where, _ParserArgs);
                Code.Append(" AND " + _ParserArgs.Builder);
                foreach (var item in _ParserArgs.SqlParameters)
                {
                    Parma.Add(item.Key, item.Value);
                }
            }

            //OrderBy
            if (OrderBy != null)
            {
                Code.Append(" ORDER BY ");
                Parser.OrderBy(OrderBy, Code, Alias);
            }

            return new SQL(Code.ToString(), Parma);
        }

        private SQL GetSqlStrById<T>(Expression<Func<T, object>> Select, object Id, Expression<Func<T, object>> OrderBy)
            where T : class, new()
        {
            var _TableInfo = TableInfo.Get(typeof(T));

            var Code = new StringBuilder();
            var Parma = new Dictionary<string, object>();
            var Alias = new Dictionary<string, string>();

            //Select
            Code.Append(" SELECT ");
            if (Select == null)
            {
                Code.Append("* ");
            }
            else
            {
                Parser.Select(Select, Code, Alias);
            }
            Code.Append(" FROM " + _TableInfo.TableName + " ");

            //Where
            var _ParserArgs = new ParserArgs();
            Code.Append("WHERE 1=1 ");
            var KeyName = _TableInfo.KeyFieldName;
            Code.Append(" AND " + KeyName + "=@" + KeyName);
            Parma.Add(KeyName, Id);

            //OrderBy
            if (OrderBy != null)
            {
                Code.Append(" ORDER BY ");
                Parser.OrderBy(OrderBy, Code, Alias);
            }

            return new SQL(Code.ToString(), Parma);
        }


    }
}
