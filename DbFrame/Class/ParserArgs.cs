using System;
using System.Collections.Generic;
using System.Text;

namespace DbFrame.Class
{
    public class ParserArgs
    {
        public ParserArgs()
        {
            Builder = new StringBuilder();
            SqlParameters = new Dictionary<string, object>();
            TabIsAlias = true;
        }

        public StringBuilder Builder { get; private set; }

        public Dictionary<string, object> SqlParameters { get; set; }

        /// <summary>
        /// 创建语句时是否需要 加上表的 别名
        /// </summary>
        public bool TabIsAlias { get; set; }

        /// <summary> 
        /// 追加参数
        /// </summary>
        public void AddParameter(object obj)
        {
            if (obj == null || obj == DBNull.Value)
            {
                Builder.Append("NULL");
                Builder.Replace(" = NULL", " IS NULL ");
                Builder.Replace(" <> NULL", " IS NOT NULL ");
            }
            else
            {
                string name = "Param_" + SqlParameters.Count;
                SqlParameters.Add(name, obj);
                Builder.Append('@');
                Builder.Append(name);
            }
        }



    }
}
