using Autofac;
using Autofac.Integration.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace ZSZ.Admin.Web
{
    public class AutofacConfig
    {
        public static void RegisterAutofac()
        {
            var build = new ContainerBuilder();
            build.RegisterControllers(typeof(MvcApplication).Assembly).PropertiesAutowired();
            build.RegisterAssemblyTypes(Assembly.Load("ZSZ.Service"))
                .Where(type => !type.IsAbstract
                    && typeof(IServiceSupport).IsAssignableFrom(type))//type1.IsAssignableFrom(type2):type1类型的变量是否可以指向type2类型的对象，换一种说法，type2是否实现了type1接口
                .AsImplementedInterfaces()
                .PropertiesAutowired();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(build.Build()));
        }
    }
}