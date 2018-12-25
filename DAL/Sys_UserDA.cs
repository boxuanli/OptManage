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

namespace DAL
{
    public class Sys_UserDA : BaseDAL
    {
        Sys_UserM userM = new Sys_UserM();

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
                .Query<Sys_UserM, Sys_UserRoleM, Sys_RoleM>((a, b, c) => new { a.User_Name, a.User_LoginName, a.User_Email, c.Role_Name, a.User_CreateTime, _ukid = a.User_ID })
                .Join((a, b, c) => a.User_ID == b.UserRole_UserID, "b")
                .Join((a, b, c) => b.UserRole_RoleID == c.Role_ID, "c")
                .WhereIF(!string.IsNullOrEmpty(query["User_Name"].ToStr()), (a, b, c) => a.User_Name.Contains(query["User_Name"].ToStr()))
                .WhereIF(!string.IsNullOrEmpty(query["User_LoginName"].ToStr()), (a, b, c) => a.User_LoginName.Contains(query["User_LoginName"].ToStr()))
                .OrderBy((a, b, c) => new { desc = a.User_CreateTime });

            return this.FindPaging(IQuery, page, rows,
                new Sys_UserM(),
                new Sys_RoleM()
            );
        }









    }
}
