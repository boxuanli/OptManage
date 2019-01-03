using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Portal.Controllers
{
    using DbFrame;
    using DbFrame.Class;
    using Common;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc.Filters;
    using System.IO;
    using System.Data;
    using global::Models;
    using Portal.AOP;

    [AopActionFilter()]
    [Area(areaName: "Portal")]
    public class BaseController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}