using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeMvc.Domain
{
    /// <summary>
    /// 权限信息
    /// </summary>
    [TableName("T_Permission")]
    public class PermissionInfo
    {
        public string ID { get; set; }
        /// <summary>
        /// 权限类型(1-模块 2-主窗体 3-工具栏)
        /// </summary>
        public int Type { get; set; }
        /// <summary>
        /// 权限名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 权限编码
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 父ID
        /// </summary>
        public string ParentID { get; set; }
        /// <summary>
        /// Icon图标
        /// </summary>
        public string Icon { get; set; }
        /// <summary>
        /// Url
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// 排序号
        /// </summary>
        public int Sort { get; set; }
    }
}
