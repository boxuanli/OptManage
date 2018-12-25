using Microsoft.AspNetCore.Mvc;
//
using AOP;
using Common;

namespace HzyAdmin.Areas.Admin.Controllers
{
    [AopActionFilter(false)]
    public class ErrorController : BaseController
    {

        public IActionResult Index(ErrorModel _ErrorModel)
        {
            return View(_ErrorModel);
        }
    }
}