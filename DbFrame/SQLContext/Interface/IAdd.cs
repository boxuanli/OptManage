using System;
using System.Collections.Generic;
namespace DbFrame.SqlContext.Interface
{
    using DbFrame.Class;
    using System.Linq.Expressions;
    /// <summary>
    /// INSERT INTO table_name (列1, 列2,...) VALUES (值1, 值2,....)
    /// </summary>
    public interface IAdd
    {

        object Add<T>(T Model) where T : class, new();

        object Add<T>(T Model, List<SQL> li) where T : class, new();

        object Add<T>(Expression<Func<T>> Model) where T : class, new();

        object Add<T>(Expression<Func<T>> Model, List<SQL> li) where T : class, new();

        //根据 sql 语句 执行insert

        /// <summary>
        /// INSERT INTO table_name (列1, 列2,...) VALUES (值1, 值2,....)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="SqlStr">(列1, 列2,...) VALUES (值1, 值2,....)</param>
        /// <param name="Param"></param>
        /// <returns></returns>
        bool Add<T>(string SqlStr, object Param) where T : class, new();

        bool Add<T>(string SqlStr, object Param, List<SQL> li) where T : class, new();

        object AddIdentity<T>(T Model, object NewModel) where T : class, new();

        bool AddIdentity<T>(T Model, object NewModel, List<SQL> li) where T : class, new();

    }
}
