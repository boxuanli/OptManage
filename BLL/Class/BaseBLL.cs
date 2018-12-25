namespace BLL.Class
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using DbFrame;
    using DbFrame.Class;
    using Common;
    using Models;

    public class BaseBLL
    {

        /// <summary>
        /// 登录 信息 对象
        /// </summary>
        protected Sys_AccountM Account = new Sys_AccountM();

        public BaseBLL()
        {
            Account = this.GetSession<Sys_AccountM>("Account");
        }

        protected DBContext db = new DBContext();

        protected List<SQL> li = new List<SQL>();

        public void SetSession(string key, object value)
        {
            Tools.SetSession(key, value);
        }

        public T GetSession<T>(string key)
        {
            return Tools.GetSession<T>(key);
        }

        /// <summary>
        /// 将多个实体组合成为一个 字典类型
        /// </summary>
        /// <param name="di"></param>
        /// <returns></returns>
        public Dictionary<string, object> EntityToDictionary(Dictionary<string, object> di)
        {
            Dictionary<string, object> r = new Dictionary<string, object>();
            foreach (var item in di)
            {
                if (item.Value is Models.Class.BaseClass)
                {
                    ReflexHelper.GetPropertyInfos(item.Value.GetType()).ToList().ForEach(pi =>
                    {
                        if (pi.GetValue(item.Value, null) == null)
                            r.Add(pi.Name, null);
                        else
                        {
                            if (pi.PropertyType == typeof(DateTime))
                                r.Add(pi.Name, pi.GetValue(item.Value, null).ToDateTimeFormat("yyyy-MM-dd HH:mm:ss"));
                            else
                                r.Add(pi.Name, pi.GetValue(item.Value, null));
                        }
                    });
                }
                else
                {
                    r.Add(item.Key, item.Value);
                }
            }
            return r;
        }

    }
}
