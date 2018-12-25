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
//
using System.Data;
using System.Linq;

namespace BLL
{
    public class Sys_MenuBL : BaseBLL
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
            return new Sys_MenuDA().GetDataSource(query, pageindex, pagesize);
        }

        /// <summary>
        /// 获取所有的菜单
        /// </summary>
        /// <returns></returns>
        public List<Sys_MenuM> GetMenuByRoleID()
        {
            return new Sys_MenuDA().GetMenuByRoleID();
        }

        #region  左侧菜单
        public string GetSysMenu()
        {
            var menu_list = this.GetMenuByRoleID();
            StringBuilder sb = new StringBuilder();
            if (menu_list.Count > 0)
            {
                var parentList = menu_list.FindAll(item => item.Menu_ParentID == null || item.Menu_ParentID.ToGuid() == Guid.Empty);
                foreach (var item in parentList)
                {
                    var childList = menu_list.FindAll(w => w.Menu_ParentID != null && w.Menu_ParentID == item.Menu_ID);
                    if (childList.Count > 0)
                    {
                        //<li class="">
                        //<a class="has-first-menu has-arrow" href="#" aria-expanded="false"><i class=" fa fa-laptop"></i>&nbsp;&nbsp;<span>系统管理11133</span></a>
                        //</li>
                        sb.Append("<li>");
                        sb.Append(string.Format("<a class=\"has-arrow has-first-menu\" href=\"javascript:void(0)\" aria-expanded=\"false\"><i class=\"{0} fa-lg\"></i>&nbsp;&nbsp;<span>{1}</span></a>", item.Menu_Icon, item.Menu_Name));
                        GetChildMenu(menu_list, item.Menu_ID.ToGuid(), sb);
                        sb.Append("</li>");
                    }
                    else
                    {
                        //< li >
                        //            < a href = "#!/home1.html" >
                        //                 < i class="fa fa-fw fa-code-fork"></i>&nbsp;&nbsp;<span>测试1</span>
                        //              </a>
                        //        </li>
                        sb.Append("<li>");
                        sb.Append(string.Format("<a href=\"javascript:void(0);var url='{0}';\" hzy-router-href=\"{0}\" hzy-router-text=\"{2}\" class=\"has-first-menu\"><i class=\"{1}\"></i>&nbsp;&nbsp;<span>{2}</span></a>", item.Menu_Url, item.Menu_Icon, item.Menu_Name));
                        sb.Append("</li>");
                    }
                }
            }
            return sb.ToString();
        }
        public void GetChildMenu(List<Sys_MenuM> menu_list, Guid id, StringBuilder sb)
        {
            var list = menu_list.FindAll(w => w.Menu_ParentID.ToGuid() == id);
            foreach (var item in list)
            {
                var childList = menu_list.FindAll(w => w.Menu_ParentID == item.Menu_ID);
                if (list.IndexOf(item) == 0) sb.Append("<ul aria-expanded=\"false\">");
                if (childList.Count > 0)
                {
                    sb.Append("<li>");
                    sb.Append(string.Format("<a class=\"has-arrow\" href=\"javascript:void(0)\" aria-expanded=\"false\"><i class=\"{0}\"></i>&nbsp;&nbsp;<span>{1}</span></a>", item.Menu_Icon, item.Menu_Name));
                    GetChildMenu(menu_list, item.Menu_ID.ToGuid(), sb);
                    sb.Append("</li>");
                }
                else
                {
                    sb.Append("<li>");
                    sb.Append(string.Format("<a  href=\"javascript:void(0);var url='{0}';\" hzy-router-href=\"{0}\" hzy-router-text=\"{2}\" ><i class=\"{1}\"></i>&nbsp;&nbsp;<span>{2}</span></a>", item.Menu_Url, item.Menu_Icon, item.Menu_Name));
                    sb.Append("</li>");
                }
            }
            sb.Append("</ul>");
        }

        #endregion  左侧菜单

        #region  系统管理》菜单功能，角色功能  树的json处理

        /// <summary>
        /// 获取菜单和功能树
        /// </summary>
        /// <returns></returns>
        public List<Dictionary<string, object>> GetMenuAndFunctionTree()
        {
            var di = new Dictionary<string, object>();
            var tf_list = db.FindList<Sys_FunctionM>(null, orderby => new { orderby.Function_Num }).ToList();
            var list = new Sys_MenuDA().GetMenuAndFunctionTree();
            var tmf_list = db.FindList<Sys_MenuFunctionM>(null, null).ToList();
            for (int i = 0; i < list.Count; i++)
            {
                string url = (list[i]["ur"]).ToStr();
                string id = (list[i]["id"]).ToGuidStr();
                if (!string.IsNullOrEmpty(url))
                {
                    tf_list.ForEach(x =>
                    {
                        di = new Dictionary<string, object>();
                        di.Add("name", x.Function_Name);
                        di.Add("id", x.Function_ID);
                        di.Add("pId", id);
                        di.Add("num", x.Function_Num);
                        di.Add("ur", "");
                        di.Add("tag", "fun");
                        if (list[i].ContainsKey("chkDisabled"))
                        {
                            di.Add("chkDisabled", true);
                        }
                        //判断该功能是否选中
                        var ischecked = tmf_list.Find(w => w.MenuFunction_FunctionID == x.Function_ID && w.MenuFunction_MenuID == id.ToGuid());
                        if (ischecked == null)
                            di.Add("checked", false);
                        else
                            di.Add("checked", true);
                        list.Add(di);
                    });
                }
            }
            return list;
        }

        /// <summary>
        /// 获取角色对应的功能树
        /// </summary>
        /// <returns></returns>
        public List<Dictionary<string, object>> GetRoleMenuFunctionTree(string roleid)
        {
            var dic = new Dictionary<string, object>();
            var menu_list = db.FindList<Sys_MenuM>(null, orderby => new { desc = orderby.Menu_Num }).ToList();
            var trmf_list = db.FindList<Sys_RoleMenuFunctionM>(item => item.RoleMenuFunction_RoleID == roleid.ToGuid(), orderby => new { orderby.RoleMenuFunction_CreateTime }).ToList();//角色菜单功能
            var tf_list = db.FindList<Sys_FunctionM>(null, orderby => new { orderby.Function_Num }).ToList();//功能
            var tmf_list = db.FindList<Sys_MenuFunctionM>(null, null).ToList();//菜单功能

            var list = new List<Dictionary<string, object>>();
            var _paret_menu_list = menu_list.FindAll(item => item.Menu_ParentID == null || item.Menu_ParentID.Equals(Guid.Empty));
            for (int i = _paret_menu_list.Count - 1; i >= 0; i--)
            {
                var _child_menu_list = menu_list.FindAll(x => x.Menu_ParentID != null && x.Menu_ParentID.Equals(_paret_menu_list[i].Menu_ID));
                //判断是否有子集
                if (_child_menu_list.Count > 0)
                {
                    dic = new Dictionary<string, object>();
                    dic.Add("name", _paret_menu_list[i].Menu_Name + "(" + _paret_menu_list[i].Menu_Num + ")");
                    dic.Add("id", _paret_menu_list[i].Menu_ID);
                    dic.Add("pId", _paret_menu_list[i].Menu_ParentID);
                    dic.Add("num", _paret_menu_list[i].Menu_Num);
                    dic.Add("ur", _paret_menu_list[i].Menu_Url);
                    dic.Add("tag", null);
                    dic.Add("checked", false);
                    list.Add(dic);
                    this.FindChildMenu(menu_list, trmf_list, tf_list, tmf_list, _paret_menu_list[i], roleid.ToGuid(), list);
                }
                else
                {
                    if (tmf_list.FindAll(val => val.MenuFunction_MenuID.Equals(_paret_menu_list[i].Menu_ID)).Count == 0)//判断该菜单是否有 勾选了功能 如果没有则删除
                    {
                        _paret_menu_list.RemoveAt(i);
                        continue;
                    }

                    dic = new Dictionary<string, object>();
                    dic.Add("name", _paret_menu_list[i].Menu_Name + "(" + _paret_menu_list[i].Menu_Num + ")");
                    dic.Add("id", _paret_menu_list[i].Menu_ID);
                    dic.Add("pId", _paret_menu_list[i].Menu_ParentID);
                    dic.Add("num", _paret_menu_list[i].Menu_Num);
                    dic.Add("ur", _paret_menu_list[i].Menu_Url);
                    dic.Add("tag", null);
                    dic.Add("checked", false);
                    list.Add(dic);

                    //找出该菜单下的功能和选中的功能
                    tf_list.ForEach(a =>
                    {
                        if (tmf_list.FindAll(val => val.MenuFunction_FunctionID.Equals(a.Function_ID)
                            && val.MenuFunction_MenuID.Equals(_paret_menu_list[i].Menu_ID)).Count > 0)
                        {
                            dic = new Dictionary<string, object>();
                            dic.Add("name", a.Function_Name);
                            dic.Add("id", a.Function_ID);
                            dic.Add("pId", _paret_menu_list[i].Menu_ID);
                            dic.Add("num", a.Function_Num);
                            dic.Add("ur", null);
                            dic.Add("tag", "fun");
                            //判断该功能是否选中
                            var ischecked = trmf_list.Find(x => x.RoleMenuFunction_FunctionID.Equals(a.Function_ID) && x.RoleMenuFunction_MenuID.Equals(_paret_menu_list[i].Menu_ID) && x.RoleMenuFunction_RoleID.Equals(roleid.ToGuid()));
                            if (ischecked == null)
                                dic.Add("checked", false);
                            else
                                dic.Add("checked", true);
                            list.Add(dic);
                        }
                    });
                }
            }
            return list;
        }

        private void FindChildMenu(List<Sys_MenuM> menu_list, List<Sys_RoleMenuFunctionM> trmf_list, List<Sys_FunctionM> tf_list, List<Sys_MenuFunctionM> tmf_list, Sys_MenuM menu, Guid roleid, List<Dictionary<string, object>> list)
        {
            var dic = new Dictionary<string, object>();

            var _paret_menu_list = menu_list.FindAll(item => item.Menu_ParentID != null && item.Menu_ParentID.Equals(menu.Menu_ID));

            for (int i = _paret_menu_list.Count - 1; i >= 0; i--)
            {
                var _child_menu_list = menu_list.FindAll(x => x.Menu_ParentID != null && x.Menu_ParentID.Equals(_paret_menu_list[i].Menu_ID));
                //判断是否有子集
                if (_child_menu_list.Count > 0)
                {
                    dic = new Dictionary<string, object>();
                    dic.Add("name", _paret_menu_list[i].Menu_Name + "(" + _paret_menu_list[i].Menu_Num + ")");
                    dic.Add("id", _paret_menu_list[i].Menu_ID);
                    dic.Add("pId", _paret_menu_list[i].Menu_ParentID);
                    dic.Add("num", _paret_menu_list[i].Menu_Num);
                    dic.Add("ur", _paret_menu_list[i].Menu_Url);
                    dic.Add("tag", null);
                    dic.Add("checked", false);
                    list.Add(dic);
                    this.FindChildMenu(menu_list, trmf_list, tf_list, tmf_list, _paret_menu_list[i], roleid.ToGuid(), list);
                }
                else
                {
                    if (tmf_list.FindAll(val => val.MenuFunction_MenuID.Equals(_paret_menu_list[i].Menu_ID)).Count == 0)//判断该菜单是否有 勾选了功能 如果没有则删除
                    {
                        _paret_menu_list.RemoveAt(i);
                        continue;
                    }

                    dic = new Dictionary<string, object>();
                    dic.Add("name", _paret_menu_list[i].Menu_Name + "(" + _paret_menu_list[i].Menu_Num + ")");
                    dic.Add("id", _paret_menu_list[i].Menu_ID);
                    dic.Add("pId", _paret_menu_list[i].Menu_ParentID);
                    dic.Add("num", _paret_menu_list[i].Menu_Num);
                    dic.Add("ur", _paret_menu_list[i].Menu_Url);
                    dic.Add("tag", null);
                    dic.Add("checked", false);
                    list.Add(dic);


                    //找出该菜单下的功能和选中的功能
                    tf_list.ForEach(a =>
                    {
                        if (tmf_list.FindAll(val => val.MenuFunction_FunctionID.Equals(a.Function_ID)
                            && val.MenuFunction_MenuID.Equals(_paret_menu_list[i].Menu_ID)).Count > 0)
                        {
                            dic = new Dictionary<string, object>();
                            dic.Add("name", a.Function_Name);
                            dic.Add("id", a.Function_ID);
                            dic.Add("pId", _paret_menu_list[i].Menu_ID);
                            dic.Add("num", a.Function_Num);
                            dic.Add("ur", null);
                            dic.Add("tag", "fun");
                            //判断该功能是否选中
                            var ischecked = trmf_list.Find(x => x.RoleMenuFunction_FunctionID.Equals(a.Function_ID) && x.RoleMenuFunction_MenuID.Equals(_paret_menu_list[i].Menu_ID) && x.RoleMenuFunction_RoleID.Equals(roleid.ToGuid()));
                            if (ischecked == null)
                                dic.Add("checked", false);
                            else
                                dic.Add("checked", true);
                            list.Add(dic);
                        }
                    });
                }
            }
        }

        #endregion 系统管理》菜单功能，角色功能  树的json处理


        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string Save(Sys_MenuM model, string Function_ID)
        {
            if (model.Menu_ID.ToGuid() == Guid.Empty)
            {
                model.Menu_ID = db.Add(model, li).ToGuid();
                if (model.Menu_ID.ToGuid().Equals(Guid.Empty))
                    throw new MessageBox(db.ErrorMessge);
            }
            else
            {
                if (!db.EditById<Sys_MenuM>(model, li))
                    throw new MessageBox(db.ErrorMessge);
            }

            //删除菜单的功能
            if (!db.Delete<Sys_MenuFunctionM>(w => w.MenuFunction_MenuID == model.Menu_ID.ToGuid(), li))
                throw new MessageBox(db.ErrorMessge);

            db.JsonToList<Sys_FunctionM>(Function_ID).ToList().ForEach(item =>
            {
                var funcid = db.Add(new Sys_MenuFunctionM()
                {
                    MenuFunction_MenuID = model.Menu_ID.ToGuid(),
                    MenuFunction_FunctionID = item.Function_ID.ToGuid(),
                }, li);
                if (funcid.ToGuid() == Guid.Empty) throw new MessageBox(db.ErrorMessge);
            });

            if (!db.Commit(li))
                throw new MessageBox(db.ErrorMessge);

            return model.Menu_ID.ToGuidStr();
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
                //删除菜单的功能
                if (!db.Delete<Sys_MenuFunctionM>(w => w.MenuFunction_MenuID == item.ToGuid(), li))
                    throw new MessageBox(db.ErrorMessge);

                if (!db.DeleteById<Sys_MenuM>(item.ToGuid(), li))
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
            var _Sys_MenuM = db.FindById<Sys_MenuM>(ID.ToGuid());

            if (ID.ToGuid().Equals(Guid.Empty))
            {
                _Sys_MenuM.Menu_CreateTime = DateTime.Now.ToDateTime();
            }

            var menu = db.FindById<Sys_MenuM>(_Sys_MenuM.Menu_ParentID);

            var Menu_Power = db.FindList<Sys_MenuFunctionM>(w => w.MenuFunction_MenuID == ID.ToGuid(), orderby => new { orderby.MenuFunction_CreateTime }).Select(item => item.MenuFunction_FunctionID);

            var di = this.EntityToDictionary(new Dictionary<string, object>()
            {
                {"_Sys_MenuM",_Sys_MenuM},
                {"pname",menu.Menu_Name.ToStr()},
                {"Menu_Power",Menu_Power},
                {"status",1}
            });
            return di;
        }

        /// <summary>
        /// 保存菜单功能
        /// </summary>
        public void SaveMenuFunction(string nodes)
        {
            var json = ((object[])nodes.DeserializeObject()).ToList();
            var list = new List<Guid>();
            if (!db.Delete<Sys_MenuFunctionM>(null, li))
                throw new MessageBox(db.ErrorMessge);
            json.ForEach(item =>
            {
                var func = (Dictionary<string, object>)item;
                if (func["tag"].ToStr().Equals("fun"))
                {
                    var menuid = list.Find(x => x.Equals(func["pId"].ToGuid()));
                    if (db.Add<Sys_MenuFunctionM>(new Sys_MenuFunctionM()
                    {
                        MenuFunction_MenuID = func["pId"].ToGuid(),
                        MenuFunction_FunctionID = func["id"].ToGuid()
                    }, li).Equals(Guid.Empty))
                        throw new MessageBox(db.ErrorMessge);
                    list.Add(func["pId"].ToGuid());
                }
            });
            if (!db.Commit(li))
                throw new MessageBox(db.ErrorMessge);
        }



    }
}
