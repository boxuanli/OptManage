using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    //
    using Common;
    using DbFrame;
    using DbFrame.Class;
    using Models;
    using DAL.Class;
    using System.Collections;

    public class MemberDA : BaseDAL
    {
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
                .Query<MemberM, Sys_UserM>((a, b) => new { a.Member_Num, a.Member_Name, a.Member_Phone, a.Member_Sex, b.User_Name, a.Member_CreateTime, _ukid = a.Member_ID })
                .Join((a, b) => a.Member_UserID == b.User_ID, "b")
                .WhereIF(!string.IsNullOrEmpty(query["Member_Name"].ToStr()), (a, b) => a.Member_Name.Contains(query["Member_Name"].ToStr()))
                .WhereIF(!string.IsNullOrEmpty(query["User_Name"].ToStr()), (a, b) => b.User_Name.Contains(query["User_Name"].ToStr()))
                .OrderBy((a, b) => new { desc = a.Member_Num });

            //return this.FindPaging(IQuery.ToSQL(), page, rows, IQuery.GetSqlParameters(),
            //    new MemberM(),
            //    new Sys_UserM()
            //);//也可以这样写

            return this.FindPaging(IQuery, page, rows,
                new MemberM(),
                new Sys_UserM()
            );
        }


    }
}
