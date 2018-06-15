using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZSZ.IService;

namespace ZSZ.Front.Web.Controllers
{
    public class HomeController : Controller
    {
        public ICityService CityService { get; set; }
        // GET: Home
        public ActionResult Index()
        {
            if (Session["str"] != null)
            {
                return Content((string)Session["str"]);
            }
            string str = "abc";
            Session["str"] = str;
            //CityService.AddNew("北京");
            return Content("ok");
        }
    }
}