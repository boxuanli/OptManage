using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbFrame.SqlContext.Abstract
{
    using Ado;
    public class BaseClass
    {
        protected DbHelper _DbHelper { get; set; }
        protected StringBuilder _Code { get; set; }

        public BaseClass()
        {
            _DbHelper = new DbHelper();
            _Code = new StringBuilder();
        }

    }
}
