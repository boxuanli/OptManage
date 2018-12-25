using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BLL;
using DbFrame.Class;
using Common;
using System.Collections;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using global::Models;
using Microsoft.AspNetCore.Http;

namespace HzyAdmin.Areas.Admin.Controllers.Base
{
    public class ExecutivesController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}