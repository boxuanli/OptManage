using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace HzyAdmin.Areas.Admin.Controllers
{
    //
    using AOP;
    using global::Models;
    using DbFrame.Class;
    using Common;
    using Common.VerificationCode;

    [AopActionFilter(false)]
    public class LoginController : BaseController
    {
        protected override void Init()
        {
            this.IsExecutePowerLogic = false;
        }


        public override IActionResult Index()
        {
            //Tools.log.WriteLog("大家好 我是日志");
            Tools.SetSession("Account", new Sys_AccountM());
            return View();
        }

        /// <summary>
        /// 检查 登录 信息
        /// </summary>
        /// <param name="uName"></param>
        /// <param name="uPwd"></param>
        /// <param name="loginCode"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Checked(string uName, string uPwd, string loginCode)
        {
            if (string.IsNullOrEmpty(uName))
                throw new MessageBox("请输入用户名！");
            if (string.IsNullOrEmpty(uPwd))
                throw new MessageBox("请输入密码！");
            if (string.IsNullOrEmpty(loginCode))
                throw new MessageBox("请输入验证码！");

            var _Sys_UserM = db.Find<Sys_UserM>(w => w.User_LoginName == uName);

            if (_Sys_UserM.User_ID.ToGuid() == Guid.Empty)
                throw new MessageBox("帐户不存在");
            if (_Sys_UserM.User_Pwd.ToStr().Trim() != uPwd)//Tools.MD5Encrypt(userpwd)))//
                throw new MessageBox("密码错误");

            string code = Tools.GetCookie("loginCode");
            if (string.IsNullOrEmpty(code))
                throw new MessageBox("验证码失效");
            if (!code.ToLower().Equals(loginCode.ToLower()))
                throw new MessageBox("验证码不正确");

            var _Sys_UserRoleM = db.Find<Sys_UserRoleM>(w => w.UserRole_UserID == _Sys_UserM.User_ID);

            var _Sys_RoleM = db.Find<Sys_RoleM>(w => w.Role_ID == _Sys_UserRoleM.UserRole_RoleID);

            var _Sys_AccountM = new Sys_AccountM();
            _Sys_AccountM.RoleID = _Sys_RoleM.Role_ID.ToGuid();
            _Sys_AccountM.UserID = _Sys_UserM.User_ID.ToGuid();
            _Sys_AccountM.UserName = _Sys_UserM.User_Name;
            //如果是超级管理员 帐户
            _Sys_AccountM.IsSuperManage = _Sys_RoleM.Role_ID == AppConfig.Admin_RoleID.ToGuid();
            Tools.SetSession("Account", _Sys_AccountM);
            return Json(new { status = 1, jumpurl = AppConfig.HomePageUrl });
        }

        /// <summary>
        /// 获取验证码
        /// </summary>
        /// <returns></returns>
        public IActionResult GetYZM()
        {
            var _Helper = new Helper();
            Tools.SetCookie("loginCode", _Helper.Text, 2);
            return File(_Helper.GetBytes(), Tools.GetFileContentType[".bmp"]);
        }

        /// <summary>
        /// 退出登录
        /// </summary>
        /// <returns></returns>
        public IActionResult Out()
        {
            return RedirectToAction("Index");
        }





    }
}