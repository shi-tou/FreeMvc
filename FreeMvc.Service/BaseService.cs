using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreeMvc.Dao;

namespace FreeMvc.Service
{
    /// <summary>
    /// 服务基类
    /// </summary>
    public class BaseService: IBaseService
    {
        public BaseRepository Repository = new BaseRepository();
        /// <summary>
        /// 插入数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public bool Insert<T>(T t)
        {
            return Repository.Insert<T>(t);
        }
        /// <summary>
        /// 批量插入数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public bool Insert<T>(List<T> listT)
        {
            return Repository.Insert<T>(listT);
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public bool Update<T>(T t)
        {
            return Repository.Update<T>(t);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="columnName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool Delete<T>(string columnName, string value)
        {
            return Repository.Delete<T>(columnName, value);
        }
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public T GetModel<T>(string columnName, string value)
        {
            return Repository.GetList<T>(columnName, value).FirstOrDefault();
        }
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public T GetModel<T>(Hashtable hs)
        {
            return Repository.GetList<T>(hs).FirstOrDefault();
        }
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public List<T> GetList<T>(string columnName,string value)
        {
            return Repository.GetList<T>(columnName,value);
        }
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public List<T> GetList<T>()
        {
            return Repository.GetList<T>();
        }
        /// 获取数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public List<T> GetList<T>(Hashtable hs)
        {
            return Repository.GetList<T>(hs);
        }

    }
}
