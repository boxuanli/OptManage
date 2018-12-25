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
    public class Sys_UserBL : BaseBLL
    {
        Sys_UserRoleM userroleM = new Sys_UserRoleM();
        Sys_RoleM roleM = new Sys_RoleM();

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="QuickConditions"></param>
        /// <param name="pageindex"></param>
        /// <param name="pagesize"></param>
        /// <returns></returns>
        public Sys_PagingEntity GetDataSource(Hashtable query, int page, int rows)
        {
            return new Sys_UserDA().GetDataSource(query, page, rows);
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string Save(Sys_UserM model, string Role_ID)
        {
            if (model.User_ID.ToGuid().Equals(Guid.Empty))
            {
                if (string.IsNullOrEmpty(model.User_Pwd))
                    model.User_Pwd = "123"; //Tools.MD5Encrypt("123456");
                else
                    model.User_Pwd = model.User_Pwd;//Tools.MD5Encrypt(model.cUsers_LoginPwd);
                model.User_ID = db.Add(model, li).ToGuid();
                if (model.User_ID.ToGuid().Equals(Guid.Empty))
                    throw new MessageBox(db.ErrorMessge);
                //用户角色
                userroleM.UserRole_UserID = model.User_ID;
                userroleM.UserRole_RoleID = Role_ID.ToGuid();
                if (db.Add(userroleM, li).ToGuid().Equals(Guid.Empty))
                    throw new MessageBox(db.ErrorMessge);
            }
            else
            {
                //如果 密码字段为空，则不修改该密码
                if (string.IsNullOrEmpty(model.User_Pwd))
                {
                    db.EditById<Sys_UserM>(() => new Sys_UserM
                    {
                        User_ID = model.User_ID,
                        User_Email = model.User_Email,
                        User_IsDelete = model.User_IsDelete,
                        User_LoginName = model.User_LoginName,
                        User_Name = model.User_Name
                    });
                }
                else
                {
                    if (!db.EditById<Sys_UserM>(model, li))
                        throw new MessageBox(db.ErrorMessge);
                }

                //用户角色
                if (!db.Delete<Sys_UserRoleM>(w => w.UserRole_UserID == model.User_ID, li))
                    throw new MessageBox(db.ErrorMessge);
                if (db.Add<Sys_UserRoleM>(new Sys_UserRoleM()
                {
                    UserRole_UserID = model.User_ID,
                    UserRole_RoleID = Role_ID.ToGuid()
                }, li).ToGuid().Equals(Guid.Empty))
                    throw new MessageBox(db.ErrorMessge);
            }

            if (!db.Commit(li))
                throw new MessageBox(db.ErrorMessge);

            return model.User_ID.ToGuidStr();
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
                var _Sys_UserM = db.FindById<Sys_UserM>(item.ToGuid());
                if (_Sys_UserM.User_IsDelete == 2) throw new MessageBox("该信息无法删除！");
                if (!db.Delete<Sys_UserRoleM>(w => w.UserRole_UserID == item.ToGuid(), li))
                    throw new MessageBox(db.ErrorMessge);
                if (!db.DeleteById<Sys_UserM>(item.ToGuid(), li))
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
            var userM = db.FindById<Sys_UserM>(ID);
            userroleM = db.Find<Sys_UserRoleM>(w => w.UserRole_UserID == userM.User_ID.ToGuid());
            roleM = db.FindById<Sys_RoleM>(userroleM.UserRole_RoleID.ToGuid());

            var di = this.EntityToDictionary(new Dictionary<string, object>()
            {
                {"userM",userM},
                //{"userroleM",userroleM},
                {"roleM",roleM},
                {"status",1}
            });

            //重要字段移除 不能传递给页面
            if (di.ContainsKey("User_Pwd")) di.Remove("User_Pwd");
            //if (di.ContainsKey("User_Token")) di.Remove("User_Token");

            return di;
        }










    }
}
