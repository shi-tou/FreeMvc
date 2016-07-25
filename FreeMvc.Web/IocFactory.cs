using FreeMvc.Service;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FreeMvc.Web
{
    public class IocFactory
    {
        public static IContainer container;
        /// <summary>
        /// 配置StructureMap，在程序入口调用IocFactory.ConfigureStructureMap()进行注册（如Global.asax文件）
        /// </summary>
        public static void ConfigureStructureMap()
        {
            container = new Container(c => { c.AddRegistry<StructureMapRegistry>(); });
        }
        /// <summary>
        /// 当使用GetInstance<Ixxx>()时，将会使用最后一个实现Ixxx的实例
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T GetInstance<T>()
        {
            return container.GetInstance<T>();
        }
        /// <summary>
        /// 根据服务名来获取对应实例
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="serviceName"></param>
        /// <returns></returns>
        public static T GetNamedInstance<T>(string serviceName)
        {
            return container.GetInstance<T>(serviceName);
        }
    }
    /// <summary>
    /// Ioc构造服务实例
    /// </summary>
    public class StructureMapRegistry : Registry
    {
        public StructureMapRegistry()
        {
            For<IUserService>().Singleton().Use<UserService>().Named("UserService");
        }
    }
}