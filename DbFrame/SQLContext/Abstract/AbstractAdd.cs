using System;
using System.Collections.Generic;
namespace DbFrame.SqlContext.Abstract
{
    //
    using DbFrame.SqlContext.Interface;
    using DbFrame.Class;
    using System.Linq.Expressions;

    public abstract class AbstractAdd : BaseClass, IAdd
    {
        public abstract object Add<T>(T Model) where T : class, new();
        public abstract object Add<T>(T Model, List<SQL> li) where T : class, new();
        public abstract object Add<T>(Expression<Func<T>> Model) where T : class, new();
        public abstract object Add<T>(Expression<Func<T>> Model, List<SQL> li) where T : class, new();
        public abstract bool Add<T>(string SqlStr, object Param) where T : class, new();
        public abstract bool Add<T>(string SqlStr, object Param, List<SQL> li) where T : class, new();
        public abstract object AddIdentity<T>(T Model, object NewModel) where T : class, new();
        public abstract bool AddIdentity<T>(T Model, object NewModel, List<SQL> li) where T : class, new();

        object IAdd.Add<T>(T Model)
        {
            return this.Add<T>(Model);
        }

        object IAdd.Add<T>(T Model, List<Class.SQL> li)
        {
            return this.Add<T>(Model, li);
        }

        object IAdd.Add<T>(Expression<Func<T>> Model)
        {
            return this.Add<T>(Model);
        }

        object IAdd.Add<T>(Expression<Func<T>> Model, List<SQL> li)
        {
            return this.Add<T>(Model, li);
        }

        bool IAdd.Add<T>(string SqlStr, object Param)
        {
            return this.Add<T>(SqlStr, Param);
        }

        bool IAdd.Add<T>(string SqlStr, object Param, List<SQL> li)
        {
            return this.Add<T>(SqlStr, Param, li);
        }

        object IAdd.AddIdentity<T>(T Model, object NewModel)
        {
            return this.AddIdentity<T>(Model, NewModel);
        }

        bool IAdd.AddIdentity<T>(T Model, object NewModel, List<SQL> li)
        {
            return this.AddIdentity<T>(Model, NewModel, li);
        }

    }
}
