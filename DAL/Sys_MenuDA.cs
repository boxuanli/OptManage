using System;
using System.Collections.Generic;
using System.Text;
//
using Common;
using DbFrame;
using DbFrame.Class;
using Models;
using DAL.Class;
using System.Collections;
//
using System.Data;

namespace DAL
{
    using System.Linq;

    public class Sys_MenuDA : BaseDAL
    {
        Sys_MenuM menuM = new Sys_MenuM();

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="query"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public Sys_PagingEntity GetDataSource(Hashtable query, int page, int rows)
        {
            var IQuery = db
                .Query<Sys_MenuM, Sys_MenuM>((a, b) => new
                {
                    a.Menu_Name,
                    a.Menu_Url,
                    父级菜单 = b.Menu_Name,
                    a.Menu_Num,
                    a.Menu_Icon,
                    SqlString = "case when a.Menu_IsShow=2 then '隐藏' else '显示' end Menu_IsShow",
                    a.Menu_CreateTime,
                    _ukid = a.Menu_ID
                })
                .Join((a, b) => a.Menu_ParentID == b.Menu_ID, "b")
                .WhereIF(string.IsNullOrEmpty(query["Menu_ID"].ToStr()), (a, b) => a.Menu_ParentID == null)
                .WhereIF(!string.IsNullOrEmpty(query["Menu_ID"].ToStr()), (a, b) => a.Menu_ParentID == query["Menu_ID"].ToGuid())
                .WhereIF(!string.IsNullOrEmpty(query["Menu_Name"].ToStr()), (a, b) => a.Menu_Name.Contains(query["Menu_Name"].ToStr()))
                .OrderBy((a, b) => new { a.Menu_Num });

            return this.FindPaging(IQuery, page, rows,
                new Sys_MenuM()
            );
        }

        /// <summary>
        /// 根据角色ID 获取菜单
        /// </summary>
        /// <returns></returns>
        public List<Sys_MenuM> GetMenuByRoleID()
        {
            if (this.Account.IsSuperManage) return db.FindList<Sys_MenuM>(null, orderby => new { orderby.Menu_Num }).ToList();

            return db.FindList<Sys_MenuM>(@"
                    select * from (
                        select Menu_ID, a.Menu_Num, Menu_Name, Menu_Url, Menu_Icon, a.Menu_ParentID, Menu_CreateTime,a.Menu_IsShow 
                        from (select * from Sys_Menu where 1=1 and Menu_Url is null or Menu_Url='') a
                            join (
	                        select Menu_Num,Menu_ParentID
		                        from [dbo].[Sys_RoleMenuFunction] 
		                        join Sys_Menu on Menu_ID=RoleMenuFunction_MenuID and RoleMenuFunction_RoleID='" + this.Account.RoleID.ToGuid() + @"' and Menu_IsShow<>2
		                        group by RoleMenuFunction_MenuID,RoleMenuFunction_RoleID,Menu_Num,Menu_ParentID
                        ) b on charindex(a.Menu_Num,b.Menu_Num)>0 and a.Menu_ID=b.Menu_ParentID
                        union
                        select Menu_ID, Menu_Num, Menu_Name, Menu_Url, Menu_Icon, Menu_ParentID, Menu_CreateTime,x.Menu_IsShow 
                        from Sys_Menu x
                        join (
	                        select RoleMenuFunction_MenuID,RoleMenuFunction_RoleID 
		                        from [dbo].[Sys_RoleMenuFunction] 
		                        group by RoleMenuFunction_MenuID,RoleMenuFunction_RoleID
                        ) y on x.Menu_ID=y.RoleMenuFunction_MenuID and RoleMenuFunction_RoleID='" + this.Account.RoleID.ToGuid() + @"' and Menu_IsShow<>2
                    ) tab
                    order by tab.Menu_Num asc
                                ").ToList();
        }

        /// <summary>
        /// 获取菜单和功能树
        /// </summary>
        /// <returns></returns>
        public List<Dictionary<string, object>> GetMenuAndFunctionTree(string RoleID = "")
        {
            //菜单功能查询
            string sql = @"SELECT (Menu_Name+'('+Menu_Num+')') name,Menu_ID id,Menu_ParentID pId,Menu_Num num,Menu_Url ur,'false' checked,null tag,'true' chkDisabled,
case when  Menu_ParentID is not null then 'false' else 'true' end [open]
                                FROM Sys_Menu 
		                        ORDER BY Menu_Num";

            if (!RoleID.ToGuid().Equals(Guid.Empty))
            {
                //角色功能查询
                sql = @"SELECT (Menu_Name+'('+Menu_Num+')') name,Menu_ID id,Menu_ParentID pId,Menu_Num num,Menu_Url ur,'false' checked,null tag ,
case when  Menu_ParentID is not null then 'false' else 'true' end [open]
		                        FROM Sys_Menu A
		                        LEFT JOIN Sys_RoleMenuFunction B ON A.Menu_ID=B.RoleMenuFunction_MenuID
		                        WHERE 1=1 AND B.RoleMenuFunction_RoleID='" + RoleID.ToGuid() + @"'
		                        ORDER BY Menu_Num";
            }

            return db.FindList<Dictionary<string, object>>(sql, null).ToList();
        }



    }
}
