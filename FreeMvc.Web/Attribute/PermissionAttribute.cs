using FreeMvc.Domain;
using FreeMvc.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FreeMvc.Web.Attribute
{
    /// <summary>
    /// 权限拦截
    /// </summary>
    public class PermissionAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            //是否匿名访问
            if (CheckAnonymous(filterContext))
            {
                return;
            }
            if (LoginUser.Current != null)
            {
                //权需要登录，不验证权限
                if (CheckOnlyNeedLogin(filterContext))
                {
                    return;
                }
                //验证权限
                //从数据库拿到用户权限信息，进行比对
                if (CheckPermission(filterContext))
                {
                    return;
                }
                else
                {
                    filterContext.HttpContext.Response.Redirect("/Home/PerssionTip");
                    return;
                }
            }
            filterContext.HttpContext.Response.Redirect("/User/Login");
        }
        /// <summary>
        /// [Anonymous标记]验证是否匿名访问
        /// </summary>
        /// <param name="filterContext"></param>
        /// <returns></returns>
        public bool CheckAnonymous(AuthorizationContext filterContext)
        {
            //验证是否是匿名访问的Action
            object[] attrs = filterContext.ActionDescriptor.GetCustomAttributes(typeof(AnonymousAttribute), true);
            //是否是Anonymous
            return attrs.Length == 1;
        }
        /// <summary>
        /// [OnlyNeedLogin标记]验证是否登录就可以访问(如果已经登陆,那么对于标识了OnlyNeedLogin的方法就不需要权限验证了)
        /// </summary>
        /// <param name="filterContext"></param>
        /// <returns></returns>
        public bool CheckOnlyNeedLogin(AuthorizationContext filterContext)
        {
            object[] attrs = filterContext.ActionDescriptor.GetCustomAttributes(typeof(OnlyNeedLogin), true);
            //是否是OnlyNeedLogin
            return attrs.Length == 1;
        }
        /// <summary>
        /// 验证分配给用记的权限
        /// </summary>
        /// <returns></returns>
        public bool CheckPermission(AuthorizationContext filterContext)
        {
            string action = filterContext.RouteData.Values["Action"].ToString();
            IUserService userService = IocFactory.GetNamedInstance<IUserService>("UserService");
            List<PermissionInfo> list = userService.GetUserPermission(LoginUser.Current.ID);
            foreach(PermissionInfo info in list)
            {
                if (info.Code == action)
                    return true;
            }
            return false;
        }
    }
}