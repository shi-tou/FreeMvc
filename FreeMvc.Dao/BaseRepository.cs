using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using Webdiyer.WebControls.Mvc;
using FreeMvc.Common;
using FreeMvc.Domain;

namespace FreeMvc.Dao
{
    /// <summary>
    /// 
    /// </summary>
    public class BaseRepository: IBaseRepository
    {
        public static MySqlHelper sqlHelper { get; set; }
        static BaseRepository()
        {
            sqlHelper = new MySqlHelper(System.Configuration.ConfigurationManager.AppSettings["Mysql"]);
        }

        #region 添加、修改、删除
        /// <summary>
        /// 插入记录
        /// </summary>
        /// <param name="h"></param>
        /// <returns></returns>
        public bool Insert<T>(T t)
        {
            if (t != null)
            {
                string sql = "insert into {0} ({1}) values ({2});";
                string cols = "";
                string vals = "";
                List<MySqlParameter> param = new List<MySqlParameter>();
                Type type = typeof(T);
                PropertyInfo[] propertys = type.GetProperties();
                var attribute = type.GetCustomAttributes(typeof(TableNameAttribute), false).FirstOrDefault();
                if (attribute == null)
                {
                    throw new Exception("类" + type.Name + "必须添加'TableNameAttribute'属性");
                }
                string tableName = ((TableNameAttribute)attribute).TableName;
                foreach (PropertyInfo pi in propertys)
                {
                    string name = pi.Name;
                    if (cols != "")
                    {
                        cols += ",";
                        vals += ",";
                    }
                    cols += name ;
                    vals += string.Format("?{0}", name);
                    param.Add(new MySqlParameter(name, type.GetProperty(name).GetValue(t, null)));
                }
                sql = string.Format(sql, tableName, cols, vals);
                return sqlHelper.ExecuteNonQuery(sql, param.ToArray()) > 0;
            }
            else { return false; }
        }
        /// <summary>
        /// 批量插入记录
        /// </summary>
        /// <param name="h"></param>
        /// <returns></returns>
        public bool Insert<T>(List<T> listT)
        {
            if (listT != null && listT.Count > 0)
            {
                List<MySqlParameter[]> listParam = new List<MySqlParameter[]>();
                List<string> listSql = new List<string>();
                string sqlTemp = "insert into {0} ({1}) values ({2});";
                
                Type type = typeof(T);
                PropertyInfo[] propertys = type.GetProperties();
                var attribute = type.GetCustomAttributes(typeof(TableNameAttribute), false).FirstOrDefault();
                if (attribute == null)
                {
                    throw new Exception("类" + type.Name + "必须添加'TableNameAttribute'属性");
                }
                string tableName = ((TableNameAttribute)attribute).TableName;
                int index = 0;
                foreach (T t in listT)
                {
                    string cols = "";
                    string vals = "";
                    List<MySqlParameter> param = new List<MySqlParameter>();
                    foreach (PropertyInfo pi in propertys)
                    {
                        string name = pi.Name;
                        if (cols != "")
                        {
                            cols += ",";
                            vals += ",";
                        }
                        cols += name;
                        vals += string.Format("?{0}", name + index.ToString());
                        param.Add(new MySqlParameter(pi.Name + index.ToString(), type.GetProperty(pi.Name).GetValue(t, null)));
                    }
                    listSql.Add(string.Format(sqlTemp, tableName, cols, vals));
                    listParam.Add(param.ToArray());
                    index++;
                }
                sqlHelper.BatchInsert(listSql, listParam);
                return true;
            }
            else { return false; }
        }
        /// <summary>
        /// 更新记录
        /// </summary>
        /// <param name="h"></param>
        /// <returns></returns>
        public bool Update<T>(T t)
        {
            if (t != null)
            {
                string sql = "update {0} set {1} where {2};";
                string sqlSet = "";
                string sqlWhere = "";
                List<MySqlParameter> param = new List<MySqlParameter>();
                Type type = typeof(T);
                PropertyInfo[] propertys = type.GetProperties();
                var attribute = type.GetCustomAttributes(typeof(TableNameAttribute), false).FirstOrDefault();
                if (attribute == null)
                {
                    throw new Exception("类" + type.Name + "必须添加'TableNameAttribute'属性");
                }
                string tableName = ((TableNameAttribute)attribute).TableName;
                string primaryKey = ((TableNameAttribute)attribute).primaryKey;
                foreach (PropertyInfo pi in propertys)
                {
                    string name = pi.Name;
                    object value = type.GetProperty(name).GetValue(t, null);
                    //为null则不更新
                    if (value == null || name.ToLower() == "createtime" || name.ToLower() == "createby")
                    {
                        continue;
                    }
                    //主键(作为更新条件)
                    if (name == primaryKey)
                    {
                        sqlWhere = string.Format("{0}=?{0}", name);
                    }
                    else
                    {
                        if (sqlSet != "")
                        {
                            sqlSet += ",";
                        }
                        sqlSet += string.Format("{0}=?{0}", name);
                    }
                    param.Add(new MySqlParameter(name, type.GetProperty(name).GetValue(t, null)));
                }
                sql = string.Format(sql, tableName, sqlSet, sqlWhere);
                return sqlHelper.ExecuteNonQuery(sql, param.ToArray()) > 0;
            }
            else { return false; }
        }
        /// <summary>
        /// 删除记录
        /// </summary>
        public bool Delete<T>()
        {
            return Delete<T>("", "");
        }
        /// <summary>
        /// 删除记录
        /// </summary>
        public bool Delete<T>( string columnName, object value)
        {
            Type type = typeof(T);
            PropertyInfo[] propertys = type.GetProperties();
            var attribute = type.GetCustomAttributes(typeof(TableNameAttribute), false).FirstOrDefault();
            if (attribute == null)
            {
                throw new Exception("类" + type.Name + "必须添加'TableNameAttribute'属性");
            }
            string tableName = ((TableNameAttribute)attribute).TableName;
            StringBuilder sbSql = new StringBuilder();
            MySqlParameter[] param = null;
            if (columnName == "")
                sbSql.AppendFormat("delete from {0}", tableName);
            else
            {
                sbSql.AppendFormat("delete from {0} where {1}=@{1}", tableName, columnName);
                param = new MySqlParameter[]
                {
                    new MySqlParameter(columnName,value)
                };
            }
            return sqlHelper.ExecuteNonQuery(CommandType.Text, sbSql.ToString(), param) > 0;
        }
        #endregion

        #region List
        /// <summary>
        /// 获取指定条件的数据
        /// </summary>
        public List<T> GetList<T>()
        {
            return GetList<T>("", "");
        }
        /// <summary>
        /// 获取指定条件的数据
        /// </summary>
        public List<T> GetList<T>(string columnName, object value)
        {
            Hashtable hs = new Hashtable();
            if (columnName != "")
            {
                hs.Add(columnName, value);
            }
            return GetList<T>(hs);
        }
        /// <summary>
        /// 获取指定条件的数据
        /// </summary>
        /// <param name="tablename"></param>
        /// <param name="hs"></param>
        /// <returns></returns>
        public List<T> GetList<T>(Hashtable hs)
        {
            Type type = typeof(T);
            PropertyInfo[] propertys = type.GetProperties();
            var attribute = type.GetCustomAttributes(typeof(TableNameAttribute), false).FirstOrDefault();
            if (attribute == null)
            {
                throw new Exception("类" + type.Name + "必须添加'TableNameAttribute'属性");
            }
            string tableName = ((TableNameAttribute)attribute).TableName;
            string sql = "select * from " + tableName + " where 1=1";
            List<MySqlParameter> param = new List<MySqlParameter>();
            foreach (string key in hs.Keys)
            {
                sql += string.Format(" and {0}=?{0}", key);
                param.Add(new MySqlParameter(key, hs[key]));
            }
            DataTable dt = sqlHelper.ExecuteDataTable(CommandType.Text, sql, param.ToArray());
            dt.TableName = tableName;
            return ObjectHelper.CopyToObjects<T>(dt).ToList() ?? default(List<T>);
            
        }
        /// <summary>
        /// 获取指定sql的数据
        /// </summary>
        /// <param name="tablename"></param>
        /// <param name="hs"></param>
        /// <returns></returns>
        public List<T> GetList<T>(string sql, MySqlParameter[] param, CommandType commandType)
        {
            DataTable dt = sqlHelper.ExecuteDataTable(CommandType.Text, sql, param.ToArray());
            return ObjectHelper.CopyToObjects<T>(dt).ToList() ?? default(List<T>);
        }
        #endregion

        #region 执行Sql语句
        /// <summary>
        /// 执行sql语句
        /// </summary>
        /// <param name="cType">执行类型</param>
        /// <param name="sql">sql语句</param>
        /// <returns>影响记录数</returns>
        public int ExecteSql(string sql, CommandType commandType = CommandType.Text)
        {
            return sqlHelper.ExecuteNonQuery(commandType, sql);
        }
        /// <summary>
        /// 执行sql语句
        /// </summary>
        /// <param name="cType">执行类型</param>
        /// <param name="sql">sql语句</param>
        /// <returns>影响记录数</returns>
        public int ExecteSql(string sql, MySqlParameter[] param, CommandType commandType = CommandType.Text)
        {
            return sqlHelper.ExecuteNonQuery(commandType, sql, param);
        }
        /// <summary>
        /// 执行sql语句
        /// </summary>
        /// <param name="cType">执行类型</param>
        /// <param name="sql">sql语句</param>
        /// <returns>结果集DataSet</returns>
        public DataSet ExecteSqlDataSet(string sql, CommandType commandType = CommandType.Text)
        {
            return sqlHelper.ExecuteDataSet(commandType, sql);
        }
        /// <summary>
        /// 执行sql语句
        /// </summary>
        /// <param name="cType">执行类型</param>
        /// <param name="sql">sql语句</param>
        /// <returns>表数据DataTable</returns>
        public DataTable ExecteSqlGetDataTable(string sql, CommandType commandType = CommandType.Text)
        {
            return sqlHelper.ExecuteDataTable(commandType, sql);
        }
        #endregion

        #region 分页
        /// <summary>
        /// 分页
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public virtual PagedList<T> GetPageList<T>(string sql, MySqlParameter[] param, int pageIndex, int pageSize,string orderBy)
        {
            PagedList<T> pageList = null;
            if (!string.IsNullOrEmpty(orderBy))
            {
                sql += " order by " + orderBy;
            }
            int totalItemCount = sqlHelper.ExecuteScalar<int>("select count(1) from (" + sql + ") as A", param);
            sql += " limit " + (pageIndex - 1) * pageSize + "," + pageSize + "";
            DataTable dt = sqlHelper.ExecuteDataTable(sql, param);
            T[] list = ObjectHelper.CopyToObjects<T>(dt);
            pageList = new PagedList<T>(list, pageIndex, pageSize, totalItemCount);
            return pageList;
        }
        /// <summary>
        /// 分页
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="selectSql"></param>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public virtual PagedList<T> GetPageList<T>(string selectSql, string sql, MySqlParameter[] param, int pageIndex, int pageSize)
        {
            PagedList<T> pageList = null;
            int totalItemCount = sqlHelper.ExecuteScalar<int>("select count(1) " + sql, param);
            sql = selectSql + sql + " limit " + (pageIndex - 1) * pageSize + "," + pageSize + "";
            DataTable dt = sqlHelper.ExecuteDataTable(sql, param);
            List<T> list = ObjectHelper.CopyToObjects<T>(dt).ToList();
            pageList = new PagedList<T>(list, pageIndex, pageSize, totalItemCount);
            return pageList;
        }
        #endregion
    }
}
