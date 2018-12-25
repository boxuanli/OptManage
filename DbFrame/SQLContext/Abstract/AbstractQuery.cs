
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbFrame.SqlContext.Abstract
{
    using System.Data;
    using System.Linq.Expressions;
    using DbFrame.Class;
    using DbFrame.SqlContext.Interface;
    using DbFrame.Ado;
    using DbFrame.ExpressionTree;

    public abstract class AbstractQuery : BaseClass, IQuery
    {

        //将别名 和表名存起来 别名是 Key
        public Dictionary<string, string> Alias { get; set; }
        protected bool IsAddWhere { get; set; }
        protected ParserArgs _ParserArgs { get; set; }
        public AbstractQuery()
        {
            this.Alias = new Dictionary<string, string>();
            this.IsAddWhere = true;
            this._ParserArgs = new ParserArgs();
        }

        public abstract IEnumerable<T> ToList<T>();
        public abstract T ToEntity<T>();
        public abstract DataTable ToTable();
        public abstract SQL ToSQL();
        public abstract Dictionary<string, object> GetSqlParameters();
        public abstract void AddSqlParameters(string Key, object Value);

        IEnumerable<T> IQuery.ToList<T>()
        {
            return this.ToList<T>();
        }

        T IQuery.ToEntity<T>()
        {
            return this.ToEntity<T>();
        }

        DataTable IQuery.ToTable()
        {
            return this.ToTable();
        }

        SQL IQuery.ToSQL()
        {
            return this.ToSQL();
        }

        Dictionary<string, object> IQuery.GetSqlParameters()
        {
            return this.GetSqlParameters();
        }

        void IQuery.AddSqlParameters(string Key, object Value)
        {
            this.AddSqlParameters(Key, Value);
        }

        /// <summary>
        /// 开始组装查询语句
        /// </summary>
        /// <param name="_LambdaExpression"></param>
        protected void CodeSelect(LambdaExpression _LambdaExpression)
        {
            _Code.Append("SELECT ");

            Parser.Select(_LambdaExpression, _Code, Alias);

            var ByName = _LambdaExpression.Parameters[0].Name;
            var TabName = Alias[ByName] + " AS " + ByName;
            _Code.Append(" FROM " + TabName);
        }

        /// <summary>
        /// 链接辅助函数
        /// </summary>
        /// <param name="JoinStr"></param>
        /// <param name="_LambdaExpression"></param>
        /// <param name="JoinTabName"></param>
        protected void CodeJoin(EJoinType _EJoinType, LambdaExpression _LambdaExpression, string JoinTabName)
        {
            _ParserArgs.Builder.Clear();
            var JoinStr = "LEFT JOIN";
            switch (_EJoinType)
            {
                case EJoinType.INNER_JOIN:
                    JoinStr = "INNER JOIN";
                    break;
                case EJoinType.LEFT_JOIN:
                    JoinStr = "LEFT JOIN";
                    break;
                case EJoinType.LEFT_OUTER_JOIN:
                    JoinStr = "LEFT OUTER JOIN";
                    break;
                case EJoinType.RIGHT_JOIN:
                    JoinStr = "RIGHT JOIN";
                    break;
                case EJoinType.RIGHT_OUTER_JOIN:
                    JoinStr = "RIGHT OUTER JOIN";
                    break;
                case EJoinType.FULL_JOIN:
                    JoinStr = "FULL JOIN";
                    break;
                case EJoinType.FULL_OUTER_JOIN:
                    JoinStr = "FULL OUTER JOIN";
                    break;
                case EJoinType.CROSS_JOIN:
                    JoinStr = "CROSS JOIN";
                    break;
                default:
                    JoinStr = "LEFT JOIN";
                    break;
            }
            Parser.JoinTable(_LambdaExpression, _Code, Alias, _ParserArgs, JoinStr, JoinTabName);
        }

        /// <summary>
        /// 分组辅助函数
        /// </summary>
        /// <param name="_LambdaExpression"></param>
        protected void CodeGroupBy(LambdaExpression _LambdaExpression)
        {
            Parser.GroupBy(_LambdaExpression, _Code, Alias);
        }

        /// <summary>
        /// 排序辅助函数
        /// </summary>
        /// <param name="_LambdaExpression"></param>
        protected void CodeOrderBy(LambdaExpression _LambdaExpression)
        {
            _Code.Append(" ORDER BY ");
            Parser.OrderBy(_LambdaExpression, _Code, Alias);
        }

        /// <summary>
        /// Where 条件辅助函数
        /// </summary>
        /// <param name="_LambdaExpression"></param>
        protected void CodeWhere(LambdaExpression _LambdaExpression)
        {
            _ParserArgs.Builder.Clear();

            if (IsAddWhere) _Code.Append(" WHERE 1=1 ");

            Parser.Where(_LambdaExpression, _ParserArgs);

            _Code.Append(" AND " + _ParserArgs.Builder);

            _ParserArgs.Builder.Clear();

            this.IsAddWhere = false;
        }

        /// <summary>
        /// 自定义 Sql
        /// </summary>
        /// <param name="Sql"></param>
        /// <param name="Param"></param>
        protected void CodeSqlString(string Sql, object Param)
        {
            if (Param != null && Param is object)
            {
                var fields = Param.GetType().GetProperties().ToList();
                foreach (var item in fields)
                {
                    this.AddSqlParameters(item.Name, item.GetValue(Param));
                }
            }
            this._Code.Append(" " + Sql + " ");
        }

    }

    public abstract class AbstractQuery<T1> : AbstractQuery, IQuery<T1>
      where T1 : class,new()
    {
        public AbstractQuery()
        { }

        public abstract IQuery<T1> Select(Expression<Func<T1, object>> Column);

        public abstract IQuery<T1> Where(Expression<Func<T1, bool>> Where);

        public abstract IQuery<T1> WhereIF(bool IsWhere, Expression<Func<T1, bool>> Where);

        public abstract IQuery<T1> GroupBy(Expression<Func<T1, object>> GroupBy);

        public abstract IQuery<T1> OrderBy(Expression<Func<T1, object>> OrderBy);

        public abstract IQuery<T1> SqlString(string SQL, object Param);


        IQuery<T1> IQuery<T1>.Select(Expression<Func<T1, object>> Column)
        {
            return this.Select(Column);
        }

        IQuery<T1> IQuery<T1>.Where(Expression<Func<T1, bool>> Where)
        {
            return this.Where(Where);
        }

        IQuery<T1> IQuery<T1>.OrderBy(Expression<Func<T1, object>> OrderBy)
        {
            return this.OrderBy(OrderBy);
        }

        IQuery<T1> IQuery<T1>.SqlString(string SQL, object Param)
        {
            return this.SqlString(SQL, Param);
        }


        public override IEnumerable<T> ToList<T>()
        {
            return _DbHelper.Query<T>(this._Code.ToString(), this.GetSqlParameters());
        }

        public override T ToEntity<T>()
        {
            return _DbHelper.QueryFirstOrDefault<T>(this._Code.ToString(), this.GetSqlParameters());
        }

        public override DataTable ToTable()
        {
            return _DbHelper.QueryDataTable(this._Code.ToString(), this.GetSqlParameters());
        }

        public override SQL ToSQL()
        {
            return new SQL(this._Code.ToString(), this.GetSqlParameters());
        }

        public override Dictionary<string, object> GetSqlParameters()
        {
            return this._ParserArgs.SqlParameters;
        }

        public override void AddSqlParameters(string Key, object Value)
        {
            if (this._ParserArgs.SqlParameters.ContainsKey(Key)) throw new Exception("参数话 键" + Key + "已经存在！");
            this._ParserArgs.SqlParameters.Add(Key, Value);
        }






    }


    public abstract class AbstractQuery<T1, T2> : AbstractQuery, IQuery<T1, T2>
        where T1 : class,new()
        where T2 : class,new()
    {
        public AbstractQuery()
        { }

        public abstract IQuery<T1, T2> Select(Expression<Func<T1, T2, object>> Column);

        public abstract IQuery<T1, T2> Where(Expression<Func<T1, T2, bool>> Where);

        public abstract IQuery<T1, T2> WhereIF(bool IsWhere, Expression<Func<T1, T2, bool>> Where);

        public abstract IQuery<T1, T2> GroupBy(Expression<Func<T1, T2, object>> GroupBy);

        public abstract IQuery<T1, T2> OrderBy(Expression<Func<T1, T2, object>> OrderBy);

        public abstract IQuery<T1, T2> SqlString(string SQL, object Param);

        public abstract IQuery<T1, T2> Join(Expression<Func<T1, T2, bool>> ON, string JoinTabName, EJoinType _EJoinType);

        IQuery<T1, T2> IQuery<T1, T2>.Select(Expression<Func<T1, T2, object>> Column)
        {
            return this.Select(Column);
        }

        IQuery<T1, T2> IQuery<T1, T2>.Where(Expression<Func<T1, T2, bool>> Where)
        {
            return this.Where(Where);
        }

        IQuery<T1, T2> IQuery<T1, T2>.OrderBy(Expression<Func<T1, T2, object>> OrderBy)
        {
            return this.OrderBy(OrderBy);
        }

        IQuery<T1, T2> IQuery<T1, T2>.SqlString(string SQL, object Param)
        {
            return this.SqlString(SQL, Param);
        }

        IQuery<T1, T2> IQuery<T1, T2>.Join(Expression<Func<T1, T2, bool>> ON, string JoinTabName, EJoinType _EJoinType)
        {
            return this.Join(ON, JoinTabName, _EJoinType);
        }

        public override IEnumerable<T> ToList<T>()
        {
            return _DbHelper.Query<T>(this._Code.ToString(), this.GetSqlParameters());
        }

        public override T ToEntity<T>()
        {
            return _DbHelper.QueryFirstOrDefault<T>(this._Code.ToString(), this.GetSqlParameters());
        }

        public override DataTable ToTable()
        {
            return _DbHelper.QueryDataTable(this._Code.ToString(), this.GetSqlParameters());
        }

        public override SQL ToSQL()
        {
            return new SQL(this._Code.ToString(), this.GetSqlParameters());
        }

        public override Dictionary<string, object> GetSqlParameters()
        {
            return this._ParserArgs.SqlParameters;
        }

        public override void AddSqlParameters(string Key, object Value)
        {
            if (this._ParserArgs.SqlParameters.ContainsKey(Key)) throw new Exception("参数话 键" + Key + "已经存在！");
            this._ParserArgs.SqlParameters.Add(Key, Value);
        }






    }


    public abstract class AbstractQuery<T1, T2, T3> : AbstractQuery, IQuery<T1, T2, T3>
        where T1 : class,new()
        where T2 : class,new()
        where T3 : class,new()
    {
        public AbstractQuery()
        { }

        public abstract IQuery<T1, T2, T3> Select(Expression<Func<T1, T2, T3, object>> Column);

        public abstract IQuery<T1, T2, T3> Where(Expression<Func<T1, T2, T3, bool>> Where);

        public abstract IQuery<T1, T2, T3> WhereIF(bool IsWhere, Expression<Func<T1, T2, T3, bool>> Where);

        public abstract IQuery<T1, T2, T3> GroupBy(Expression<Func<T1, T2, T3, object>> GroupBy);

        public abstract IQuery<T1, T2, T3> OrderBy(Expression<Func<T1, T2, T3, object>> OrderBy);

        public abstract IQuery<T1, T2, T3> SqlString(string SQL, object Param);

        public abstract IQuery<T1, T2, T3> Join(Expression<Func<T1, T2, T3, bool>> ON, string JoinTabName, EJoinType _EJoinType);

        IQuery<T1, T2, T3> IQuery<T1, T2, T3>.Select(Expression<Func<T1, T2, T3, object>> Column)
        {
            return this.Select(Column);
        }

        IQuery<T1, T2, T3> IQuery<T1, T2, T3>.Where(Expression<Func<T1, T2, T3, bool>> Where)
        {
            return this.Where(Where);
        }

        IQuery<T1, T2, T3> IQuery<T1, T2, T3>.OrderBy(Expression<Func<T1, T2, T3, object>> OrderBy)
        {
            return this.OrderBy(OrderBy);
        }

        IQuery<T1, T2, T3> IQuery<T1, T2, T3>.SqlString(string SQL, object Param)
        {
            return this.SqlString(SQL, Param);
        }

        IQuery<T1, T2, T3> IQuery<T1, T2, T3>.Join(Expression<Func<T1, T2, T3, bool>> ON, string JoinTabName, EJoinType _EJoinType)
        {
            return this.Join(ON, JoinTabName, _EJoinType);
        }

        public override IEnumerable<T> ToList<T>()
        {
            return _DbHelper.Query<T>(this._Code.ToString(), this.GetSqlParameters());
        }

        public override T ToEntity<T>()
        {
            return _DbHelper.QueryFirstOrDefault<T>(this._Code.ToString(), this.GetSqlParameters());
        }

        public override DataTable ToTable()
        {
            return _DbHelper.QueryDataTable(this._Code.ToString(), this.GetSqlParameters());
        }

        public override SQL ToSQL()
        {
            return new SQL(this._Code.ToString(), this.GetSqlParameters());
        }

        public override Dictionary<string, object> GetSqlParameters()
        {
            return this._ParserArgs.SqlParameters;
        }

        public override void AddSqlParameters(string Key, object Value)
        {
            if (this._ParserArgs.SqlParameters.ContainsKey(Key)) throw new Exception("参数话 键" + Key + "已经存在！");
            this._ParserArgs.SqlParameters.Add(Key, Value);
        }






    }


    public abstract class AbstractQuery<T1, T2, T3, T4> : AbstractQuery, IQuery<T1, T2, T3, T4>
        where T1 : class,new()
        where T2 : class,new()
        where T3 : class,new()
        where T4 : class,new()
    {
        public AbstractQuery()
        { }

        public abstract IQuery<T1, T2, T3, T4> Select(Expression<Func<T1, T2, T3, T4, object>> Column);

        public abstract IQuery<T1, T2, T3, T4> Where(Expression<Func<T1, T2, T3, T4, bool>> Where);

        public abstract IQuery<T1, T2, T3, T4> WhereIF(bool IsWhere, Expression<Func<T1, T2, T3, T4, bool>> Where);

        public abstract IQuery<T1, T2, T3, T4> GroupBy(Expression<Func<T1, T2, T3, T4, object>> GroupBy);

        public abstract IQuery<T1, T2, T3, T4> OrderBy(Expression<Func<T1, T2, T3, T4, object>> OrderBy);

        public abstract IQuery<T1, T2, T3, T4> SqlString(string SQL, object Param);

        public abstract IQuery<T1, T2, T3, T4> Join(Expression<Func<T1, T2, T3, T4, bool>> ON, string JoinTabName, EJoinType _EJoinType);

        IQuery<T1, T2, T3, T4> IQuery<T1, T2, T3, T4>.Select(Expression<Func<T1, T2, T3, T4, object>> Column)
        {
            return this.Select(Column);
        }

        IQuery<T1, T2, T3, T4> IQuery<T1, T2, T3, T4>.Where(Expression<Func<T1, T2, T3, T4, bool>> Where)
        {
            return this.Where(Where);
        }

        IQuery<T1, T2, T3, T4> IQuery<T1, T2, T3, T4>.OrderBy(Expression<Func<T1, T2, T3, T4, object>> OrderBy)
        {
            return this.OrderBy(OrderBy);
        }

        IQuery<T1, T2, T3, T4> IQuery<T1, T2, T3, T4>.SqlString(string SQL, object Param)
        {
            return this.SqlString(SQL, Param);
        }

        IQuery<T1, T2, T3, T4> IQuery<T1, T2, T3, T4>.Join(Expression<Func<T1, T2, T3, T4, bool>> ON, string JoinTabName, EJoinType _EJoinType)
        {
            return this.Join(ON, JoinTabName, _EJoinType);
        }

        public override IEnumerable<T> ToList<T>()
        {
            return _DbHelper.Query<T>(this._Code.ToString(), this.GetSqlParameters());
        }

        public override T ToEntity<T>()
        {
            return _DbHelper.QueryFirstOrDefault<T>(this._Code.ToString(), this.GetSqlParameters());
        }

        public override DataTable ToTable()
        {
            return _DbHelper.QueryDataTable(this._Code.ToString(), this.GetSqlParameters());
        }

        public override SQL ToSQL()
        {
            return new SQL(this._Code.ToString(), this.GetSqlParameters());
        }

        public override Dictionary<string, object> GetSqlParameters()
        {
            return this._ParserArgs.SqlParameters;
        }

        public override void AddSqlParameters(string Key, object Value)
        {
            if (this._ParserArgs.SqlParameters.ContainsKey(Key)) throw new Exception("参数话 键" + Key + "已经存在！");
            this._ParserArgs.SqlParameters.Add(Key, Value);
        }






    }


    public abstract class AbstractQuery<T1, T2, T3, T4, T5> : AbstractQuery, IQuery<T1, T2, T3, T4, T5>
        where T1 : class,new()
        where T2 : class,new()
        where T3 : class,new()
        where T4 : class,new()
        where T5 : class,new()
    {
        public AbstractQuery()
        { }

        public abstract IQuery<T1, T2, T3, T4, T5> Select(Expression<Func<T1, T2, T3, T4, T5, object>> Column);

        public abstract IQuery<T1, T2, T3, T4, T5> Where(Expression<Func<T1, T2, T3, T4, T5, bool>> Where);

        public abstract IQuery<T1, T2, T3, T4, T5> WhereIF(bool IsWhere, Expression<Func<T1, T2, T3, T4, T5, bool>> Where);

        public abstract IQuery<T1, T2, T3, T4, T5> GroupBy(Expression<Func<T1, T2, T3, T4, T5, object>> GroupBy);

        public abstract IQuery<T1, T2, T3, T4, T5> OrderBy(Expression<Func<T1, T2, T3, T4, T5, object>> OrderBy);

        public abstract IQuery<T1, T2, T3, T4, T5> SqlString(string SQL, object Param);

        public abstract IQuery<T1, T2, T3, T4, T5> Join(Expression<Func<T1, T2, T3, T4, T5, bool>> ON, string JoinTabName, EJoinType _EJoinType);

        IQuery<T1, T2, T3, T4, T5> IQuery<T1, T2, T3, T4, T5>.Select(Expression<Func<T1, T2, T3, T4, T5, object>> Column)
        {
            return this.Select(Column);
        }

        IQuery<T1, T2, T3, T4, T5> IQuery<T1, T2, T3, T4, T5>.Where(Expression<Func<T1, T2, T3, T4, T5, bool>> Where)
        {
            return this.Where(Where);
        }

        IQuery<T1, T2, T3, T4, T5> IQuery<T1, T2, T3, T4, T5>.OrderBy(Expression<Func<T1, T2, T3, T4, T5, object>> OrderBy)
        {
            return this.OrderBy(OrderBy);
        }

        IQuery<T1, T2, T3, T4, T5> IQuery<T1, T2, T3, T4, T5>.SqlString(string SQL, object Param)
        {
            return this.SqlString(SQL, Param);
        }

        IQuery<T1, T2, T3, T4, T5> IQuery<T1, T2, T3, T4, T5>.Join(Expression<Func<T1, T2, T3, T4, T5, bool>> ON, string JoinTabName, EJoinType _EJoinType)
        {
            return this.Join(ON, JoinTabName, _EJoinType);
        }

        public override IEnumerable<T> ToList<T>()
        {
            return _DbHelper.Query<T>(this._Code.ToString(), this.GetSqlParameters());
        }

        public override T ToEntity<T>()
        {
            return _DbHelper.QueryFirstOrDefault<T>(this._Code.ToString(), this.GetSqlParameters());
        }

        public override DataTable ToTable()
        {
            return _DbHelper.QueryDataTable(this._Code.ToString(), this.GetSqlParameters());
        }

        public override SQL ToSQL()
        {
            return new SQL(this._Code.ToString(), this.GetSqlParameters());
        }

        public override Dictionary<string, object> GetSqlParameters()
        {
            return this._ParserArgs.SqlParameters;
        }

        public override void AddSqlParameters(string Key, object Value)
        {
            if (this._ParserArgs.SqlParameters.ContainsKey(Key)) throw new Exception("参数话 键" + Key + "已经存在！");
            this._ParserArgs.SqlParameters.Add(Key, Value);
        }






    }


    public abstract class AbstractQuery<T1, T2, T3, T4, T5, T6> : AbstractQuery, IQuery<T1, T2, T3, T4, T5, T6>
        where T1 : class,new()
        where T2 : class,new()
        where T3 : class,new()
        where T4 : class,new()
        where T5 : class,new()
        where T6 : class,new()
    {
        public AbstractQuery()
        { }

        public abstract IQuery<T1, T2, T3, T4, T5, T6> Select(Expression<Func<T1, T2, T3, T4, T5, T6, object>> Column);

        public abstract IQuery<T1, T2, T3, T4, T5, T6> Where(Expression<Func<T1, T2, T3, T4, T5, T6, bool>> Where);

        public abstract IQuery<T1, T2, T3, T4, T5, T6> WhereIF(bool IsWhere, Expression<Func<T1, T2, T3, T4, T5, T6, bool>> Where);

        public abstract IQuery<T1, T2, T3, T4, T5, T6> GroupBy(Expression<Func<T1, T2, T3, T4, T5, T6, object>> GroupBy);

        public abstract IQuery<T1, T2, T3, T4, T5, T6> OrderBy(Expression<Func<T1, T2, T3, T4, T5, T6, object>> OrderBy);

        public abstract IQuery<T1, T2, T3, T4, T5, T6> SqlString(string SQL, object Param);

        public abstract IQuery<T1, T2, T3, T4, T5, T6> Join(Expression<Func<T1, T2, T3, T4, T5, T6, bool>> ON, string JoinTabName, EJoinType _EJoinType);

        IQuery<T1, T2, T3, T4, T5, T6> IQuery<T1, T2, T3, T4, T5, T6>.Select(Expression<Func<T1, T2, T3, T4, T5, T6, object>> Column)
        {
            return this.Select(Column);
        }

        IQuery<T1, T2, T3, T4, T5, T6> IQuery<T1, T2, T3, T4, T5, T6>.Where(Expression<Func<T1, T2, T3, T4, T5, T6, bool>> Where)
        {
            return this.Where(Where);
        }

        IQuery<T1, T2, T3, T4, T5, T6> IQuery<T1, T2, T3, T4, T5, T6>.OrderBy(Expression<Func<T1, T2, T3, T4, T5, T6, object>> OrderBy)
        {
            return this.OrderBy(OrderBy);
        }

        IQuery<T1, T2, T3, T4, T5, T6> IQuery<T1, T2, T3, T4, T5, T6>.SqlString(string SQL, object Param)
        {
            return this.SqlString(SQL, Param);
        }

        IQuery<T1, T2, T3, T4, T5, T6> IQuery<T1, T2, T3, T4, T5, T6>.Join(Expression<Func<T1, T2, T3, T4, T5, T6, bool>> ON, string JoinTabName, EJoinType _EJoinType)
        {
            return this.Join(ON, JoinTabName, _EJoinType);
        }

        public override IEnumerable<T> ToList<T>()
        {
            return _DbHelper.Query<T>(this._Code.ToString(), this.GetSqlParameters());
        }

        public override T ToEntity<T>()
        {
            return _DbHelper.QueryFirstOrDefault<T>(this._Code.ToString(), this.GetSqlParameters());
        }

        public override DataTable ToTable()
        {
            return _DbHelper.QueryDataTable(this._Code.ToString(), this.GetSqlParameters());
        }

        public override SQL ToSQL()
        {
            return new SQL(this._Code.ToString(), this.GetSqlParameters());
        }

        public override Dictionary<string, object> GetSqlParameters()
        {
            return this._ParserArgs.SqlParameters;
        }

        public override void AddSqlParameters(string Key, object Value)
        {
            if (this._ParserArgs.SqlParameters.ContainsKey(Key)) throw new Exception("参数话 键" + Key + "已经存在！");
            this._ParserArgs.SqlParameters.Add(Key, Value);
        }






    }


    public abstract class AbstractQuery<T1, T2, T3, T4, T5, T6, T7> : AbstractQuery, IQuery<T1, T2, T3, T4, T5, T6, T7>
        where T1 : class,new()
        where T2 : class,new()
        where T3 : class,new()
        where T4 : class,new()
        where T5 : class,new()
        where T6 : class,new()
        where T7 : class,new()
    {
        public AbstractQuery()
        { }

        public abstract IQuery<T1, T2, T3, T4, T5, T6, T7> Select(Expression<Func<T1, T2, T3, T4, T5, T6, T7, object>> Column);

        public abstract IQuery<T1, T2, T3, T4, T5, T6, T7> Where(Expression<Func<T1, T2, T3, T4, T5, T6, T7, bool>> Where);

        public abstract IQuery<T1, T2, T3, T4, T5, T6, T7> WhereIF(bool IsWhere, Expression<Func<T1, T2, T3, T4, T5, T6, T7, bool>> Where);

        public abstract IQuery<T1, T2, T3, T4, T5, T6, T7> GroupBy(Expression<Func<T1, T2, T3, T4, T5, T6, T7, object>> GroupBy);

        public abstract IQuery<T1, T2, T3, T4, T5, T6, T7> OrderBy(Expression<Func<T1, T2, T3, T4, T5, T6, T7, object>> OrderBy);

        public abstract IQuery<T1, T2, T3, T4, T5, T6, T7> SqlString(string SQL, object Param);

        public abstract IQuery<T1, T2, T3, T4, T5, T6, T7> Join(Expression<Func<T1, T2, T3, T4, T5, T6, T7, bool>> ON, string JoinTabName, EJoinType _EJoinType);

        IQuery<T1, T2, T3, T4, T5, T6, T7> IQuery<T1, T2, T3, T4, T5, T6, T7>.Select(Expression<Func<T1, T2, T3, T4, T5, T6, T7, object>> Column)
        {
            return this.Select(Column);
        }

        IQuery<T1, T2, T3, T4, T5, T6, T7> IQuery<T1, T2, T3, T4, T5, T6, T7>.Where(Expression<Func<T1, T2, T3, T4, T5, T6, T7, bool>> Where)
        {
            return this.Where(Where);
        }

        IQuery<T1, T2, T3, T4, T5, T6, T7> IQuery<T1, T2, T3, T4, T5, T6, T7>.OrderBy(Expression<Func<T1, T2, T3, T4, T5, T6, T7, object>> OrderBy)
        {
            return this.OrderBy(OrderBy);
        }

        IQuery<T1, T2, T3, T4, T5, T6, T7> IQuery<T1, T2, T3, T4, T5, T6, T7>.SqlString(string SQL, object Param)
        {
            return this.SqlString(SQL, Param);
        }

        IQuery<T1, T2, T3, T4, T5, T6, T7> IQuery<T1, T2, T3, T4, T5, T6, T7>.Join(Expression<Func<T1, T2, T3, T4, T5, T6, T7, bool>> ON, string JoinTabName, EJoinType _EJoinType)
        {
            return this.Join(ON, JoinTabName, _EJoinType);
        }

        public override IEnumerable<T> ToList<T>()
        {
            return _DbHelper.Query<T>(this._Code.ToString(), this.GetSqlParameters());
        }

        public override T ToEntity<T>()
        {
            return _DbHelper.QueryFirstOrDefault<T>(this._Code.ToString(), this.GetSqlParameters());
        }

        public override DataTable ToTable()
        {
            return _DbHelper.QueryDataTable(this._Code.ToString(), this.GetSqlParameters());
        }

        public override SQL ToSQL()
        {
            return new SQL(this._Code.ToString(), this.GetSqlParameters());
        }

        public override Dictionary<string, object> GetSqlParameters()
        {
            return this._ParserArgs.SqlParameters;
        }

        public override void AddSqlParameters(string Key, object Value)
        {
            if (this._ParserArgs.SqlParameters.ContainsKey(Key)) throw new Exception("参数话 键" + Key + "已经存在！");
            this._ParserArgs.SqlParameters.Add(Key, Value);
        }






    }


    public abstract class AbstractQuery<T1, T2, T3, T4, T5, T6, T7, T8> : AbstractQuery, IQuery<T1, T2, T3, T4, T5, T6, T7, T8>
        where T1 : class,new()
        where T2 : class,new()
        where T3 : class,new()
        where T4 : class,new()
        where T5 : class,new()
        where T6 : class,new()
        where T7 : class,new()
        where T8 : class,new()
    {
        public AbstractQuery()
        { }

        public abstract IQuery<T1, T2, T3, T4, T5, T6, T7, T8> Select(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, object>> Column);

        public abstract IQuery<T1, T2, T3, T4, T5, T6, T7, T8> Where(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, bool>> Where);

        public abstract IQuery<T1, T2, T3, T4, T5, T6, T7, T8> WhereIF(bool IsWhere, Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, bool>> Where);

        public abstract IQuery<T1, T2, T3, T4, T5, T6, T7, T8> GroupBy(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, object>> GroupBy);

        public abstract IQuery<T1, T2, T3, T4, T5, T6, T7, T8> OrderBy(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, object>> OrderBy);

        public abstract IQuery<T1, T2, T3, T4, T5, T6, T7, T8> SqlString(string SQL, object Param);

        public abstract IQuery<T1, T2, T3, T4, T5, T6, T7, T8> Join(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, bool>> ON, string JoinTabName, EJoinType _EJoinType);

        IQuery<T1, T2, T3, T4, T5, T6, T7, T8> IQuery<T1, T2, T3, T4, T5, T6, T7, T8>.Select(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, object>> Column)
        {
            return this.Select(Column);
        }

        IQuery<T1, T2, T3, T4, T5, T6, T7, T8> IQuery<T1, T2, T3, T4, T5, T6, T7, T8>.Where(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, bool>> Where)
        {
            return this.Where(Where);
        }

        IQuery<T1, T2, T3, T4, T5, T6, T7, T8> IQuery<T1, T2, T3, T4, T5, T6, T7, T8>.OrderBy(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, object>> OrderBy)
        {
            return this.OrderBy(OrderBy);
        }

        IQuery<T1, T2, T3, T4, T5, T6, T7, T8> IQuery<T1, T2, T3, T4, T5, T6, T7, T8>.SqlString(string SQL, object Param)
        {
            return this.SqlString(SQL, Param);
        }

        IQuery<T1, T2, T3, T4, T5, T6, T7, T8> IQuery<T1, T2, T3, T4, T5, T6, T7, T8>.Join(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, bool>> ON, string JoinTabName, EJoinType _EJoinType)
        {
            return this.Join(ON, JoinTabName, _EJoinType);
        }

        public override IEnumerable<T> ToList<T>()
        {
            return _DbHelper.Query<T>(this._Code.ToString(), this.GetSqlParameters());
        }

        public override T ToEntity<T>()
        {
            return _DbHelper.QueryFirstOrDefault<T>(this._Code.ToString(), this.GetSqlParameters());
        }

        public override DataTable ToTable()
        {
            return _DbHelper.QueryDataTable(this._Code.ToString(), this.GetSqlParameters());
        }

        public override SQL ToSQL()
        {
            return new SQL(this._Code.ToString(), this.GetSqlParameters());
        }

        public override Dictionary<string, object> GetSqlParameters()
        {
            return this._ParserArgs.SqlParameters;
        }

        public override void AddSqlParameters(string Key, object Value)
        {
            if (this._ParserArgs.SqlParameters.ContainsKey(Key)) throw new Exception("参数话 键" + Key + "已经存在！");
            this._ParserArgs.SqlParameters.Add(Key, Value);
        }






    }


    public abstract class AbstractQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9> : AbstractQuery, IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9>
        where T1 : class,new()
        where T2 : class,new()
        where T3 : class,new()
        where T4 : class,new()
        where T5 : class,new()
        where T6 : class,new()
        where T7 : class,new()
        where T8 : class,new()
        where T9 : class,new()
    {
        public AbstractQuery()
        { }

        public abstract IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9> Select(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, object>> Column);

        public abstract IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9> Where(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, bool>> Where);

        public abstract IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9> WhereIF(bool IsWhere, Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, bool>> Where);

        public abstract IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9> GroupBy(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, object>> GroupBy);

        public abstract IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9> OrderBy(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, object>> OrderBy);

        public abstract IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9> SqlString(string SQL, object Param);

        public abstract IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9> Join(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, bool>> ON, string JoinTabName, EJoinType _EJoinType);

        IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9> IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9>.Select(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, object>> Column)
        {
            return this.Select(Column);
        }

        IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9> IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9>.Where(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, bool>> Where)
        {
            return this.Where(Where);
        }

        IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9> IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9>.OrderBy(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, object>> OrderBy)
        {
            return this.OrderBy(OrderBy);
        }

        IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9> IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9>.SqlString(string SQL, object Param)
        {
            return this.SqlString(SQL, Param);
        }

        IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9> IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9>.Join(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, bool>> ON, string JoinTabName, EJoinType _EJoinType)
        {
            return this.Join(ON, JoinTabName, _EJoinType);
        }

        public override IEnumerable<T> ToList<T>()
        {
            return _DbHelper.Query<T>(this._Code.ToString(), this.GetSqlParameters());
        }

        public override T ToEntity<T>()
        {
            return _DbHelper.QueryFirstOrDefault<T>(this._Code.ToString(), this.GetSqlParameters());
        }

        public override DataTable ToTable()
        {
            return _DbHelper.QueryDataTable(this._Code.ToString(), this.GetSqlParameters());
        }

        public override SQL ToSQL()
        {
            return new SQL(this._Code.ToString(), this.GetSqlParameters());
        }

        public override Dictionary<string, object> GetSqlParameters()
        {
            return this._ParserArgs.SqlParameters;
        }

        public override void AddSqlParameters(string Key, object Value)
        {
            if (this._ParserArgs.SqlParameters.ContainsKey(Key)) throw new Exception("参数话 键" + Key + "已经存在！");
            this._ParserArgs.SqlParameters.Add(Key, Value);
        }






    }


    public abstract class AbstractQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> : AbstractQuery, IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>
        where T1 : class,new()
        where T2 : class,new()
        where T3 : class,new()
        where T4 : class,new()
        where T5 : class,new()
        where T6 : class,new()
        where T7 : class,new()
        where T8 : class,new()
        where T9 : class,new()
        where T10 : class,new()
    {
        public AbstractQuery()
        { }

        public abstract IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> Select(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, object>> Column);

        public abstract IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> Where(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, bool>> Where);

        public abstract IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> WhereIF(bool IsWhere, Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, bool>> Where);

        public abstract IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> GroupBy(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, object>> GroupBy);

        public abstract IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> OrderBy(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, object>> OrderBy);

        public abstract IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> SqlString(string SQL, object Param);

        public abstract IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> Join(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, bool>> ON, string JoinTabName, EJoinType _EJoinType);

        IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>.Select(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, object>> Column)
        {
            return this.Select(Column);
        }

        IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>.Where(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, bool>> Where)
        {
            return this.Where(Where);
        }

        IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>.OrderBy(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, object>> OrderBy)
        {
            return this.OrderBy(OrderBy);
        }

        IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>.SqlString(string SQL, object Param)
        {
            return this.SqlString(SQL, Param);
        }

        IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>.Join(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, bool>> ON, string JoinTabName, EJoinType _EJoinType)
        {
            return this.Join(ON, JoinTabName, _EJoinType);
        }

        public override IEnumerable<T> ToList<T>()
        {
            return _DbHelper.Query<T>(this._Code.ToString(), this.GetSqlParameters());
        }

        public override T ToEntity<T>()
        {
            return _DbHelper.QueryFirstOrDefault<T>(this._Code.ToString(), this.GetSqlParameters());
        }

        public override DataTable ToTable()
        {
            return _DbHelper.QueryDataTable(this._Code.ToString(), this.GetSqlParameters());
        }

        public override SQL ToSQL()
        {
            return new SQL(this._Code.ToString(), this.GetSqlParameters());
        }

        public override Dictionary<string, object> GetSqlParameters()
        {
            return this._ParserArgs.SqlParameters;
        }

        public override void AddSqlParameters(string Key, object Value)
        {
            if (this._ParserArgs.SqlParameters.ContainsKey(Key)) throw new Exception("参数话 键" + Key + "已经存在！");
            this._ParserArgs.SqlParameters.Add(Key, Value);
        }






    }


    public abstract class AbstractQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> : AbstractQuery, IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>
        where T1 : class,new()
        where T2 : class,new()
        where T3 : class,new()
        where T4 : class,new()
        where T5 : class,new()
        where T6 : class,new()
        where T7 : class,new()
        where T8 : class,new()
        where T9 : class,new()
        where T10 : class,new()
        where T11 : class,new()
    {
        public AbstractQuery()
        { }

        public abstract IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> Select(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, object>> Column);

        public abstract IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> Where(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, bool>> Where);

        public abstract IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> WhereIF(bool IsWhere, Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, bool>> Where);

        public abstract IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> GroupBy(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, object>> GroupBy);

        public abstract IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> OrderBy(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, object>> OrderBy);

        public abstract IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> SqlString(string SQL, object Param);

        public abstract IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> Join(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, bool>> ON, string JoinTabName, EJoinType _EJoinType);

        IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>.Select(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, object>> Column)
        {
            return this.Select(Column);
        }

        IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>.Where(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, bool>> Where)
        {
            return this.Where(Where);
        }

        IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>.OrderBy(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, object>> OrderBy)
        {
            return this.OrderBy(OrderBy);
        }

        IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>.SqlString(string SQL, object Param)
        {
            return this.SqlString(SQL, Param);
        }

        IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>.Join(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, bool>> ON, string JoinTabName, EJoinType _EJoinType)
        {
            return this.Join(ON, JoinTabName, _EJoinType);
        }

        public override IEnumerable<T> ToList<T>()
        {
            return _DbHelper.Query<T>(this._Code.ToString(), this.GetSqlParameters());
        }

        public override T ToEntity<T>()
        {
            return _DbHelper.QueryFirstOrDefault<T>(this._Code.ToString(), this.GetSqlParameters());
        }

        public override DataTable ToTable()
        {
            return _DbHelper.QueryDataTable(this._Code.ToString(), this.GetSqlParameters());
        }

        public override SQL ToSQL()
        {
            return new SQL(this._Code.ToString(), this.GetSqlParameters());
        }

        public override Dictionary<string, object> GetSqlParameters()
        {
            return this._ParserArgs.SqlParameters;
        }

        public override void AddSqlParameters(string Key, object Value)
        {
            if (this._ParserArgs.SqlParameters.ContainsKey(Key)) throw new Exception("参数话 键" + Key + "已经存在！");
            this._ParserArgs.SqlParameters.Add(Key, Value);
        }






    }


    public abstract class AbstractQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> : AbstractQuery, IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>
        where T1 : class,new()
        where T2 : class,new()
        where T3 : class,new()
        where T4 : class,new()
        where T5 : class,new()
        where T6 : class,new()
        where T7 : class,new()
        where T8 : class,new()
        where T9 : class,new()
        where T10 : class,new()
        where T11 : class,new()
        where T12 : class,new()
    {
        public AbstractQuery()
        { }

        public abstract IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> Select(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, object>> Column);

        public abstract IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> Where(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, bool>> Where);

        public abstract IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> WhereIF(bool IsWhere, Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, bool>> Where);

        public abstract IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> GroupBy(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, object>> GroupBy);

        public abstract IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> OrderBy(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, object>> OrderBy);

        public abstract IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> SqlString(string SQL, object Param);

        public abstract IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> Join(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, bool>> ON, string JoinTabName, EJoinType _EJoinType);

        IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>.Select(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, object>> Column)
        {
            return this.Select(Column);
        }

        IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>.Where(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, bool>> Where)
        {
            return this.Where(Where);
        }

        IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>.OrderBy(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, object>> OrderBy)
        {
            return this.OrderBy(OrderBy);
        }

        IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>.SqlString(string SQL, object Param)
        {
            return this.SqlString(SQL, Param);
        }

        IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>.Join(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, bool>> ON, string JoinTabName, EJoinType _EJoinType)
        {
            return this.Join(ON, JoinTabName, _EJoinType);
        }

        public override IEnumerable<T> ToList<T>()
        {
            return _DbHelper.Query<T>(this._Code.ToString(), this.GetSqlParameters());
        }

        public override T ToEntity<T>()
        {
            return _DbHelper.QueryFirstOrDefault<T>(this._Code.ToString(), this.GetSqlParameters());
        }

        public override DataTable ToTable()
        {
            return _DbHelper.QueryDataTable(this._Code.ToString(), this.GetSqlParameters());
        }

        public override SQL ToSQL()
        {
            return new SQL(this._Code.ToString(), this.GetSqlParameters());
        }

        public override Dictionary<string, object> GetSqlParameters()
        {
            return this._ParserArgs.SqlParameters;
        }

        public override void AddSqlParameters(string Key, object Value)
        {
            if (this._ParserArgs.SqlParameters.ContainsKey(Key)) throw new Exception("参数话 键" + Key + "已经存在！");
            this._ParserArgs.SqlParameters.Add(Key, Value);
        }






    }


    public abstract class AbstractQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> : AbstractQuery, IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>
        where T1 : class,new()
        where T2 : class,new()
        where T3 : class,new()
        where T4 : class,new()
        where T5 : class,new()
        where T6 : class,new()
        where T7 : class,new()
        where T8 : class,new()
        where T9 : class,new()
        where T10 : class,new()
        where T11 : class,new()
        where T12 : class,new()
        where T13 : class,new()
    {
        public AbstractQuery()
        { }

        public abstract IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> Select(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, object>> Column);

        public abstract IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> Where(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, bool>> Where);

        public abstract IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> WhereIF(bool IsWhere, Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, bool>> Where);

        public abstract IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> GroupBy(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, object>> GroupBy);

        public abstract IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> OrderBy(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, object>> OrderBy);

        public abstract IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> SqlString(string SQL, object Param);

        public abstract IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> Join(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, bool>> ON, string JoinTabName, EJoinType _EJoinType);

        IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>.Select(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, object>> Column)
        {
            return this.Select(Column);
        }

        IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>.Where(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, bool>> Where)
        {
            return this.Where(Where);
        }

        IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>.OrderBy(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, object>> OrderBy)
        {
            return this.OrderBy(OrderBy);
        }

        IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>.SqlString(string SQL, object Param)
        {
            return this.SqlString(SQL, Param);
        }

        IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>.Join(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, bool>> ON, string JoinTabName, EJoinType _EJoinType)
        {
            return this.Join(ON, JoinTabName, _EJoinType);
        }

        public override IEnumerable<T> ToList<T>()
        {
            return _DbHelper.Query<T>(this._Code.ToString(), this.GetSqlParameters());
        }

        public override T ToEntity<T>()
        {
            return _DbHelper.QueryFirstOrDefault<T>(this._Code.ToString(), this.GetSqlParameters());
        }

        public override DataTable ToTable()
        {
            return _DbHelper.QueryDataTable(this._Code.ToString(), this.GetSqlParameters());
        }

        public override SQL ToSQL()
        {
            return new SQL(this._Code.ToString(), this.GetSqlParameters());
        }

        public override Dictionary<string, object> GetSqlParameters()
        {
            return this._ParserArgs.SqlParameters;
        }

        public override void AddSqlParameters(string Key, object Value)
        {
            if (this._ParserArgs.SqlParameters.ContainsKey(Key)) throw new Exception("参数话 键" + Key + "已经存在！");
            this._ParserArgs.SqlParameters.Add(Key, Value);
        }






    }


    public abstract class AbstractQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> : AbstractQuery, IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>
        where T1 : class,new()
        where T2 : class,new()
        where T3 : class,new()
        where T4 : class,new()
        where T5 : class,new()
        where T6 : class,new()
        where T7 : class,new()
        where T8 : class,new()
        where T9 : class,new()
        where T10 : class,new()
        where T11 : class,new()
        where T12 : class,new()
        where T13 : class,new()
        where T14 : class,new()
    {
        public AbstractQuery()
        { }

        public abstract IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> Select(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, object>> Column);

        public abstract IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> Where(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, bool>> Where);

        public abstract IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> WhereIF(bool IsWhere, Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, bool>> Where);

        public abstract IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> GroupBy(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, object>> GroupBy);

        public abstract IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> OrderBy(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, object>> OrderBy);

        public abstract IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> SqlString(string SQL, object Param);

        public abstract IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> Join(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, bool>> ON, string JoinTabName, EJoinType _EJoinType);

        IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>.Select(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, object>> Column)
        {
            return this.Select(Column);
        }

        IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>.Where(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, bool>> Where)
        {
            return this.Where(Where);
        }

        IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>.OrderBy(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, object>> OrderBy)
        {
            return this.OrderBy(OrderBy);
        }

        IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>.SqlString(string SQL, object Param)
        {
            return this.SqlString(SQL, Param);
        }

        IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>.Join(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, bool>> ON, string JoinTabName, EJoinType _EJoinType)
        {
            return this.Join(ON, JoinTabName, _EJoinType);
        }

        public override IEnumerable<T> ToList<T>()
        {
            return _DbHelper.Query<T>(this._Code.ToString(), this.GetSqlParameters());
        }

        public override T ToEntity<T>()
        {
            return _DbHelper.QueryFirstOrDefault<T>(this._Code.ToString(), this.GetSqlParameters());
        }

        public override DataTable ToTable()
        {
            return _DbHelper.QueryDataTable(this._Code.ToString(), this.GetSqlParameters());
        }

        public override SQL ToSQL()
        {
            return new SQL(this._Code.ToString(), this.GetSqlParameters());
        }

        public override Dictionary<string, object> GetSqlParameters()
        {
            return this._ParserArgs.SqlParameters;
        }

        public override void AddSqlParameters(string Key, object Value)
        {
            if (this._ParserArgs.SqlParameters.ContainsKey(Key)) throw new Exception("参数话 键" + Key + "已经存在！");
            this._ParserArgs.SqlParameters.Add(Key, Value);
        }






    }


    public abstract class AbstractQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> : AbstractQuery, IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>
        where T1 : class,new()
        where T2 : class,new()
        where T3 : class,new()
        where T4 : class,new()
        where T5 : class,new()
        where T6 : class,new()
        where T7 : class,new()
        where T8 : class,new()
        where T9 : class,new()
        where T10 : class,new()
        where T11 : class,new()
        where T12 : class,new()
        where T13 : class,new()
        where T14 : class,new()
        where T15 : class,new()
    {
        public AbstractQuery()
        { }

        public abstract IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> Select(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, object>> Column);

        public abstract IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> Where(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, bool>> Where);

        public abstract IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> WhereIF(bool IsWhere, Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, bool>> Where);

        public abstract IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> GroupBy(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, object>> GroupBy);

        public abstract IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> OrderBy(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, object>> OrderBy);

        public abstract IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> SqlString(string SQL, object Param);

        public abstract IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> Join(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, bool>> ON, string JoinTabName, EJoinType _EJoinType);

        IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>.Select(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, object>> Column)
        {
            return this.Select(Column);
        }

        IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>.Where(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, bool>> Where)
        {
            return this.Where(Where);
        }

        IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>.OrderBy(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, object>> OrderBy)
        {
            return this.OrderBy(OrderBy);
        }

        IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>.SqlString(string SQL, object Param)
        {
            return this.SqlString(SQL, Param);
        }

        IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>.Join(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, bool>> ON, string JoinTabName, EJoinType _EJoinType)
        {
            return this.Join(ON, JoinTabName, _EJoinType);
        }

        public override IEnumerable<T> ToList<T>()
        {
            return _DbHelper.Query<T>(this._Code.ToString(), this.GetSqlParameters());
        }

        public override T ToEntity<T>()
        {
            return _DbHelper.QueryFirstOrDefault<T>(this._Code.ToString(), this.GetSqlParameters());
        }

        public override DataTable ToTable()
        {
            return _DbHelper.QueryDataTable(this._Code.ToString(), this.GetSqlParameters());
        }

        public override SQL ToSQL()
        {
            return new SQL(this._Code.ToString(), this.GetSqlParameters());
        }

        public override Dictionary<string, object> GetSqlParameters()
        {
            return this._ParserArgs.SqlParameters;
        }

        public override void AddSqlParameters(string Key, object Value)
        {
            if (this._ParserArgs.SqlParameters.ContainsKey(Key)) throw new Exception("参数话 键" + Key + "已经存在！");
            this._ParserArgs.SqlParameters.Add(Key, Value);
        }






    }












}

