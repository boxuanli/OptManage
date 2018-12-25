using Microsoft.AspNetCore.Mvc;

namespace HzyAdmin.Areas.Admin.Controllers.Sys
{
    //
    using global::Models;
    using BLL;
    using DbFrame.Class;
    using Common;
    using System.Collections;

    /// <summary>
    /// 功能管理
    /// </summary>
    public class FunctionController : BaseController
    {
        protected override void Init()
        {
            this.MenuID = "Z-120";
        }

        Sys_FunctionBL _Sys_FunctionBL = new Sys_FunctionBL();

        #region  查询数据列表
        /// <summary>
        /// 获取列表数据
        /// </summary>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        [NonAction]
        public override Sys_PagingEntity GetPagingEntity(Hashtable query, int page = 1, int rows = 20)
        {
            //获取列表
            return _Sys_FunctionBL.GetDataSource(query, page, rows);
        }
        #endregion  查询数据列表

        #region  基本操作，增删改查
        /// <summary>
        /// 保存
        /// </summary>
        /// <returns></returns>
        [AOP.AopCheckEntityFilter(new string[] { "model" })]
        [HttpPost]
        public IActionResult Save(Sys_FunctionM model)
        {
            this.KeyID = _Sys_FunctionBL.Save(model);
            return Json(new { status = 1, ID = KeyID });
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Delete(string ID)
        {
            _Sys_FunctionBL.Delete(ID);
            return Json(new { status = 1 });
        }

        /// <summary>
        /// 查询根据ID
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Find(string ID)
        {
            return Json(_Sys_FunctionBL.Find(ID.ToGuid()));
        }

        #endregion  基本操作，增删改查





    }
}