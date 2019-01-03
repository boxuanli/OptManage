using Microsoft.AspNetCore.Mvc.Filters;
using Common;
using DbFrame;
using DbFrame.Class;

namespace Portal.AOP
{
    public class AopCheckEntityFilterAttribute : ActionFilterAttribute
    {
        private DBContext db = new DBContext();
        public string[] ParamName { get; set; }
        public AopCheckEntityFilterAttribute(string[] _ParamName)
        {
            this.ParamName = _ParamName;
        }

        /// <summary>
        /// 每次请求Action之前发生，，在行为方法执行前执行
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            if (this.ParamName != null)
            {
                foreach (var item in this.ParamName)
                {
                    var _Value = (Models.Class.BaseClass)filterContext.ActionArguments[item];
                    if (_Value != null)
                    {
                        if (!db.CheckModel(_Value))
                        {
                            throw new MessageBox(db.ErrorMessge);
                        }
                    }
                }
            }

        }
    }
}
