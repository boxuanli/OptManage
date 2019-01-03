using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Portal.AOP;
using global::Models;
using DbFrame.Class;
using Common;
using Common.VerificationCode;

namespace Portal.Areas.Portal.Controllers
{
    [AopActionFilter(false)]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}