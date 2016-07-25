using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FreeMvc.Web.Attribute;
using FreeMvc.Domain;
using System.Text;

namespace FreeMvc.Web.Controllers
{
    [PermissionAttribute]
    public class HomeController : BaseController
    {
        /// <summary>
        /// UI主框架
        /// </summary>
        /// <returns></returns>
        [OnlyNeedLogin]
        public ActionResult Index()
        {
            List<PermissionInfo> permission = UserService.GetUserPermission(LoginUser.Current.ID);
            var listP1 = from p in permission
                         where p.Type == 1
                         orderby p.Sort
                         select p;
            StringBuilder sb = new StringBuilder();
            foreach (PermissionInfo p1 in listP1)
            {
                sb.Append(string.Format("<li><a><i class=\"fa fa-{0}\"></i><span class=\"nav-label\">{1}</span><span class=\"fa arrow\"></span></a>",p1.Icon, p1.Name));
                sb.Append("<ul class=\"nav nav-second-level\">");
                var listP2 = from p in permission
                             where p.Type == 2 && p.ParentID == p1.ID
                             orderby p.Sort
                             select p;
                foreach (PermissionInfo p2 in listP2)
                {
                    sb.Append(string.Format("<li><a class=\"J_menuItem\" href=\"{0}\" data-index=\"0\">{1}</a></li>", p2.Url, p2.Name));
                }
                sb.Append("</ul>");
                sb.Append("</li>");
            }
            ViewBag.MenuList = sb.ToString();
            return View();
        }
        /// <summary>
        /// 欢迎页
        /// </summary>
        /// <returns></returns>
        [OnlyNeedLogin]
        public ActionResult Welcome()
        {
            return View();
        }
        /// <summary>
        /// 欢迎页
        /// </summary>
        /// <returns></returns>
        [OnlyNeedLogin]
        public ActionResult PerssionTip()
        {
            return View();
        }
    }
}