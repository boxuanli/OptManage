using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace HzyAdmin.Areas.Admin.Controllers.Base
{
    //
    using BLL;
    using DbFrame.Class;
    using Common;
    using System.Collections;
    using Microsoft.AspNetCore.Hosting;
    using System.IO;
    using global::Models;
    using Microsoft.AspNetCore.Http;

    /// <summary>
    /// 会员管理
    /// </summary>
    public class MemberController : BaseController
    {
        private IHostingEnvironment _IHostingEnvironment = null;
        private string _WebRootPath = string.Empty;
        public MemberController(IHostingEnvironment IHostingEnvironment)
        {
            this._IHostingEnvironment = IHostingEnvironment;
            _WebRootPath = this._IHostingEnvironment.WebRootPath;
        }

        protected override void Init()
        {
            this.MenuID = "A-100";
            this.PrintTitle = "我是一个 打印标题！";
        }

        MemberBL _MemberBL = new MemberBL();

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
            return _MemberBL.GetDataSource(query, page, rows);
        }
        #endregion  查询数据列表

        #region  基本操作，增删改查
        /// <summary>
        /// 保存
        /// </summary>
        /// <returns></returns>
        [AOP.AopCheckEntityFilter(new string[] { "model" })]
        [HttpPost]
        public ActionResult Save(MemberM model, IFormFile Member_Photo_Files, IFormFile Member_FilePath_Fiels)
        {
            //接收图片
            if (Member_Photo_Files != null)
            {
                this.HandleUpFile(Member_Photo_Files, new string[] { ".jpg", ".gif", ".png" }, _WebRootPath, null, (_Path) =>
                {
                    model.Member_Photo = _Path;
                });
            }

            //接收文件
            if (Member_FilePath_Fiels != null)
            {
                this.HandleUpFile(Member_FilePath_Fiels, null, _WebRootPath, null, (_Path) =>
                {
                    model.Member_FilePath = _Path;
                });
            }

            //判断是否有文件上传上来
            //if (Member_Photo_Files.Count > 0)
            //{
            //    foreach (var item in Member_Photo_Files)
            //    {
            //        this.HandleUpFile(item, new string[] { ".jpg", ".gif", ".png" }, _WebRootPath, null, (_Path) =>
            //         {
            //             model.Member_Photo = _Path;
            //         });
            //    }
            //}

            this.KeyID = _MemberBL.Save(model);
            return Json(new { status = 1, ID = KeyID });
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Delete(string ID)
        {
            _MemberBL.Delete(ID);
            return Json(new { status = 1 });
        }

        /// <summary>
        /// 查询根据ID
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Find(string ID)
        {
            return Json(_MemberBL.Find(ID.ToGuid()));
        }
        #endregion  基本操作，增删改查










    }
}