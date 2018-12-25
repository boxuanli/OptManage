/*
    * 
    * 
    * 
    * 
    * DbFrame 数据访问框架
    * 
    * 作者：Hzy 开源地址：https://gitee.com/hao-zhi-ying/DbFrame
    * 
    * 
    * 
    * 
    */



























using System;
using System.Collections.Generic;
namespace DbFrame
{
    using System.Linq.Expressions;
    using System.Data;
    using Class;
    using Ado;
    using SqlContext;
    using SqlContext.Analysis;
    using SqlContext.Interface;

    public class DBContext : IAdd, IEdit, IDelete, IFind
    {
        protected AddAnalysis _AddAnalysis;
        protected EditAnalysis _EditAnalysis;
        protected DeleteAnalysis _DeleteAnalysis;
        protected FindAnalysis _FindAnalysis;
        public DbHelper _DbHelper;
        public string ErrorMessge = string.Empty;

        /// <summary>
        /// 数据库操作对象
        /// </summary>
        /// <param name="ConnectionString"></param>
        /// <param name="DataBaseType"></param>
        public DBContext()
        {
            if (string.IsNullOrEmpty(DbConfig.ConnectionString))
                throw new DbFrameException("请初始化 数据访问对象！");

            this._AddAnalysis = new AddAnalysis();
            this._EditAnalysis = new EditAnalysis();
            this._DeleteAnalysis = new DeleteAnalysis();
            this._FindAnalysis = new FindAnalysis();
            this._DbHelper = new DbHelper();
        }

        /// <summary>
        /// 初始化 数据访问对象
        /// </summary>
        /// <param name="ConnectionString"></param>
        /// <param name="DataBaseType"></param>
        public static void Initialization(string ConnectionString)
        {
            DbConfig.ConnectionString = ConnectionString;
        }

        /// <summary>
        /// 初始化 数据访问对象 ,其他数据库请使用改 初始化函数
        /// </summary>
        /// <param name="ConnectionString">连接字符串</param>
        /// <param name="LastInsertId">获取最后一次插入数据得自增id sql 语句</param>
        /// <param name="_IDbConnection">数据连接对象</param>
        /// <param name="FindPaging">分页查询逻辑部分</param>
        /// <param name="FindMaxNumber">查询最大编号 得 sql 语句</param>
        /// <param name="DataBaseType">数据库类型</param>
        public static void Initialization(
            string ConnectionString,
            string LastInsertId,
            Func<string, IDbConnection> _IDbConnection,
            Func<string, int, int, object, Paging> FindPaging,
            Func<string, string, string, object, string> FindMaxNumber)
        {
            DbConfig.ConnectionString = ConnectionString;
            DbConfig._EDataBaseType = EDataBaseType.Other;
            DbConfig.FindPaging = FindPaging;
            DbConfig.GetLastInsertId = LastInsertId;
            DbConfig.GetDbConnection = _IDbConnection;
            DbConfig.FindMaxNumber = FindMaxNumber;
        }

        /// <summary>
        /// 设置错误消息
        /// </summary>
        /// <param name="Error"></param>
        private void SetError(string Error)
        {
            ErrorMessge = string.Empty;
            ErrorMessge = Error.Replace("\r\n", "<br />");
            return;
        }

        /// <summary>
        /// Json 转换为 List <T>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Json"></param>
        /// <returns></returns>
        public List<T> JsonToList<T>(string Json)
        {
            T[] str = Json.DeserializeObject<T[]>() as T[];
            return new List<T>(str);
        }


        /// <summary>
        /// 验证实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool CheckModel<T>(T _Model) where T : class, new()
        {
            var _CheckAnalysis = new CheckAnalysis<T>();
            if (!_CheckAnalysis.Check(_Model))
            {
                SetError(_CheckAnalysis.ErrorMessage);
                return false;
            }
            return true;
        }




        /********************************************************/
        /************************数据库操作
        /********************************************************/


        /// <summary>
        /// 提交事务
        /// </summary>
        /// <returns></returns>
        public bool Commit(List<SQL> li, Action<int, Class.SQL, IDbTransaction> _CallBack = null)
        {
            if (_DbHelper.Commit(li, _CallBack))
                return true;
            SetError("操作失败");
            return false;
        }

        /// <summary>
        /// Insert
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Model"></param>
        /// <returns></returns>
        public object Add<T>(T Model) where T : class, new()
        {
            var key = _AddAnalysis.Add<T>(Model);
            if (string.IsNullOrEmpty(key.ToStr()))
            {
                SetError("操作失败");
                return null;
            }
            return key;
        }

        /// <summary>
        /// Insert
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Model"></param>
        /// <param name="li"></param>
        /// <returns></returns>
        public object Add<T>(T Model, List<SQL> li) where T : class, new()
        {
            var key = _AddAnalysis.Add<T>(Model, li);
            if (string.IsNullOrEmpty(key.ToStr()))
            {
                SetError("操作失败");
                return null;
            }
            return key;
        }

        /// <summary>
        /// insert
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="SqlStr">例如：(列1, 列2,...) VALUES (值1, 值2,....)</param>
        /// <param name="Param"></param>
        /// <returns></returns>
        public bool Add<T>(string SqlStr, object Param) where T : class, new()
        {
            if (_AddAnalysis.Add<T>(SqlStr, Param)) return true;
            SetError("操作失败");
            return false;
        }

        /// <summary>
        /// Insert
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Model"></param>
        /// <returns></returns>
        public object Add<T>(Expression<Func<T>> Model) where T : class, new()
        {
            var key = _AddAnalysis.Add<T>(Model);
            if (string.IsNullOrEmpty(key.ToStr()))
            {
                SetError("操作失败");
                return null;
            }
            return key;
        }

        /// <summary>
        /// Insert
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Model"></param>
        /// <param name="li"></param>
        /// <returns></returns>
        public object Add<T>(Expression<Func<T>> Model, List<SQL> li) where T : class, new()
        {
            var key = _AddAnalysis.Add<T>(Model, li);
            if (string.IsNullOrEmpty(key.ToStr()))
            {
                SetError("操作失败");
                return null;
            }
            return key;
        }

        /// <summary>
        /// insert
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="SqlStr">例如：(列1, 列2,...) VALUES (值1, 值2,....)</param>
        /// <param name="Param"></param>
        /// <param name="li"></param>
        /// <returns></returns>
        public bool Add<T>(string SqlStr, object Param, List<SQL> li) where T : class, new()
        {
            if (_AddAnalysis.Add<T>(SqlStr, Param, li)) return true;
            SetError("操作失败");
            return false;
        }

        /// <summary>
        /// insert 针对 int 类型主键并且自增
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Model"></param>
        /// <param name="NewModel">给外键赋值</param>
        /// <returns></returns>
        public object AddIdentity<T>(T Model, object NewModel) where T : class, new()
        {
            var key = _AddAnalysis.AddIdentity<T>(Model, NewModel);
            if (key == null) SetError("操作失败");
            return key;
        }

        /// <summary>
        /// insert 针对 int 类型主键并且自增
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Model"></param>
        /// <param name="NewModel">给外键赋值</param>
        /// <param name="li"></param>
        /// <returns></returns>
        public bool AddIdentity<T>(T Model, object NewModel, List<SQL> li) where T : class, new()
        {
            if (_AddAnalysis.AddIdentity<T>(Model, NewModel, li)) return true;
            SetError("操作失败");
            return false;
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Set"></param>
        /// <param name="Where"></param>
        /// <returns></returns>
        public bool Edit<T>(T Set, Expression<Func<T, bool>> Where) where T : class, new()
        {
            if (_EditAnalysis.Edit(Set, Where))
                return true;
            SetError("操作失败");
            return false;
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Set"></param>
        /// <param name="Where"></param>
        /// <returns></returns>
        public bool Edit<T>(Expression<Func<T>> Set, Expression<Func<T, bool>> Where) where T : class, new()
        {
            if (_EditAnalysis.Edit(Set, Where))
                return true;
            SetError("操作失败");
            return false;
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Set"></param>
        /// <param name="Where"></param>
        /// <param name="li"></param>
        /// <returns></returns>
        public bool Edit<T>(T Set, Expression<Func<T, bool>> Where, List<SQL> li) where T : class, new()
        {
            if (_EditAnalysis.Edit(Set, Where, li))
                return true;
            SetError("操作失败");
            return false;
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Set"></param>
        /// <param name="Where"></param>
        /// <param name="li"></param>
        /// <returns></returns>
        public bool Edit<T>(Expression<Func<T>> Set, Expression<Func<T, bool>> Where, List<SQL> li) where T : class, new()
        {
            if (_EditAnalysis.Edit(Set, Where, li))
                return true;
            SetError("操作失败");
            return false;
        }

        /// <summary>
        /// Update 根据ID
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Set"></param>
        /// <param name="Id"></param>
        /// <returns></returns>
        public bool EditById<T>(T Set) where T : class, new()
        {
            if (_EditAnalysis.EditById(Set))
                return true;
            SetError("操作失败");
            return false;
        }

        /// <summary>
        /// Update 根据ID
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Set"></param>
        /// <param name="Id"></param>
        /// <returns></returns>
        public bool EditById<T>(Expression<Func<T>> Set) where T : class, new()
        {
            if (_EditAnalysis.EditById(Set))
                return true;
            SetError("操作失败");
            return false;
        }

        /// <summary>
        /// Update 根据ID
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Set"></param>
        /// <param name="Id"></param>
        /// <param name="li"></param>
        /// <returns></returns>
        public bool EditById<T>(T Set, List<SQL> li) where T : class, new()
        {
            if (_EditAnalysis.EditById(Set, li))
                return true;
            SetError("操作失败");
            return false;
        }

        /// <summary>
        /// Update 根据ID
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Set"></param>
        /// <param name="Id"></param>
        /// <param name="li"></param>
        /// <returns></returns>
        public bool EditById<T>(Expression<Func<T>> Set, List<SQL> li) where T : class, new()
        {
            if (_EditAnalysis.EditById(Set, li))
                return true;
            SetError("操作失败");
            return false;
        }

        /// <summary>
        /// Update 根据sql 语句片段 做修改操作
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="SetStr"> Count+=12 </param>
        /// <param name="WhereStr"> and UserName like '%123%' </param>
        /// <param name="Param"></param>
        /// <returns></returns>
        public bool EditCustomSet<T>(Expression<Func<T, object>> Set, Expression<Func<T, bool>> Where) where T : class, new()
        {
            if (_EditAnalysis.EditCustomSet<T>(Set, Where))
                return true;
            SetError("操作失败");
            return false;
        }

        /// <summary>
        /// Update 根据sql 语句片段 做修改操作
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="SetStr"> Count+=12 </param>
        /// <param name="WhereStr"> and UserName like '%123%' </param>
        /// <param name="Param"></param>
        /// <param name="li"></param>
        /// <returns></returns>
        public bool EditCustomSet<T>(Expression<Func<T, object>> Set, Expression<Func<T, bool>> Where, List<SQL> li) where T : class, new()
        {
            if (_EditAnalysis.EditCustomSet<T>(Set, Where, li))
                return true;
            SetError("操作失败");
            return false;
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Where"></param>
        /// <returns></returns>
        public bool Delete<T>(Expression<Func<T, bool>> Where) where T : class, new()
        {
            if (_DeleteAnalysis.Delete<T>(Where))
                return true;
            SetError("操作失败");
            return false;
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Where"></param>
        /// <param name="li"></param>
        /// <returns></returns>
        public bool Delete<T>(Expression<Func<T, bool>> Where, List<SQL> li) where T : class, new()
        {
            if (_DeleteAnalysis.Delete<T>(Where, li))
                return true;
            SetError("操作失败");
            return false;
        }

        /// <summary>
        /// Delete 根据ID
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Id"></param>
        /// <returns></returns>
        public bool DeleteById<T>(object Id) where T : class, new()
        {
            if (_DeleteAnalysis.DeleteById<T>(Id))
                return true;
            SetError("操作失败");
            return false;
        }

        /// <summary>
        /// Delete 根据ID
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Id"></param>
        /// <param name="li"></param>
        /// <returns></returns>
        public bool DeleteById<T>(object Id, List<SQL> li) where T : class, new()
        {
            if (_DeleteAnalysis.DeleteById<T>(Id, li))
                return true;
            SetError("操作失败");
            return false;
        }

        /// <summary>
        /// Delete 根据 sql 语句片段 操作
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="WhereStr"> and UserName like '%123%' </param>
        /// <param name="Param"></param>
        /// <returns></returns>
        public bool Delete<T>(string WhereStr, object Param) where T : class, new()
        {
            if (_DeleteAnalysis.Delete<T>(WhereStr, Param))
                return true;
            SetError("操作失败");
            return false;
        }

        /// <summary>
        /// Delete 根据 sql 语句片段 操作
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="WhereStr"> and UserName like '%123%' </param>
        /// <param name="Param"></param>
        /// <param name="li"></param>
        /// <returns></returns>
        public bool Delete<T>(string WhereStr, object Param, List<SQL> li) where T : class, new()
        {
            if (_DeleteAnalysis.Delete<T>(WhereStr, Param, li))
                return true;
            SetError("操作失败");
            return false;
        }

        /// <summary>
        /// 查询得到 DataTable
        /// </summary>
        /// <param name="SqlStr"></param>
        /// <param name="Param"></param>
        /// <returns></returns>
        public DataTable FindTable(string SqlStr, object Param = null)
        {
            return _FindAnalysis.FindTable(SqlStr, Param);
        }

        /// <summary>
        /// 得到单行单列数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="SqlStr"></param>
        /// <param name="Param"></param>
        /// <returns></returns>
        public T FindSingle<T>(string SqlStr, object Param = null)
        {
            return _FindAnalysis.FindSingle<T>(SqlStr, Param);
        }

        /// <summary>
        /// 得到 实体 对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="SqlStr"></param>
        /// <param name="Param"></param>
        /// <returns></returns>
        public T Find<T>(string SqlStr, object Param = null) where T : class, new()
        {
            return _FindAnalysis.Find<T>(SqlStr, Param);
        }

        /// <summary>
        /// 得到 IEnumerable 集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="SqlStr"></param>
        /// <param name="Param"></param>
        /// <returns></returns>
        public IEnumerable<T> FindList<T>(string SqlStr, object Param = null)
        {
            return _FindAnalysis.FindList<T>(SqlStr, Param);
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="Sql"></param>
        /// <param name="Page"></param>
        /// <param name="PageSize"></param>
        /// <param name="Total"></param>
        /// <param name="Param"></param>
        /// <returns></returns>
        public Paging FindPaging(string Sql, int Page, int PageSize, object Param = null)
        {
            return _FindAnalysis.FindPaging(Sql, Page, PageSize, Param);
        }

        /// <summary>
        /// 得到最大编号
        /// </summary>
        /// <param name="TabName"></param>
        /// <param name="FieldNum"></param>
        /// <param name="Where"></param>
        /// <param name="Param"></param>
        /// <returns></returns>
        public int FindMaxNumber(string TabName, string FieldNum, string Where = "", object Param = null)
        {
            return _FindAnalysis.FindMaxNumber(TabName, FieldNum, Where, Param);
        }

        /// <summary>
        /// 得到 DataTable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Where"></param>
        /// <param name="OrderBy"></param>
        /// <returns></returns>
        public DataTable FindTable<T>(Expression<Func<T, bool>> Where, Expression<Func<T, object>> OrderBy) where T : class, new()
        {
            return _FindAnalysis.FindTable<T>(Where, OrderBy);
        }

        /// <summary>
        /// 得到 实体对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Where"></param>
        /// <returns></returns>
        public T Find<T>(Expression<Func<T, bool>> Where) where T : class, new()
        {
            return _FindAnalysis.Find<T>(Where);
        }

        /// <summary>
        /// 得到 实体对象 根据ID
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Id"></param>
        /// <returns></returns>
        public T FindById<T>(object Id) where T : class, new()
        {
            return _FindAnalysis.FindById<T>(Id);
        }

        /// <summary>
        /// 得到 IEnumerable 集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Where"></param>
        /// <param name="OrderBy"></param>
        /// <returns></returns>
        public IEnumerable<T> FindList<T>(Expression<Func<T, bool>> Where, Expression<Func<T, object>> OrderBy) where T : class, new()
        {
            return _FindAnalysis.FindList<T>(Where, OrderBy);
        }

        /// <summary>
        /// 得到 DataTable 可自定义 列
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Select"></param>
        /// <param name="Where"></param>
        /// <param name="OrderBy"></param>
        /// <returns></returns>
        public DataTable FindTable<T>(Expression<Func<T, object>> Select, Expression<Func<T, bool>> Where, Expression<Func<T, object>> OrderBy) where T : class, new()
        {
            return _FindAnalysis.FindTable<T>(Select, Where, OrderBy);
        }

        /// <summary>
        /// 得到单行单列 列通过表达式树设置
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="W"></typeparam>
        /// <param name="Select"></param>
        /// <param name="Where"></param>
        /// <returns></returns>
        public TResult FindSingle<T, TResult>(Expression<Func<T, object>> Select, Expression<Func<T, bool>> Where)
            where T : class, new()
        {
            return _FindAnalysis.FindSingle<T, TResult>(Select, Where);
        }

        /// <summary>
        /// 得到实体对象 可自定义列
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Select"></param>
        /// <param name="Where"></param>
        /// <returns></returns>
        public T Find<T>(Expression<Func<T, object>> Select, Expression<Func<T, bool>> Where, Expression<Func<T, object>> OrderBy = null) where T : class, new()
        {
            return _FindAnalysis.Find<T>(Select, Where, OrderBy);
        }

        /// <summary>
        /// 得到实体对象 可自定义列 根据ID
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Select"></param>
        /// <param name="Id"></param>
        /// <returns></returns>
        public T FindById<T>(Expression<Func<T, object>> Select, object Id) where T : class, new()
        {
            return _FindAnalysis.FindById<T>(Select, Id);
        }

        /// <summary>
        /// 得到 IEnumerable 集合 可自定义列
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Select"></param>
        /// <param name="Where"></param>
        /// <param name="OrderBy"></param>
        /// <returns></returns>
        public IEnumerable<T> FindList<T>(Expression<Func<T, object>> Select, Expression<Func<T, bool>> Where, Expression<Func<T, object>> OrderBy) where T : class, new()
        {
            return _FindAnalysis.FindList<T>(Select, Where, OrderBy);
        }

        /// <summary>
        /// 得到 IEnumerable 集合 可自定义列 并且选择返回对象类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="Select"></param>
        /// <param name="Where"></param>
        /// <param name="OrderBy"></param>
        /// <returns></returns>
        public IEnumerable<TResult> FindList<T, TResult>(Expression<Func<T, object>> Select, Expression<Func<T, bool>> Where, Expression<Func<T, object>> OrderBy) where T : class, new()
        {
            return _FindAnalysis.FindList<T, TResult>(Select, Where, OrderBy);
        }


        public IQuery<T1> Query<T1>(Expression<Func<T1, object>> Select)
          where T1 : class, new()
        {
            return _FindAnalysis.Query(Select);
        }

        public IQuery<T1, T2> Query<T1, T2>(Expression<Func<T1, T2, object>> Select)
            where T1 : class, new()
            where T2 : class, new()
        {
            return _FindAnalysis.Query(Select);
        }

        public IQuery<T1, T2, T3> Query<T1, T2, T3>(Expression<Func<T1, T2, T3, object>> Select)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
        {
            return _FindAnalysis.Query(Select);
        }

        public IQuery<T1, T2, T3, T4> Query<T1, T2, T3, T4>(Expression<Func<T1, T2, T3, T4, object>> Select)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where T4 : class, new()
        {
            return _FindAnalysis.Query(Select);
        }

        public IQuery<T1, T2, T3, T4, T5> Query<T1, T2, T3, T4, T5>(Expression<Func<T1, T2, T3, T4, T5, object>> Select)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where T4 : class, new()
            where T5 : class, new()
        {
            return _FindAnalysis.Query(Select);
        }

        public IQuery<T1, T2, T3, T4, T5, T6> Query<T1, T2, T3, T4, T5, T6>(Expression<Func<T1, T2, T3, T4, T5, T6, object>> Select)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where T4 : class, new()
            where T5 : class, new()
            where T6 : class, new()
        {
            return _FindAnalysis.Query(Select);
        }

        public IQuery<T1, T2, T3, T4, T5, T6, T7> Query<T1, T2, T3, T4, T5, T6, T7>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, object>> Select)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where T4 : class, new()
            where T5 : class, new()
            where T6 : class, new()
            where T7 : class, new()
        {
            return _FindAnalysis.Query(Select);
        }

        public IQuery<T1, T2, T3, T4, T5, T6, T7, T8> Query<T1, T2, T3, T4, T5, T6, T7, T8>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, object>> Select)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where T4 : class, new()
            where T5 : class, new()
            where T6 : class, new()
            where T7 : class, new()
            where T8 : class, new()
        {
            return _FindAnalysis.Query(Select);
        }

        public IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9> Query<T1, T2, T3, T4, T5, T6, T7, T8, T9>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, object>> Select)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where T4 : class, new()
            where T5 : class, new()
            where T6 : class, new()
            where T7 : class, new()
            where T8 : class, new()
            where T9 : class, new()
        {
            return _FindAnalysis.Query(Select);
        }

        public IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> Query<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, object>> Select)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where T4 : class, new()
            where T5 : class, new()
            where T6 : class, new()
            where T7 : class, new()
            where T8 : class, new()
            where T9 : class, new()
            where T10 : class, new()
        {
            return _FindAnalysis.Query(Select);
        }

        public IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> Query<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, object>> Select)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where T4 : class, new()
            where T5 : class, new()
            where T6 : class, new()
            where T7 : class, new()
            where T8 : class, new()
            where T9 : class, new()
            where T10 : class, new()
            where T11 : class, new()
        {
            return _FindAnalysis.Query(Select);
        }

        public IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> Query<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, object>> Select)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where T4 : class, new()
            where T5 : class, new()
            where T6 : class, new()
            where T7 : class, new()
            where T8 : class, new()
            where T9 : class, new()
            where T10 : class, new()
            where T11 : class, new()
            where T12 : class, new()
        {
            return _FindAnalysis.Query(Select);
        }

        public IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> Query<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, object>> Select)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where T4 : class, new()
            where T5 : class, new()
            where T6 : class, new()
            where T7 : class, new()
            where T8 : class, new()
            where T9 : class, new()
            where T10 : class, new()
            where T11 : class, new()
            where T12 : class, new()
            where T13 : class, new()
        {
            return _FindAnalysis.Query(Select);
        }

        public IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> Query<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, object>> Select)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where T4 : class, new()
            where T5 : class, new()
            where T6 : class, new()
            where T7 : class, new()
            where T8 : class, new()
            where T9 : class, new()
            where T10 : class, new()
            where T11 : class, new()
            where T12 : class, new()
            where T13 : class, new()
            where T14 : class, new()
        {
            return _FindAnalysis.Query(Select);
        }

        public IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> Query<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, object>> Select)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where T4 : class, new()
            where T5 : class, new()
            where T6 : class, new()
            where T7 : class, new()
            where T8 : class, new()
            where T9 : class, new()
            where T10 : class, new()
            where T11 : class, new()
            where T12 : class, new()
            where T13 : class, new()
            where T14 : class, new()
            where T15 : class, new()
        {
            return _FindAnalysis.Query(Select);
        }



    }
}
