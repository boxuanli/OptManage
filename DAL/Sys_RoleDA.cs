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
    public class Sys_RoleDA : BaseDAL
    {
        Sys_RoleM roleM = new Sys_RoleM();

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
                .Query<Sys_RoleM>((a) => new { a.Role_Num, a.Role_Name, a.Role_Remark, a.Role_CreateTime, _ukid = a.Role_ID })
                .WhereIF(!string.IsNullOrEmpty(query["Role_Name"].ToStr()), (a) => a.Role_Name.Contains(query["Role_Name"].ToStr()))
                .OrderBy((a) => new { a.Role_Num });

            return this.FindPaging(IQuery, page, rows,
                new Sys_RoleM()
            );
        }

    }
}
