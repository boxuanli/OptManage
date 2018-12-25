namespace DAL.Class
{
    using System.Collections.Generic;
    using System.Reflection;
    using System.Data;
    using System.Linq;
    using DbFrame;
    using DbFrame.Class;
    using Common;
    using Models;
    using System;
    using DbFrame.SqlContext.Interface;
    using DbFrame.Attributes;

    public class BaseDAL
    {

        /// <summary>
        /// 登录 信息 对象
        /// </summary>
        protected Sys_AccountM Account = new Sys_AccountM();

        public BaseDAL()
        {
            Account = this.GetSession<Sys_AccountM>("Account");
        }

        protected DBContext db = new DBContext();

        public void SetSession(string key, object value)
        {
            Tools.SetSession(key, value);
        }

        public T GetSession<T>(string key)
        {
            return Tools.GetSession<T>(key);
        }

        public Sys_PagingEntity NewPagingEntity(Sys_PagingEntity pe, params object[] ArryEntity)
        {
            var dic = new Dictionary<string, object>();
            var list = new List<PropertyInfo>();
            var colNames = new List<Dictionary<string, string>>();
            ArryEntity.ToList().ForEach(item =>
            {
                //将所有实体里面的属性放入list中
                ReflexHelper.GetPropertyInfos(item.GetType()).ToList().ForEach(p =>
                {
                    list.Add(p);
                });
            });
            foreach (DataColumn dc in pe.Table.Columns)
            {
                dic = new Dictionary<string, object>();
                var col = new Dictionary<string, string>();
                var pro = list.Find(item => item.Name.Equals(dc.ColumnName));

                dic["field"] = dc.ColumnName;
                dic["align"] = "left";
                if (pro == null)
                {
                    dic["title"] = dc.ColumnName;
                    dic["visible"] = !dc.ColumnName.Equals("_ukid");
                    col.Add(dc.ColumnName, dc.ColumnName);
                }
                else
                {
                    //获取有特性标记的属性【获取字段别名（中文名称）】
                    var FiledConfig = pro.GetCustomAttribute(typeof(FieldAttribute)) as FieldAttribute;
                    dic["title"] = (FiledConfig.Alias == "" ? dc.ColumnName : FiledConfig.Alias);
                    dic["visible"] = true;
                    col.Add(dc.ColumnName, dic["title"].ToStr());
                }
                pe.ColNames.Add(col);
                pe.ColModel.Add(dic);
            }

            return pe;
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="SqlStr"></param>
        /// <param name="Page"></param>
        /// <param name="Rows"></param>
        /// <param name="Param"></param>
        /// <param name="ArryEntity"></param>
        /// <returns></returns>
        public Sys_PagingEntity FindPaging(string SqlStr, int Page, int Rows, object Param, params object[] ArryEntity)
        {
            var Total = 0;
            var _Sys_PagingEntity = new Sys_PagingEntity();
            var _FindPaging = db.FindPaging(SqlStr, Page, Rows, Param);
            Total = _FindPaging.Total;
            _Sys_PagingEntity.Table = _FindPaging.Table;
            _Sys_PagingEntity.Counts = Total;
            _Sys_PagingEntity.PageCount = (Total / Rows);
            _Sys_PagingEntity.List = _FindPaging.Table.ToList();
            return this.NewPagingEntity(_Sys_PagingEntity, ArryEntity);
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="Iquery"></param>
        /// <param name="Page"></param>
        /// <param name="Rows"></param>
        /// <param name="ArryEntity"></param>
        /// <returns></returns>
        public Sys_PagingEntity FindPaging(IQuery Iquery, int Page, int Rows, params object[] ArryEntity)
        {
            var _Sql = Iquery.ToSQL();
            return this.FindPaging(_Sql.Sql_Parameter, Page, Rows, _Sql.Parameter, ArryEntity);
        }

    }
}
