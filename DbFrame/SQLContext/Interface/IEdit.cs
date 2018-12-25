using System;
using System.Collections.Generic;
namespace DbFrame.SqlContext.Interface
{
    using DbFrame.Class;
    using System.Linq.Expressions;
    /// <summary>
    /// UPDATE 表名称 SET 列名称 = 新值 WHERE 列名称 = 某值
    /// </summary>
    public interface IEdit
    {
        //根据 拉姆达表达式 作为 Where 条件
        bool Edit<T>(T Set, Expression<Func<T, bool>> Where) where T : class, new();

        bool Edit<T>(Expression<Func<T>> Set, Expression<Func<T, bool>> Where) where T : class, new();

        bool Edit<T>(T Set, Expression<Func<T, bool>> Where, List<SQL> li) where T : class, new();

        bool Edit<T>(Expression<Func<T>> Set, Expression<Func<T, bool>> Where, List<SQL> li) where T : class, new();


        //根据ID 作为 Where 条件
        bool EditById<T>(T Set) where T : class, new();

        bool EditById<T>(Expression<Func<T>> Set) where T : class, new();

        bool EditById<T>(T Set, List<SQL> li) where T : class, new();

        bool EditById<T>(Expression<Func<T>> Set, List<SQL> li) where T : class, new();

        //根据 对 set 更加灵活操作
        bool EditCustomSet<T>(Expression<Func<T, object>> Set, Expression<Func<T, bool>> Where) where T : class, new();

        bool EditCustomSet<T>(Expression<Func<T, object>> Set, Expression<Func<T, bool>> Where, List<SQL> li) where T : class, new();

    }
}
