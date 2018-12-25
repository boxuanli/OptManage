namespace DbFrame.Ado
{
    using System;
    using System.Data;
    using System.Collections.Generic;
    using DbFrame.Class;
    using Dapper;
    public interface IDbHelper
    {
        /// <summary>
        /// 连接对象
        /// </summary>
        /// <returns></returns>
        IDbConnection GetDbConnection();

        /// <summary>
        /// 执行 Insert Delete Update
        /// </summary>
        /// <param name="SqlStr"></param>
        /// <param name="Param"></param>
        /// <returns></returns>
        bool Execute(string SqlStr, object Param);

        /// <summary>
        /// 执行 Insert Delete Update 并且 返回 值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="SqlStr"></param>
        /// <param name="Param"></param>
        /// <returns></returns>
        T ExecuteScalar<T>(string SqlStr, object Param);

        /// <summary>
        /// 查询单行单列数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="SqlStr"></param>
        /// <param name="Param"></param>
        /// <returns></returns>
        T QuerySingleOrDefault<T>(string SqlStr, object Param);

        /// <summary>
        /// 查询单行 数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="SqlStr"></param>
        /// <param name="Param"></param>
        /// <returns></returns>
        T QueryFirstOrDefault<T>(string SqlStr, object Param);

        /// <summary>
        /// 执行查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="command"></param>
        /// <returns></returns>
        //IEnumerable<T> Query<T>(CommandDefinition command);

        /// <summary>
        /// 执行查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="transaction"></param>
        /// <param name="buffered"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        IEnumerable<T> Query<T>(string sql, object param = null, IDbTransaction transaction = null, bool buffered = true, int? commandTimeout = null, CommandType? commandType = null);

        //public static IEnumerable<T> Query<T>(this IDbConnection cnn, CommandDefinition command);
        //public static IEnumerable<T> Query<T>(this IDbConnection cnn, string sql, object param = null, IDbTransaction transaction = null, bool buffered = true, int? commandTimeout = null, CommandType? commandType = null);
        //public static IEnumerable<dynamic> Query(this IDbConnection cnn, string sql, object param = null, IDbTransaction transaction = null, bool buffered = true, int? commandTimeout = null, CommandType? commandType = null);
        //public static IEnumerable<object> Query(this IDbConnection cnn, Type type, string sql, object param = null, IDbTransaction transaction = null, bool buffered = true, int? commandTimeout = null, CommandType? commandType = null);

        /// <summary>
        /// 执行查询 得到 DataTable
        /// </summary>
        /// <param name="SqlStr"></param>
        /// <param name="Param"></param>
        /// <returns></returns>
        DataTable QueryDataTable(string SqlStr, object Param);

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="SqlStr"></param>
        /// <param name="Page"></param>
        /// <param name="PageSize"></param>
        /// <param name="Param"></param>
        /// <returns></returns>
        Paging QueryPaging(string SqlStr, int Page, int PageSize, object Param);

        /// <summary>
        /// 提交事务
        /// </summary>
        /// <param name="li"></param>
        /// <param name="_CallBack"></param>
        /// <returns></returns>
        bool Commit(List<SQL> li, Action<int, SQL, IDbTransaction> _CallBack = null);

        /// <summary>
        /// 获取最后一次 插入的 ID SQL 语句
        /// </summary>
        /// <returns></returns>
        string GetLastInsertId();

        /// <summary>
        /// 获取最大编号
        /// </summary>
        /// <param name="TabName"></param>
        /// <param name="FieldNum"></param>
        /// <param name="Where"></param>
        /// <param name="Param"></param>
        /// <returns></returns>
        string FindMaxNumber(string TabName, string FieldNum, string Where, object Param);


    }
}
