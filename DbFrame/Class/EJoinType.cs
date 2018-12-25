using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbFrame.Class
{
    public enum EJoinType
    {
        /// <summary>
        /// 内连接 inner join
        /// </summary>
        INNER_JOIN,

        /// <summary>
        /// 左连接 left join
        /// </summary>
        LEFT_JOIN,

        /// <summary>
        /// 左外连接 left outer join
        /// </summary>
        LEFT_OUTER_JOIN,

        /// <summary>
        /// 右连接 right join
        /// </summary>
        RIGHT_JOIN,

        /// <summary>
        /// 右外连接 right outer join
        /// </summary>
        RIGHT_OUTER_JOIN,

        /// <summary>
        /// 全连接 full join
        /// </summary>
        FULL_JOIN,

        /// <summary>
        /// 全外连接 full outer join
        /// </summary>
        FULL_OUTER_JOIN,

        /// <summary>
        /// 交叉连接 cross join
        /// </summary>
        CROSS_JOIN


    }
}
