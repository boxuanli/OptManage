using Microsoft.AspNetCore.Mvc;

namespace HzyAdmin.Areas.Admin.Controllers.Sys
{
    //
    using global::Models;
    using Common;

    /// <summary>
    /// 修改密码
    /// </summary>
    public class ChangePwdController : BaseController
    {
        protected override void Init()
        {
            this.MenuID = "Z-150";
        }

        Sys_UserM _Sys_UserM = new Sys_UserM();

        public override IActionResult Index()
        {
            _Sys_UserM = db.Find<Sys_UserM>(w => w.User_ID == Account.UserID);
            ViewData["userName"] = _Sys_UserM.User_Name;
            return View();
        }

        [HttpPost]
        public IActionResult ChangePwd(string oldpwd, string newpwd, string newlypwd)
        {
            if (string.IsNullOrEmpty(oldpwd))
                throw new MessageBox("旧密码不能为空");
            if (string.IsNullOrEmpty(newpwd))
                throw new MessageBox("新密码不能为空");
            if (string.IsNullOrEmpty(newlypwd))
                throw new MessageBox("确认新密码不能为空");
            if (!newpwd.Equals(newlypwd))
                throw new MessageBox("两次密码不一致");
            _Sys_UserM = db.Find<Sys_UserM>(w => w.User_ID == Account.UserID);
            if (!_Sys_UserM.User_Pwd.Equals(oldpwd.Trim()))//Tools.MD5Encrypt(oldpwd.Trim())))
                throw new MessageBox("旧密码不正确");
            if (!db.Edit<Sys_UserM>(() => new Sys_UserM() { User_Pwd = newlypwd.Trim() }, w => w.User_ID == Account.UserID, li))
                throw new MessageBox(db.ErrorMessge);
            if (!db.Commit(li))
                throw new MessageBox(db.ErrorMessge);
            return Json(new { status = 1 });
        }












    }
}