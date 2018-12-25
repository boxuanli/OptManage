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
    public class Sys_FunctionDA : BaseDAL
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
                .Query<Sys_FunctionM>((a) => new { a.Function_Num, a.Function_Name, a.Function_ByName, a.Function_CreateTime, _ukid = a.Function_ID })
                .OrderBy((a) => new { a.Function_Num });

            return this.FindPaging(IQuery, page, rows,
                new Sys_FunctionM()
            );
        }




    }
}
