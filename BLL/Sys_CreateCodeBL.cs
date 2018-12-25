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
    public class Sys_CreateCodeBL : BaseBLL
    {


        /// <summary>
        /// 获取数据库中所有的表
        /// </summary>
        /// <returns></returns>
        public List<Dictionary<string, object>> GetDatabaseAllTable()
        {
            return new Sys_CreateCodeDA().GetDatabaseAllTable();
        }

        /// <summary>
        /// 根据表获取列
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public List<Dictionary<string, object>> GetColByTable(string table)
        {
            return new Sys_CreateCodeDA().GetColByTable(table);
        }

        /// <summary>
        /// 获取所有的table
        /// </summary>
        /// <returns></returns>
        public List<Dictionary<string, object>> GetAllTable()
        {
            return new Sys_CreateCodeDA().GetAllTable();
        }

        public void CreateFileLogic(string Content, string Table, string Str, string Type, string Url)
        {
            var ClassName = Table + Str;

            Content = Content.Replace("<#ClassName#>", ClassName);
            Content = Content.Replace("<#TableName#>", Table);
            string filds = string.Empty;

            if (Type == "Model")
            {
                var col = this.GetColByTable(Table);
                foreach (var item in col)
                {
                    var key = item["iskey"] == null ? "" : item["iskey"].ToString();
                    var colname = item["colname"] == null ? "" : item["colname"].ToString();
                    var type = item["type"] == null ? "" : item["type"].ToString();

                    switch (type)
                    {
                        case "uniqueidentifier":
                            type = !string.IsNullOrEmpty(key) ? "Guid" : "Guid?";
                            break;
                        case "bit":
                        case "int":
                            type = type = !string.IsNullOrEmpty(key) ? "int" : "int?";
                            break;
                        case "datetime":
                            type = "DateTime?";
                            break;
                        case "float":
                            type = "float?";
                            break;
                        case "money":
                            type = "double?";
                            break;
                        case "decimal":
                            type = "decimal?";
                            break;
                        default:
                            type = "string";
                            break;
                    }

                    if (!string.IsNullOrEmpty(key))
                    {
                        filds += "\t\t[Field(\"" + colname + "\", IsPrimaryKey = true)]" + "\r\n";
                    }
                    else
                    {
                        if (colname.Contains("_CreateTime") && type == "DateTime?")
                            filds += "\t\t[Field(\"创建时间\", IsIgnore = true)]" + "\r\n";
                        else
                            filds += "\t\t[Field(\"" + colname + "\")]" + "\r\n";
                    }
                    filds += "\t\tpublic " + type + " " + colname + " { get; set; }\r\n\r\n";
                }
                Content = Content.Replace("<#Filds#>", filds);
            }
            System.IO.File.WriteAllText(Url + "\\" + ClassName + ".cs", Content);
        }




    }
}
