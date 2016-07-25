using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using FreeMvc.Dao;
using FreeMvc.Domain;

namespace FreeMvc.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            //StructureMap注册
            IocFactory.ConfigureStructureMap();
        }
    }
}
