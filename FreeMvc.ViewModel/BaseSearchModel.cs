using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FreeMvc.ViewModel
{
    /// <summary>
    /// 搜索基类
    /// </summary>
    public class BaseRequest
    {
        public int pageSize = 10;
        public int pageIndex = 1;
        /// <summary>
        /// 页大小
        /// </summary>
        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = value; }
        }
        /// <summary>
        /// 页索引
        /// </summary>
        public int PageIndex
        {
            get { return pageIndex; }
            set { pageIndex = value; }
        }
        /// <summary>
        /// 排序
        /// </summary>
        public string OrderBy { get; set; }
    }
}