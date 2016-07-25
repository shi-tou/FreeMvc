using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FreeMvc.Domain;

namespace FreeMvc.Web
{
    /// <summary>
    /// 当前登录用户信息
    /// </summary>
    public static class LoginUser
    {
        public static UserInfo Current
        {
            get
            {
                if (HttpContext.Current.Session["CrrentUserLoginInfo"] != null)
                {
                    return HttpContext.Current.Session["CrrentUserLoginInfo"] as UserInfo;
                }
                else
                {
                    return null;
                }
            }
            set
            {
                if (HttpContext.Current.Session != null)
                    HttpContext.Current.Session["CrrentUserLoginInfo"] = value;
            }

        }
    }
}