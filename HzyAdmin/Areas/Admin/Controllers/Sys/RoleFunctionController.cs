using System;
using Microsoft.AspNetCore.Mvc;

namespace HzyAdmin.Areas.Admin.Controllers.Sys
{
    //
    using global::Models;
    using BLL;
    using DbFrame.Class;
    using Common;

    /// <summary>
    /// 角色功能
    /// </summary>
    public class RoleFunctionController : BaseController
    {

        protected override void Init()
        {
            this.MenuID = "Z-140";
        }

        Sys_RoleBL _Sys_RoleBL = new Sys_RoleBL();
        Sys_MenuBL _Sys_MenuBL = new Sys_MenuBL();

        #region  基本操作，增删改查

        /// <summary>
        /// 获取角色菜单功能
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult GetRoleMenuFunctionTree(string roleid)
        {
            return Json(new { status = 1, value = _Sys_MenuBL.GetRoleMenuFunctionTree(roleid) });
        }

        /// <summary>
        /// 保存角色功能
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Save(string rows, string roleid)
        {
            _Sys_RoleBL.SaveFunction(rows, roleid);
            return Json(new { status = 1 });
        }
        #endregion 基本操作，增删改查


    }
}