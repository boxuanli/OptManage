using System;
using System.Collections.Generic;
namespace DbFrame.SqlContext.Interface
{
    using DbFrame.Class;
    using System.Linq.Expressions;
    /// <summary>
    /// DELETE FROM 表名称 WHERE 列名称 = 值
    /// </summary>
    public interface IDelete
    {
        //根据 拉姆达表达式 作为 Where 条件
        bool Delete<T>(Expression<Func<T, bool>> Where) where T : class, new();
        bool Delete<T>(Expression<Func<T, bool>> Where, List<SQL> li) where T : class, new();

        // 根据ID 作为 Where 条件
        bool DeleteById<T>(object Id) where T : class, new();
        bool DeleteById<T>(object Id, List<SQL> li) where T : class, new();

        //根据sql 语句操作
        bool Delete<T>(string WhereStr, object Param) where T : class, new();
        bool Delete<T>(string WhereStr, object Param, List<SQL> li) where T : class, new();

    }
}
