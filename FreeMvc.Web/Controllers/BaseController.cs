using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FreeMvc.Web.Attribute;
using FreeMvc.Web.Models;
using FreeMvc.Service;

namespace FreeMvc.Web.Controllers
{
    /// <summary>
    /// BaseController
    /// </summary>
    [PermissionAttribute]
    public class BaseController : Controller
    {
        #region Service实例
        /// <summary>
        /// 用户
        /// </summary>
        public IUserService UserService
        {
            get { return IocFactory.GetNamedInstance<IUserService>("UserService"); }
        }
        #endregion
        /// <summary>
        /// Ajax请求结果
        /// </summary>
        public AjaxResult Result { get; set; }
        /// <summary>
        /// 构造函数
        /// </summary>
        public BaseController()
        {
            Result = new AjaxResult();
        }
    }
}