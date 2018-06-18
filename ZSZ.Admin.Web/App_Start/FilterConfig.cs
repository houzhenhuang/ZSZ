using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZSZ.CommonMVC;

namespace ZSZ.Admin.Web
{
    public class FilterConfig
    {
        public static void RegisterFilters(GlobalFilterCollection filters)
        {
            filters.Add(new ExceptionFilter());
            filters.Add(new JsonNetActionFilter());
            filters.Add(new CustomAuthorizeFilter());
        }
    }
}