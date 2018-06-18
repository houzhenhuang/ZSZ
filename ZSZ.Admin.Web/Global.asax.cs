using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using ZSZ.CommonMVC;

namespace ZSZ.Admin.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            log4net.Config.XmlConfigurator.Configure();
            ModelBinders.Binders.Add(typeof(string), new TrimToDBCModelBinder());
            ModelBinders.Binders.Add(typeof(int), new TrimToDBCModelBinder());
            ModelBinders.Binders.Add(typeof(long), new TrimToDBCModelBinder());
            ModelBinders.Binders.Add(typeof(double), new TrimToDBCModelBinder());
            AutofacConfig.RegisterAutofac();
            FilterConfig.RegisterFilters(GlobalFilters.Filters);

            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}
