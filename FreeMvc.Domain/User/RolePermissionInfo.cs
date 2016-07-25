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
    [TableName("T_RolePermission")]
    public class RolePermissionInfo
    {
        /// <summary>
        /// 角色ID
        /// </summary>
        public string RoleID { get; set; }
        /// <summary>
        /// 权限ID
        /// </summary>
        public string PermissionID { get; set; }
    }
}
