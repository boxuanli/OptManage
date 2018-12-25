using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    //
    using Models;
    using DAL;
    using System.Collections;
    using BLL.Class;
    using Common;
    using DbFrame.Class;

    public class MemberBL : BaseBLL
    {
        MemberM _MemberM = new MemberM();

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="QuickConditions"></param>
        /// <param name="pageindex"></param>
        /// <param name="pagesize"></param>
        /// <returns></returns>
        public Sys_PagingEntity GetDataSource(Hashtable query, int page, int rows)
        {
            return new MemberDA().GetDataSource(query, page, rows);
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string Save(MemberM model)
        {
            if (model.Member_ID.ToGuid().Equals(Guid.Empty))
            {
                model.Member_ID = db.Add(model, li).ToGuid();
                if (model.Member_ID.Equals(Guid.Empty))
                    throw new MessageBox(db.ErrorMessge);
            }
            else
            {
                if (!db.EditById(model, li))
                    throw new MessageBox(db.ErrorMessge);
            }
            if (!db.Commit(li))
                throw new MessageBox(db.ErrorMessge);

            return model.Member_ID.ToGuidStr();
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public bool Delete(string ID)
        {
            db.JsonToList<string>(ID).ForEach(item =>
            {
                if (!db.DeleteById<MemberM>(item.ToGuid(), li))
                    throw new MessageBox(db.ErrorMessge);
            });
            if (!db.Commit(li))
                throw new MessageBox(db.ErrorMessge);
            return true;
        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public Dictionary<string, object> Find(Guid ID)
        {
            var _MemberM = db.FindById<MemberM>(ID.ToGuid());
            var _Sys_UserM = db.FindById<Sys_UserM>(_MemberM.Member_UserID);
            var di = this.EntityToDictionary(new Dictionary<string, object>()
            {
                {"_MemberM",_MemberM},
                {"_Sys_UserM",_Sys_UserM},
                {"status",1}
            });
            if (di.ContainsKey("User_Pwd"))
                di.Remove("User_Pwd");

            //格式化 日期
            di["Member_Birthday"] = di["Member_Birthday"].ToDateTimeFormat();

            return di;
        }

        /// <summary>
        /// 导入excel 数据到数据库中
        /// </summary>
        /// <param name="fileStream"></param>
        /// <param name="_Action"></param>
        //public void ExcelToDb(System.IO.Stream fileStream, Action<string> _Action)
        //{
        //    StringBuilder errorMsg = new StringBuilder();
        //    //excel工作表
        //    ISheet sheet = null;
        //    //根据文件流创建excel数据结构,NPOI的工厂类WorkbookFactory会自动识别excel版本，创建出不同的excel数据结构
        //    IWorkbook workbook = WorkbookFactory.Create(fileStream);
        //    sheet = workbook.GetSheetAt(0);
        //    IRow row = null;
        //    if (sheet.LastRowNum > 0)
        //    {
        //        for (int i = 0; i <= sheet.LastRowNum; i++)
        //        {
        //            row = sheet.GetRow(i);
        //            if (row == null) continue;
        //            int rowNum = i + 1;
        //            if (i > 0)//忽略表头
        //            {
        //                var hymc = row.GetCell(0) == null ? "" : NPOIHelper.GetCellValue(row.GetCell(0)).ToStr();//用户名称
        //                var hydh = row.GetCell(1) == null ? "" : NPOIHelper.GetCellValue(row.GetCell(1)).ToStr();//用户电话
        //                var xb = row.GetCell(2) == null ? "" : NPOIHelper.GetCellValue(row.GetCell(2)).ToStr();//性别

        //                /**********开始你的逻辑部分 start***********/

        //                if (string.IsNullOrEmpty(hymc))
        //                {
        //                    errorMsg.Append(string.Format("第{0}行的会员名称不能为空", rowNum)); break;
        //                }

        //                //得到信息 写入数据库
        //                var kydid = db.Add<MemberM>(() => new MemberM
        //                {
        //                    Member_Name = hymc,
        //                    Member_Phone = hydh.GetInt(),
        //                    Member_Sex = xb
        //                }, li);
        //                if (kydid.ToGuid() == Guid.Empty)
        //                {
        //                    errorMsg.Append(string.Format("第{0}行添加会员信息错误：" + db.ErrorMessge, rowNum)); break;
        //                }

        //                throw new MessageBox("这里只是做一个 例子！");

        //                /**********开始你的逻辑部分 end***********/

        //            }
        //        }
        //    }
        //    else
        //    {
        //        errorMsg.Append("未找到任何数据");
        //    }

        //    if (!db.Commit(li))//提交事务
        //    {
        //        errorMsg.Append(string.Format("错误信息：" + db.ErrorMessge + " "));
        //    }

        //    _Action(errorMsg.ToString());

        //}

    }
}
