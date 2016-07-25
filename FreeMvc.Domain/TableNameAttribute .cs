using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeMvc.Domain
{
    /// <summary>
    /// 实体与数据表的映射
    /// </summary>
    public class TableNameAttribute : Attribute
    {
        /// <summary>
        /// 注入属性值
        /// </summary>
        /// <param name="tableName">映射到数据库的表名</param>
        /// <param name="primaryKey">主键(默认为"ID")</param>
        public TableNameAttribute(string tableName, string primaryKey = "ID")
        {
            this.tableName = tableName;
            this.primaryKey = primaryKey;
        }
        //表名
        public string tableName;
        public string TableName
        {
            set { tableName = value; }
            get { return tableName; }
        }
        //主键
        public string primaryKey;
        public string PrimaryKey
        {
            set { primaryKey = value; }
            get { return primaryKey; }
        }
    }
}
