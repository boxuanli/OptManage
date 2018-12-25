using System;
using System.Collections.Generic;
using System.Linq;
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
    /// 菜单功能
    /// </summary>
    public class MenuFunctionController : BaseController
    {

        protected override void Init()
        {
            this.MenuID = "Z-130";
        }

        Sys_MenuM _Sys_MenuM = new Sys_MenuM();
        Sys_MenuBL _Sys_MenuBL = new Sys_MenuBL();
        Sys_FunctionM _Sys_FunctionM = new Sys_FunctionM();
        Sys_MenuFunctionM _Sys_MenuFunctionM = new Sys_MenuFunctionM();


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
            return _Sys_MenuBL.GetDataSource(query, page, rows);
        }
        #endregion  查询数据列表

        /// <summary>
        /// 获取菜单和功能树
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult GetMenuAndFunctionTree()
        {
            return Json(new { status = 1, value = _Sys_MenuBL.GetMenuAndFunctionTree() });
        }




        #region  基本操作，增删改查
        /// <summary>
        /// 保存
        /// </summary>
        /// <returns></returns>
        [AOP.AopCheckEntityFilter(new string[] { "model" })]
        [HttpPost]
        public IActionResult Save(Sys_MenuM model, string Function_ID)
        {
            this.KeyID = _Sys_MenuBL.Save(model, Function_ID);
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
            _Sys_MenuBL.Delete(ID);
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
            return Json(_Sys_MenuBL.Find(ID.ToGuid()));
        }

        /// <summary>
        /// 保存菜单功能
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult SaveMenuFunction(string nodes)
        {
            _Sys_MenuBL.SaveMenuFunction(nodes);
            return Json(new { status = 1 });
        }
        #endregion  基本操作，增删改查




    }
}