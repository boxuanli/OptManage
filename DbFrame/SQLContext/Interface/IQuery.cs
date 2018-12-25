
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbFrame.SqlContext.Interface
{
    using System.Data;
    using System.Linq.Expressions;
    using Class;

    public interface IQuery
    {
        /// <summary>
        /// 转换为 List<T> 对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IEnumerable<T> ToList<T>();
        /// <summary>
        /// 转换为 T 对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T ToEntity<T>();
        /// <summary>
        /// 转换为 DataTable 对象
        /// </summary>
        /// <returns></returns>
        DataTable ToTable();
        /// <summary>
        /// 转换为 SQL 对象
        /// </summary>
        /// <returns></returns>
        SQL ToSQL();
        /// <summary>
        /// 获取参数
        /// </summary>
        /// <returns></returns>
        Dictionary<string, object> GetSqlParameters();
        /// <summary>
        /// 添加参数
        /// </summary>
        /// <param name="Key"></param>
        /// <param name="Value"></param>
        void AddSqlParameters(string Key, object Value);
    }


    public interface IQuery<T1> : IQuery
    where T1 : class,new()
    {
        IQuery<T1> Select(Expression<Func<T1, object>> Select);

        IQuery<T1> Where(Expression<Func<T1, bool>> Where);

        IQuery<T1> WhereIF(bool IsWhere, Expression<Func<T1, bool>> Where);

        IQuery<T1> OrderBy(Expression<Func<T1, object>> OrderBy);

        IQuery<T1> GroupBy(Expression<Func<T1, object>> GroupBy);

        IQuery<T1> SqlString(string SQL, object Param);

    }


    public interface IQuery<T1, T2> : IQuery
        where T1 : class,new()
        where T2 : class,new()
    {
        IQuery<T1, T2> Select(Expression<Func<T1, T2, object>> Select);

        IQuery<T1, T2> Where(Expression<Func<T1, T2, bool>> Where);

        IQuery<T1, T2> WhereIF(bool IsWhere, Expression<Func<T1, T2, bool>> Where);

        IQuery<T1, T2> OrderBy(Expression<Func<T1, T2, object>> OrderBy);

        IQuery<T1, T2> GroupBy(Expression<Func<T1, T2, object>> GroupBy);

        IQuery<T1, T2> SqlString(string SQL, object Param);

        IQuery<T1, T2> Join(Expression<Func<T1, T2, bool>> ON, string JoinTabName, EJoinType _EJoinType = EJoinType.LEFT_JOIN);
    }


    public interface IQuery<T1, T2, T3> : IQuery
        where T1 : class,new()
        where T2 : class,new()
        where T3 : class,new()
    {
        IQuery<T1, T2, T3> Select(Expression<Func<T1, T2, T3, object>> Select);

        IQuery<T1, T2, T3> Where(Expression<Func<T1, T2, T3, bool>> Where);

        IQuery<T1, T2, T3> WhereIF(bool IsWhere, Expression<Func<T1, T2, T3, bool>> Where);

        IQuery<T1, T2, T3> OrderBy(Expression<Func<T1, T2, T3, object>> OrderBy);

        IQuery<T1, T2, T3> GroupBy(Expression<Func<T1, T2, T3, object>> GroupBy);

        IQuery<T1, T2, T3> SqlString(string SQL, object Param);

        IQuery<T1, T2, T3> Join(Expression<Func<T1, T2, T3, bool>> ON, string JoinTabName, EJoinType _EJoinType = EJoinType.LEFT_JOIN);
    }


    public interface IQuery<T1, T2, T3, T4> : IQuery
        where T1 : class,new()
        where T2 : class,new()
        where T3 : class,new()
        where T4 : class,new()
    {
        IQuery<T1, T2, T3, T4> Select(Expression<Func<T1, T2, T3, T4, object>> Select);

        IQuery<T1, T2, T3, T4> Where(Expression<Func<T1, T2, T3, T4, bool>> Where);

        IQuery<T1, T2, T3, T4> WhereIF(bool IsWhere, Expression<Func<T1, T2, T3, T4, bool>> Where);

        IQuery<T1, T2, T3, T4> OrderBy(Expression<Func<T1, T2, T3, T4, object>> OrderBy);

        IQuery<T1, T2, T3, T4> GroupBy(Expression<Func<T1, T2, T3, T4, object>> GroupBy);

        IQuery<T1, T2, T3, T4> SqlString(string SQL, object Param);

        IQuery<T1, T2, T3, T4> Join(Expression<Func<T1, T2, T3, T4, bool>> ON, string JoinTabName, EJoinType _EJoinType = EJoinType.LEFT_JOIN);
    }


    public interface IQuery<T1, T2, T3, T4, T5> : IQuery
        where T1 : class,new()
        where T2 : class,new()
        where T3 : class,new()
        where T4 : class,new()
        where T5 : class,new()
    {
        IQuery<T1, T2, T3, T4, T5> Select(Expression<Func<T1, T2, T3, T4, T5, object>> Select);

        IQuery<T1, T2, T3, T4, T5> Where(Expression<Func<T1, T2, T3, T4, T5, bool>> Where);

        IQuery<T1, T2, T3, T4, T5> WhereIF(bool IsWhere, Expression<Func<T1, T2, T3, T4, T5, bool>> Where);

        IQuery<T1, T2, T3, T4, T5> OrderBy(Expression<Func<T1, T2, T3, T4, T5, object>> OrderBy);

        IQuery<T1, T2, T3, T4, T5> GroupBy(Expression<Func<T1, T2, T3, T4, T5, object>> GroupBy);

        IQuery<T1, T2, T3, T4, T5> SqlString(string SQL, object Param);

        IQuery<T1, T2, T3, T4, T5> Join(Expression<Func<T1, T2, T3, T4, T5, bool>> ON, string JoinTabName, EJoinType _EJoinType = EJoinType.LEFT_JOIN);
    }


    public interface IQuery<T1, T2, T3, T4, T5, T6> : IQuery
        where T1 : class,new()
        where T2 : class,new()
        where T3 : class,new()
        where T4 : class,new()
        where T5 : class,new()
        where T6 : class,new()
    {
        IQuery<T1, T2, T3, T4, T5, T6> Select(Expression<Func<T1, T2, T3, T4, T5, T6, object>> Select);

        IQuery<T1, T2, T3, T4, T5, T6> Where(Expression<Func<T1, T2, T3, T4, T5, T6, bool>> Where);

        IQuery<T1, T2, T3, T4, T5, T6> WhereIF(bool IsWhere, Expression<Func<T1, T2, T3, T4, T5, T6, bool>> Where);

        IQuery<T1, T2, T3, T4, T5, T6> OrderBy(Expression<Func<T1, T2, T3, T4, T5, T6, object>> OrderBy);

        IQuery<T1, T2, T3, T4, T5, T6> GroupBy(Expression<Func<T1, T2, T3, T4, T5, T6, object>> GroupBy);

        IQuery<T1, T2, T3, T4, T5, T6> SqlString(string SQL, object Param);

        IQuery<T1, T2, T3, T4, T5, T6> Join(Expression<Func<T1, T2, T3, T4, T5, T6, bool>> ON, string JoinTabName, EJoinType _EJoinType = EJoinType.LEFT_JOIN);
    }


    public interface IQuery<T1, T2, T3, T4, T5, T6, T7> : IQuery
        where T1 : class,new()
        where T2 : class,new()
        where T3 : class,new()
        where T4 : class,new()
        where T5 : class,new()
        where T6 : class,new()
        where T7 : class,new()
    {
        IQuery<T1, T2, T3, T4, T5, T6, T7> Select(Expression<Func<T1, T2, T3, T4, T5, T6, T7, object>> Select);

        IQuery<T1, T2, T3, T4, T5, T6, T7> Where(Expression<Func<T1, T2, T3, T4, T5, T6, T7, bool>> Where);

        IQuery<T1, T2, T3, T4, T5, T6, T7> WhereIF(bool IsWhere, Expression<Func<T1, T2, T3, T4, T5, T6, T7, bool>> Where);

        IQuery<T1, T2, T3, T4, T5, T6, T7> OrderBy(Expression<Func<T1, T2, T3, T4, T5, T6, T7, object>> OrderBy);

        IQuery<T1, T2, T3, T4, T5, T6, T7> GroupBy(Expression<Func<T1, T2, T3, T4, T5, T6, T7, object>> GroupBy);

        IQuery<T1, T2, T3, T4, T5, T6, T7> SqlString(string SQL, object Param);

        IQuery<T1, T2, T3, T4, T5, T6, T7> Join(Expression<Func<T1, T2, T3, T4, T5, T6, T7, bool>> ON, string JoinTabName, EJoinType _EJoinType = EJoinType.LEFT_JOIN);
    }


    public interface IQuery<T1, T2, T3, T4, T5, T6, T7, T8> : IQuery
        where T1 : class,new()
        where T2 : class,new()
        where T3 : class,new()
        where T4 : class,new()
        where T5 : class,new()
        where T6 : class,new()
        where T7 : class,new()
        where T8 : class,new()
    {
        IQuery<T1, T2, T3, T4, T5, T6, T7, T8> Select(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, object>> Select);

        IQuery<T1, T2, T3, T4, T5, T6, T7, T8> Where(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, bool>> Where);

        IQuery<T1, T2, T3, T4, T5, T6, T7, T8> WhereIF(bool IsWhere, Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, bool>> Where);

        IQuery<T1, T2, T3, T4, T5, T6, T7, T8> OrderBy(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, object>> OrderBy);

        IQuery<T1, T2, T3, T4, T5, T6, T7, T8> GroupBy(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, object>> GroupBy);

        IQuery<T1, T2, T3, T4, T5, T6, T7, T8> SqlString(string SQL, object Param);

        IQuery<T1, T2, T3, T4, T5, T6, T7, T8> Join(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, bool>> ON, string JoinTabName, EJoinType _EJoinType = EJoinType.LEFT_JOIN);
    }


    public interface IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9> : IQuery
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
        IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9> Select(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, object>> Select);

        IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9> Where(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, bool>> Where);

        IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9> WhereIF(bool IsWhere, Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, bool>> Where);

        IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9> OrderBy(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, object>> OrderBy);

        IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9> GroupBy(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, object>> GroupBy);

        IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9> SqlString(string SQL, object Param);

        IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9> Join(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, bool>> ON, string JoinTabName, EJoinType _EJoinType = EJoinType.LEFT_JOIN);
    }


    public interface IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> : IQuery
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
        IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> Select(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, object>> Select);

        IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> Where(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, bool>> Where);

        IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> WhereIF(bool IsWhere, Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, bool>> Where);

        IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> OrderBy(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, object>> OrderBy);

        IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> GroupBy(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, object>> GroupBy);

        IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> SqlString(string SQL, object Param);

        IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> Join(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, bool>> ON, string JoinTabName, EJoinType _EJoinType = EJoinType.LEFT_JOIN);
    }


    public interface IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> : IQuery
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
        IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> Select(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, object>> Select);

        IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> Where(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, bool>> Where);

        IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> WhereIF(bool IsWhere, Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, bool>> Where);

        IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> OrderBy(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, object>> OrderBy);

        IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> GroupBy(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, object>> GroupBy);

        IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> SqlString(string SQL, object Param);

        IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> Join(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, bool>> ON, string JoinTabName, EJoinType _EJoinType = EJoinType.LEFT_JOIN);
    }


    public interface IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> : IQuery
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
        IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> Select(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, object>> Select);

        IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> Where(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, bool>> Where);

        IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> WhereIF(bool IsWhere, Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, bool>> Where);

        IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> OrderBy(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, object>> OrderBy);

        IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> GroupBy(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, object>> GroupBy);

        IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> SqlString(string SQL, object Param);

        IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> Join(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, bool>> ON, string JoinTabName, EJoinType _EJoinType = EJoinType.LEFT_JOIN);
    }


    public interface IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> : IQuery
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
        IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> Select(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, object>> Select);

        IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> Where(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, bool>> Where);

        IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> WhereIF(bool IsWhere, Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, bool>> Where);

        IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> OrderBy(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, object>> OrderBy);

        IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> GroupBy(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, object>> GroupBy);

        IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> SqlString(string SQL, object Param);

        IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> Join(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, bool>> ON, string JoinTabName, EJoinType _EJoinType = EJoinType.LEFT_JOIN);
    }


    public interface IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> : IQuery
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
        IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> Select(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, object>> Select);

        IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> Where(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, bool>> Where);

        IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> WhereIF(bool IsWhere, Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, bool>> Where);

        IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> OrderBy(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, object>> OrderBy);

        IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> GroupBy(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, object>> GroupBy);

        IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> SqlString(string SQL, object Param);

        IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> Join(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, bool>> ON, string JoinTabName, EJoinType _EJoinType = EJoinType.LEFT_JOIN);
    }


    public interface IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> : IQuery
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
        IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> Select(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, object>> Select);

        IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> Where(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, bool>> Where);

        IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> WhereIF(bool IsWhere, Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, bool>> Where);

        IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> OrderBy(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, object>> OrderBy);

        IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> GroupBy(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, object>> GroupBy);

        IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> SqlString(string SQL, object Param);

        IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> Join(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, bool>> ON, string JoinTabName, EJoinType _EJoinType = EJoinType.LEFT_JOIN);
    }


}