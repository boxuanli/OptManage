using System;
using System.Collections.Generic;

namespace DbFrame.Ado
{
    using Dapper;
    using System.Data;
    using DbFrame.Class;
    public class DbHelper : IDbHelper
    {
        public IDbConnection GetDbConnection()
        {
            if (EDataBaseType.SqlServer == DbConfig._EDataBaseType)
                return new SqlDataBase().GetDbConnection();
            else
                return DbConfig.GetDbConnection(DbConfig.ConnectionString);
        }

        /// <summary>
        /// 执行 Insert Delete Update
        /// </summary>
        /// <param name="SqlStr"></param>
        /// <param name="Param"></param>
        /// <returns></returns>
        public bool Execute(string SqlStr, object Param)
        {
            return this.GetDbConnection().Execute(SqlStr, Param) > 0;
        }

        /// <summary>
        /// 执行 Insert Delete Update 并且 返回 值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="SqlStr"></param>
        /// <param name="Param"></param>
        /// <returns></returns>
        public T ExecuteScalar<T>(string SqlStr, object Param)
        {
            return this.GetDbConnection().ExecuteScalar<T>(SqlStr, Param);
        }

        /// <summary>
        /// 查询单行单列数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="SqlStr"></param>
        /// <param name="Param"></param>
        /// <returns></returns>
        public T QuerySingleOrDefault<T>(string SqlStr, object Param)
        {
            return this.GetDbConnection().QuerySingleOrDefault<T>(SqlStr, Param);
        }

        /// <summary>
        /// 查询单行 数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="SqlStr"></param>
        /// <param name="Param"></param>
        /// <returns></returns>
        public T QueryFirstOrDefault<T>(string SqlStr, object Param)
        {
            return this.GetDbConnection().QueryFirstOrDefault<T>(SqlStr, Param);
        }

        /// <summary>
        /// 执行查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="SqlStr"></param>
        /// <param name="Param"></param>
        /// <returns></returns>
        public IEnumerable<T> Query<T>(string SqlStr, object Param, IDbTransaction transaction = null, bool buffered = true, int? commandTimeout = null, CommandType? commandType = null)
        {
            if (typeof(T).Name == new Dictionary<string, object>().GetType().Name)
            {
                return (IEnumerable<T>)(this.QueryDataTable(SqlStr, Param).ToList());
            }
            else
            {
                return this.GetDbConnection().Query<T>(SqlStr, Param, transaction, buffered, commandTimeout, commandType);
            }
        }

        public DataTable QueryDataTable(string SqlStr, object Param)
        {
            IDataReader _IDataReader = null;
            if (Param == null)
                _IDataReader = this.GetDbConnection().ExecuteReader(SqlStr);
            else
                _IDataReader = this.GetDbConnection().ExecuteReader(SqlStr, Param);
            return _IDataReader.ToDataTable();
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="SqlStr"></param>
        /// <param name="Page"></param>
        /// <param name="PageSize"></param>
        /// <param name="Param"></param>
        /// <returns></returns>
        public Paging QueryPaging(string SqlStr, int Page, int PageSize, object Param)
        {
            var _Paging = new Paging();

            if (EDataBaseType.SqlServer == DbConfig._EDataBaseType)
                _Paging = new SqlDataBase().FindPaging(SqlStr, Page, PageSize, Param);
            else
                _Paging = DbConfig.FindPaging(SqlStr, Page, PageSize, Param);

            return _Paging;
        }

        /// <summary>
        /// 提交事务
        /// </summary>
        /// <param name="li"></param>
        /// <param name="_CallBack"></param>
        /// <returns></returns>
        public bool Commit(List<SQL> li, Action<int, SQL, IDbTransaction> _CallBack = null)
        {
            var conn = this.GetDbConnection();
            conn.Open();
            using (var _BeginTransaction = conn.BeginTransaction())
            {
                try
                {
                    for (int i = 0; i < li.Count; i++)
                    {
                        var _SQL = li[i];
                        if (_CallBack != null) _CallBack(i, _SQL, _BeginTransaction);
                        conn.Execute(_SQL.Sql_Parameter, _SQL.Parameter, _BeginTransaction);
                    }
                    _BeginTransaction.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    _BeginTransaction.Rollback();
                    throw ex;
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public string GetLastInsertId()
        {
            var Str = "ID#";
            if (EDataBaseType.SqlServer == DbConfig._EDataBaseType)
                Str += "SELECT LAST_INSERT_ID()";
            else
                Str += DbConfig.GetLastInsertId;
            return Str;
        }

        public string FindMaxNumber(string TabName, string FieldNum, string Where, object Param)
        {
            var Str = "";
            if (EDataBaseType.SqlServer == DbConfig._EDataBaseType)
                Str = @"SELECT ISNULL(MAX(CONVERT(INT," + FieldNum + ")),0) FROM " + TabName + " WHERE 1=1 " + Where;
            else
                Str = DbConfig.FindMaxNumber(TabName, FieldNum, Where, Param);
            return Str;
        }



    }
}
