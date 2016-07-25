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
    public interface IBaseService
    {
        /// <summary>
        /// 插入数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        bool Insert<T>(T t);
        /// <summary>
        /// 插入数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="listT"></param>
        /// <returns></returns>
        bool Insert<T>(List<T> listT);
        /// <summary>
        /// 更新
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        bool Update<T>(T t);
        /// <summary>
        /// 删除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="columnName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        bool Delete<T>(string columnName, string value);
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        T GetModel<T>(string columnName, string value);
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        T GetModel<T>(Hashtable hs);
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        List<T> GetList<T>(string columnName, string value);
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        List<T> GetList<T>();
        /// 获取数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        List<T> GetList<T>(Hashtable hs);

    }
}
