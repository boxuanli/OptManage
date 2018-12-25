using Microsoft.AspNetCore.Mvc;

namespace HzyAdmin.Areas.Admin.Controllers.Sys
{
    //
    using BLL;
    using DbFrame.Class;
    using Common;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Hosting;

    /// <summary>
    /// 创建代码
    /// </summary>
    public class CreateCodeController : BaseController
    {
        private IHostingEnvironment _IHostingEnvironment = null;
        private string _WebRootPath = string.Empty;
        public CreateCodeController(IHostingEnvironment IHostingEnvironment)
        {
            this._IHostingEnvironment = IHostingEnvironment;
            _WebRootPath = this._IHostingEnvironment.WebRootPath;
        }

        protected override void Init()
        {
            this.MenuID = "Z-160";
        }

        public override IActionResult Index()
        {
            ViewData["Path"] = (_WebRootPath + "\\Content\\CreateFile\\").Replace("\\", "\\\\");
            return base.Index();
        }

        Sys_CreateCodeBL _Sys_CreateCodeBL = new Sys_CreateCodeBL();

        /// <summary>
        /// 获取数据库中所有的表和字段
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetDatabaseAllTable()
        {
            return Json(new { status = 1, value = _Sys_CreateCodeBL.GetDatabaseAllTable() });
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Save(IFormCollection fc)
        {
            var Type = fc["ClassType"].ToStr();
            var Url = (fc["Url"].ToStr() == null ? _WebRootPath + "\\Content\\CreateFile\\" : fc["Url"].ToStr());
            var Str = fc["Str"];
            var Table = fc["Table"];
            var isall = fc["isall"].ToBool();
            var template = _WebRootPath + "\\Content\\Template\\";

            if (Type == "Model")
            {
                Url = (Url + "\\Model");
                template = template + "Model\\Model.txt";
                Str = string.IsNullOrEmpty(Str.ToStr()) ? "M" : Str.ToStr();
            }
            else if (Type == "BLL")
            {
                Url = Url + "\\BLL";
                template = template + "Bll\\BLL.txt";
                Str = string.IsNullOrEmpty(Str.ToStr()) ? "BL" : Str.ToStr();
            }
            else if (Type == "DAL")
            {
                Url = Url + "\\DAL";
                template = template + "DAL\\DAL.txt";
                Str = string.IsNullOrEmpty(Str.ToStr()) ? "DA" : Str.ToStr();
            }

            if (System.IO.Directory.Exists(Url + "\\"))
            {
                var dir = new System.IO.DirectoryInfo(Url + "\\");
                var fileinfo = dir.GetFileSystemInfos();  //返回目录中所有文件和子目录
                foreach (var i in fileinfo)
                {
                    if (i is System.IO.DirectoryInfo)            //判断是否文件夹
                    {
                        var subdir = new System.IO.DirectoryInfo(i.FullName);
                        subdir.Delete(true);          //删除子目录和文件
                    }
                    else
                    {
                        System.IO.File.Delete(i.FullName);      //删除指定文件
                    }
                }
                //System.IO.Directory.Delete(Url + "\\");
            }
            System.IO.Directory.CreateDirectory(Url);

            if (!System.IO.File.Exists(template))
                throw new MessageBox("模板文件不存在");

            var Content = System.IO.File.ReadAllText(template);

            if (isall)
            {
                var list = _Sys_CreateCodeBL.GetAllTable();
                foreach (var item in list)
                {
                    Table = item["TABLE_NAME"] == null ? "" : item["TABLE_NAME"].ToString();
                    this._Sys_CreateCodeBL.CreateFileLogic(Content, Table, Str, Type, Url);
                }
            }
            else
            {
                if (string.IsNullOrEmpty(Table))
                    throw new MessageBox("请选择表");
                this._Sys_CreateCodeBL.CreateFileLogic(Content, Table, Str, Type, Url);
            }

            return Json(new { status = 1 });
        }




    }
}