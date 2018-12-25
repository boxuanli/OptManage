using System;
using System.Collections.Generic;
namespace DbFrame.SqlContext.Abstract
{
    //
    using DbFrame.SqlContext.Interface;
    using DbFrame.Class;
    using System.Linq.Expressions;

    public abstract class AbstractDelete : BaseClass, IDelete
    {
        //根据 拉姆达表达式 作为 Where 条件
        public abstract bool Delete<T>(Expression<Func<T, bool>> Where) where T : class, new();
        public abstract bool Delete<T>(Expression<Func<T, bool>> Where, List<SQL> li) where T : class, new();

        // 根据ID 作为 Where 条件
        public abstract bool DeleteById<T>(object Id) where T : class, new();
        public abstract bool DeleteById<T>(object Id, List<SQL> li) where T : class, new();

        //根据sql 语句操作
        public abstract bool Delete<T>(string WhereStr, object Param) where T : class, new();
        public abstract bool Delete<T>(string WhereStr, object Param, List<SQL> li) where T : class, new();

        bool IDelete.Delete<T>(Expression<Func<T, bool>> Where)
        {
            return Delete<T>(Where);
        }

        bool IDelete.Delete<T>(Expression<Func<T, bool>> Where, List<Class.SQL> li)
        {
            return Delete<T>(Where, li);
        }


        bool IDelete.DeleteById<T>(object Id)
        {
            return DeleteById<T>(Id);
        }

        bool IDelete.DeleteById<T>(object Id, List<Class.SQL> li)
        {
            return DeleteById<T>(Id, li);
        }

        bool IDelete.Delete<T>(string WhereStr, object Param)
        {
            return Delete<T>(WhereStr, Param);
        }

        bool IDelete.Delete<T>(string WhereStr, object Param, List<Class.SQL> li)
        {
            return Delete<T>(WhereStr, Param, li);
        }



    }
}
