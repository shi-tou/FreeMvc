using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FreeMvc.Web.Attribute
{
    /// <summary>
    /// 权需要登录就可以访问
    /// </summary>
    public class OnlyNeedLogin : ActionFilterAttribute
    {
    }
}