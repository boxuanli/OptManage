using System;
using System.Collections.Generic;
namespace DbFrame.SqlContext.Interface
{
    using System.Linq.Expressions;
    using System.Data;
    using Class;
    public interface IFind
    {

        /************************* 基础 函数***************************/
        T FindSingle<T>(string SqlStr, object Param);

        DataTable FindTable(string SqlStr, object Param);

        T Find<T>(string SqlStr, object Param) where T : class, new();

        IEnumerable<T> FindList<T>(string SqlStr, object Param);

        Paging FindPaging(string Sql, int Page, int PageSize, object Param);

        int FindMaxNumber(string TabName, string FieldNum, string Where, object Param);


        /************************* 表达式树 函数***************************/
        DataTable FindTable<T>(Expression<Func<T, bool>> Where, Expression<Func<T, object>> OrderBy) where T : class, new();

        T Find<T>(Expression<Func<T, bool>> Where) where T : class, new();

        T FindById<T>(object Id) where T : class, new();

        IEnumerable<T> FindList<T>(Expression<Func<T, bool>> Where, Expression<Func<T, object>> OrderBy) where T : class, new();


        /************************* 表达式树 函数 自定义 Select ***************************/
        DataTable FindTable<T>(Expression<Func<T, object>> Select, Expression<Func<T, bool>> Where, Expression<Func<T, object>> OrderBy) where T : class, new();

        TResult FindSingle<T, TResult>(Expression<Func<T, object>> Select, Expression<Func<T, bool>> Where) where T : class, new();

        T Find<T>(Expression<Func<T, object>> Select, Expression<Func<T, bool>> Where, Expression<Func<T, object>> OrderBy) where T : class, new();

        T FindById<T>(Expression<Func<T, object>> Select, object Id) where T : class, new();

        IEnumerable<T> FindList<T>(Expression<Func<T, object>> Select, Expression<Func<T, bool>> Where, Expression<Func<T, object>> OrderBy) where T : class, new();

        IEnumerable<TResult> FindList<T, TResult>(Expression<Func<T, object>> Select, Expression<Func<T, bool>> Where, Expression<Func<T, object>> OrderBy) where T : class, new();






    }
}
