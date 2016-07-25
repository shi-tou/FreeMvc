using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FreeMvc.Web.Attribute
{
    /// <summary>
    /// 匿名登录 过滤器
    /// </summary>
    public class AnonymousAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
        }
    }
}