using System;
using System.Collections.Generic;
using System.Text;
//
using Models;
using DAL;
using System.Collections;
using BLL.Class;
using Common;
using DbFrame;
using DbFrame.Class;

namespace BLL
{
    public class Sys_RoleBL : BaseBLL
    {
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="QuickConditions"></param>
        /// <param name="pageindex"></param>
        /// <param name="pagesize"></param>
        /// <returns></returns>
        public Sys_PagingEntity GetDataSource(Hashtable query, int pageindex, int pagesize)
        {
            return new Sys_RoleDA().GetDataSource(query, pageindex, pagesize);
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string Save(Sys_RoleM model)
        {
            if (model.Role_ID.ToGuid().Equals(Guid.Empty))
            {
                model.Role_ID = db.Add(model, li).ToGuid();
                if (model.Role_ID.Equals(Guid.Empty))
                    throw new MessageBox(db.ErrorMessge);
            }
            else
            {
                if (!db.EditById(model, li))
                    throw new MessageBox(db.ErrorMessge);
            }

            if (!db.Commit(li))
                throw new MessageBox(db.ErrorMessge);

            return model.Role_ID.ToGuidStr();
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
                var _Sys_RoleM = db.FindById<Sys_RoleM>(item.ToGuid());
                if (_Sys_RoleM.Role_IsDelete == 2) throw new MessageBox("该信息无法删除！");
                if (!db.DeleteById<Sys_RoleM>(item.ToGuid(), li))
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
            var roleM = db.FindById<Sys_RoleM>(ID.ToGuid());
            var di = this.EntityToDictionary(new Dictionary<string, object>()
            {
                {"roleM",roleM},
                {"status",1}
            });
            return di;
        }

        /// <summary>
        /// 保存角色功能
        /// </summary>
        public void SaveFunction(string rows, string roleid)
        {
            if (roleid.ToGuid() == Guid.Empty)
                throw new MessageBox("请选择角色");

            if (!db.Delete<Sys_RoleMenuFunctionM>(w => w.RoleMenuFunction_RoleID == roleid.ToGuid(), li))
                throw new MessageBox(db.ErrorMessge);

            db.JsonToList<Sys_MenuFunctionM>(rows).ForEach(item =>
            {
                db.Add<Sys_RoleMenuFunctionM>(new Sys_RoleMenuFunctionM
                {
                    RoleMenuFunction_MenuID = item.MenuFunction_MenuID,
                    RoleMenuFunction_FunctionID = item.MenuFunction_FunctionID,
                    RoleMenuFunction_RoleID = roleid.ToGuid()
                }, li);
            });

            if (!db.Commit(li))
                throw new MessageBox(db.ErrorMessge);
        }



    }
}
