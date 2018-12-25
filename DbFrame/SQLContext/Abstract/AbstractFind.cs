using System;
using System.Collections.Generic;
namespace DbFrame.SqlContext.Abstract
{
    using System.Data;
    using System.Linq.Expressions;
    using DbFrame.SqlContext.Interface;
    using DbFrame.Class;

    public abstract class AbstractFind : BaseClass, IFind
    {


        public AbstractFind() { }

        /************************* 基础 函数***************************/
        public abstract T FindSingle<T>(string SqlStr, object Param);

        public abstract DataTable FindTable(string SqlStr, object Param);

        public abstract T Find<T>(string SqlStr, object Param) where T : class, new();

        public abstract IEnumerable<T> FindList<T>(string SqlStr, object Param);

        public abstract Paging FindPaging(string Sql, int Page, int PageSize, object Param);

        public abstract int FindMaxNumber(string TabName, string FieldNum, string Where, object Param);


        /************************* 表达式树 函数***************************/
        public abstract DataTable FindTable<T>(Expression<Func<T, bool>> Where, Expression<Func<T, object>> OrderBy) where T : class, new();

        public abstract T Find<T>(Expression<Func<T, bool>> Where) where T : class, new();

        public abstract T FindById<T>(object Id) where T : class, new();

        public abstract IEnumerable<T> FindList<T>(Expression<Func<T, bool>> Where, Expression<Func<T, object>> OrderBy) where T : class, new();


        /************************* 表达式树 函数 自定义 Select ***************************/
        public abstract DataTable FindTable<T>(Expression<Func<T, object>> Select, Expression<Func<T, bool>> Where, Expression<Func<T, object>> OrderBy) where T : class, new();

        public abstract TResult FindSingle<T, TResult>(Expression<Func<T, object>> Select, Expression<Func<T, bool>> Where) where T : class, new();

        public abstract T Find<T>(Expression<Func<T, object>> Select, Expression<Func<T, bool>> Where, Expression<Func<T, object>> OrderBy) where T : class, new();

        public abstract T FindById<T>(Expression<Func<T, object>> Select, object Id) where T : class, new();

        public abstract IEnumerable<T> FindList<T>(Expression<Func<T, object>> Select, Expression<Func<T, bool>> Where, Expression<Func<T, object>> OrderBy) where T : class, new();

        public abstract IEnumerable<TResult> FindList<T, TResult>(Expression<Func<T, object>> Select, Expression<Func<T, bool>> Where, Expression<Func<T, object>> OrderBy) where T : class, new();



        /************************* 基础 函数***************************/

        T IFind.FindSingle<T>(string SqlStr, object Param)
        {
            return this.FindSingle<T>(SqlStr, Param);
        }

        DataTable IFind.FindTable(string SqlStr, object Param)
        {
            return this.FindTable(SqlStr, Param);
        }

        T IFind.Find<T>(string SqlStr, object Param)
        {
            return this.Find<T>(SqlStr, Param);
        }

        IEnumerable<T> IFind.FindList<T>(string SqlStr, object Param)
        {
            return this.FindList<T>(SqlStr, Param);
        }

        Paging IFind.FindPaging(string Sql, int Page, int PageSize, object Param)
        {
            return this.FindPaging(Sql, Page, PageSize, Param);
        }

        int IFind.FindMaxNumber(string TabName, string FieldNum, string Where, object Param)
        {
            return this.FindMaxNumber(TabName, FieldNum, Where, Param);
        }



        /************************* 表达式树 函数***************************/
        DataTable IFind.FindTable<T>(Expression<Func<T, bool>> Where, Expression<Func<T, object>> OrderBy)
        {
            return this.FindTable<T>(Where, OrderBy);
        }

        T IFind.Find<T>(Expression<Func<T, bool>> Where)
        {
            return this.Find<T>(Where);
        }

        T IFind.FindById<T>(object Id)
        {
            return this.FindById<T>(Id);
        }

        IEnumerable<T> IFind.FindList<T>(Expression<Func<T, bool>> Where, Expression<Func<T, object>> OrderBy)
        {
            return this.FindList<T>(Where, OrderBy);
        }



        /************************* 表达式树 函数 自定义 Select ***************************/
        DataTable IFind.FindTable<T>(Expression<Func<T, object>> Select, Expression<Func<T, bool>> Where, Expression<Func<T, object>> OrderBy)
        {
            return this.FindTable<T>(Select, Where, OrderBy);
        }

        TResult IFind.FindSingle<T, TResult>(Expression<Func<T, object>> Select, Expression<Func<T, bool>> Where)
        {
            return this.FindSingle<T, TResult>(Select, Where);
        }

        T IFind.Find<T>(Expression<Func<T, object>> Select, Expression<Func<T, bool>> Where, Expression<Func<T, object>> OrderBy)
        {
            return this.Find<T>(Select, Where, OrderBy);
        }

        T IFind.FindById<T>(Expression<Func<T, object>> Select, object Id)
        {
            return this.FindById<T>(Select, Id);
        }

        IEnumerable<T> IFind.FindList<T>(Expression<Func<T, object>> Select, Expression<Func<T, bool>> Where, Expression<Func<T, object>> OrderBy)
        {
            return this.FindList<T>(Select, Where, OrderBy);
        }

        IEnumerable<TResult> IFind.FindList<T, TResult>(Expression<Func<T, object>> Select, Expression<Func<T, bool>> Where, Expression<Func<T, object>> OrderBy)
        {
            return this.FindList<T, TResult>(Select, Where, OrderBy);
        }






    }
}
