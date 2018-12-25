using Microsoft.AspNetCore.Mvc;

namespace HzyAdmin.Areas.Admin.Controllers
{
    using BLL;

    public class HomeController : BaseController
    {

        Sys_MenuBL _Sys_MenuBL = new Sys_MenuBL();

        protected override void Init()
        {
            base.Init();
            this.IsExecutePowerLogic = false;
        }

        public override IActionResult Index()
        {
            ViewData["MenuHtml"] = _Sys_MenuBL.GetSysMenu();
            return View(Account);
        }

        public IActionResult Main()
        {
            return View();
        }



    }
}