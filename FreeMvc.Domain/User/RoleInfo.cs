using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeMvc.Domain
{
    /// <summary>
    /// 角色信息
    /// </summary>
    [TableName("T_Role")]
    public class RoleInfo
    {
        public string ID { get; set; }
        /// <summary>
        /// 角色名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Remark { get; set; }
    }
}
