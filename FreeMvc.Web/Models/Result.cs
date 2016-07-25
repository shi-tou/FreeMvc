using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FreeMvc.Web.Models
{
    /// <summary>
    /// Ajax请求结果
    /// </summary>
    public class AjaxResult
    {
        public bool IsOk = true;
        public string Msg = "操作成功";
        public string RedirectUrl = "";
    }
}