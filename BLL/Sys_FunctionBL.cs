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
    public class Sys_FunctionBL : BaseBLL
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
            return new Sys_FunctionDA().GetDataSource(query, pageindex, pagesize);
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string Save(Sys_FunctionM model)
        {
            if (model.Function_ID.ToGuid().Equals(Guid.Empty))
            {
                model.Function_ID = db.Add(model, li).ToGuid();
                if (model.Function_ID.ToGuid().Equals(Guid.Empty))
                    throw new MessageBox(db.ErrorMessge);
            }
            else
            {
                if (!db.EditById<Sys_FunctionM>(model, li))
                    throw new MessageBox(db.ErrorMessge);
            }
            if (!db.Commit(li))
                throw new MessageBox(db.ErrorMessge);

            return model.Function_ID.ToGuidStr();
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
                if (!db.DeleteById<Sys_FunctionM>(item.ToGuid(), li))
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
            var functionM = db.FindById<Sys_FunctionM>(ID.ToGuid());
            var di = this.EntityToDictionary(new Dictionary<string, object>()
            {
                {"functionM",functionM},
                {"status",1}
            });
            return di;
        }









    }
}
