using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Reflection;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace FreeMvc.Common
{
    public sealed class ObjectHelper
    {
        /// <summary>
        /// 类实例转DataRow
        /// </summary>
        /// <param name="dtview"></param>
        /// <param name="pt"></param>
        /// <returns></returns>
        public static DataRow CopyToDataRow<T>(T obj)
        {
            Type t = typeof(T);
            DataTable dt = new DataTable();
            foreach (PropertyInfo pi in t.GetProperties())
            {
                dt.Columns.Add(new DataColumn(pi.Name, pi.PropertyType));
            }
            return FillDataRow(dt, t, obj);
        }
        /// <summary>
        /// 类实例转DataTable
        /// </summary>
        /// <param name="dtview"></param>
        /// <param name="pt"></param>
        /// <returns></returns>
        public static DataTable CopyToDataTable<T>(T obj,string tableName)
        {
            Type t = typeof(T);
            DataTable dt = new DataTable();
            dt.TableName = tableName;
            FieldInfo[] temp = t.GetFields();
            foreach (FieldInfo f in temp)
            {
                dt.Columns.Add(new DataColumn(f.Name, f.FieldType));
            }

            dt.Rows.Add(FillDataRowFields(dt, t, obj));
            return dt;
        }

        public static void test<T>(T obj)
        {
            Type type = typeof(T);
            FieldInfo[] temp = type.GetFields();
            foreach (FieldInfo item in temp)
            {
                string s = item.Name;
                string d = item.FieldType.Name;
            }
        }


        public static DataTable CopyToDataTable<T>(T[] objs)
        {
            Type t = typeof(T);
            DataTable dt = new DataTable();
            foreach (PropertyInfo pi in t.GetProperties())
            {
                dt.Columns.Add(new DataColumn(pi.Name, pi.PropertyType));
            }
            foreach (object obj in objs)
            {
                dt.Rows.Add(FillDataRow(dt, t, obj));
            }
            return dt;
        }

        private static DataRow FillDataRowFields(DataTable dt, Type t, object obj)
        {
            DataRow dr = dt.NewRow();
            //循环所有列
            DataColumnCollection dcc = dt.Columns;
            for (int i = 0; i < dcc.Count; i++)
            {
                String colName = dcc[i].ColumnName;
                FieldInfo pInfo = t.GetField(colName);
                if (pInfo != null)
                {
                    object o = pInfo.GetValue(obj);
                    if ((o != null) && (!Convert.IsDBNull(o) && o.ToString() != null))
                    {
                        dr[colName] = o;
                    }
                }
            }
            return dr;
        }

        private static DataRow FillDataRow(DataTable dt, Type t, object obj)
        {
            DataRow dr = dt.NewRow();
            //循环所有列
            DataColumnCollection dcc = dt.Columns;
            for (int i = 0; i < dcc.Count; i++)
            {
                String colName = dcc[i].ColumnName;
                PropertyInfo pInfo = t.GetProperty(colName);
                if (pInfo != null)
                {
                    object o = pInfo.GetValue(obj, null);
                    if ((o != null) && (!Convert.IsDBNull(o) && o.ToString() != null))
                    {
                        dr[colName] = o;
                    }
                }
            }
            return dr;
        }
        /// <summary>
        /// DataRow转对象
        /// </summary>
        /// <param name="pDataRow"></param>
        /// <param name="pType"></param>
        /// <returns></returns>
        public static T CopyToObject<T>(DataRow pDataRow)
        {
            Object proValue = null;
            PropertyInfo propertyInfo = null;
            FieldInfo fieldInfo = null;// added by chenyh 2012-07-03 支持成员变量
            T t = Activator.CreateInstance<T>();

            if (pDataRow != null)
            {
                //动态创建类的实例 
                foreach (DataColumn dc in pDataRow.Table.Columns)
                {
                    //忽略绑定时的大小写 
                    propertyInfo = t.GetType().GetProperty(dc.ColumnName, BindingFlags.Public
                        | BindingFlags.Instance | BindingFlags.IgnoreCase);
                    fieldInfo = t.GetType().GetField(dc.ColumnName, BindingFlags.Public
                        | BindingFlags.Instance | BindingFlags.IgnoreCase);
                    proValue = pDataRow[dc];
                    //当值不为空时
                    if (proValue != DBNull.Value)
                    {
                        if (propertyInfo != null)
                        {   //给属性赋值
                            propertyInfo.SetValue(t, Convert.ChangeType(proValue, propertyInfo.PropertyType), null);
                        }
                        else if (fieldInfo != null)
                        {
                            fieldInfo.SetValue(t, Convert.ChangeType(proValue, fieldInfo.FieldType));
                        }
                        else
                        {
                            continue;
                        }
                    }
                }
            }
            return t;
        }

        public static T[] CopyToObjects<T>(DataTable dt)
        {
            T[] objs = new T[dt.Rows.Count];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                objs[i] = CopyToObject<T>(dt.Rows[i]);
            }
            return objs;
        }

        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string SerializeObject(object obj)
        {
            if (obj == null)
                return "";
            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream ms = new MemoryStream();
            bf.Serialize(ms, obj);
            byte[] b = ms.ToArray();
            string s = Convert.ToBase64String(b);
            return s;
        }

        /// <summary>
        /// 反序列化
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static object DeserializeObject(string str)
        {
            if (string.IsNullOrEmpty(str))
                return null;
            byte[] b = Convert.FromBase64String(str);
            Stream s = new MemoryStream(b);
            BinaryFormatter bf = new BinaryFormatter();
            return bf.Deserialize(s);
        }
        #region 对象赋值
        /// <summary>
        /// 复制
        /// </summary>
        /// <param name="target">目标</param>
        /// <param name="source">来源</param>
        /// <returns>成功复制的值个数</returns>
        public static int Copy(object target, object source)
        {
            if (target == null || source == null)
            {
                return 0;
            }
            return Copy(target, source, source.GetType());
        }
        /// <summary>
        /// 复制
        /// </summary>
        /// <param name="target">目标</param>
        /// <param name="source">来源</param>
        /// <param name="type">复制的属性字段模板</param>
        /// <param name="excludeName">排除下列名称的属性不要复制</param>
        /// <returns>成功复制的值个数</returns>
        public static int Copy(object target, object source, Type type)
        {
            if (target == null || source == null)
            {
                return 0;
            }
            int i = 0;
            Type desType = target.GetType();
            foreach (FieldInfo mi in type.GetFields())
            {
                try
                {
                    FieldInfo des = desType.GetField(mi.Name);
                    if (des != null && des.FieldType == mi.FieldType)
                    {
                        des.SetValue(target, mi.GetValue(source));
                        i++;
                    }
                }
                catch
                {
                }
            }
            foreach (PropertyInfo pi in type.GetProperties())
            {
                try
                {
                    PropertyInfo des = desType.GetProperty(pi.Name);
                    if (des != null && des.PropertyType == pi.PropertyType && des.CanWrite && pi.CanRead)
                    {
                        des.SetValue(target, pi.GetValue(source, null), null);
                        i++;
                    }
                }
                catch
                {
                    //throw ex;
                }
            }
            return i;
        }
        #endregion
    }

}
