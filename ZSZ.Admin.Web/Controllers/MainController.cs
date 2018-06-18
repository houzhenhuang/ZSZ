using CaptchaGen;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZSZ.Admin.Web.Models;
using ZSZ.Common;
using ZSZ.CommonMVC;
using ZSZ.IService;

namespace ZSZ.Admin.Web.Controllers
{
    public class MainController : Controller
    {
        public IAdminUserService AdminUserService { get; set; }
        // GET: Index
        public ActionResult Index()
        {
            if (Session["LoginUserId"] == null)
            {
                return new RedirectResult("Login");
            }
            return View();
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public JsonResult Login(AdminLoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new AjaxResult { Status = "error", ErrorMsg = MVCHelper.GetValidMsg(ModelState) });
            }
            if ((string)TempData["VerifyCode"] != model.VerifyCode)
            {
                return Json(new AjaxResult { Status = "error", ErrorMsg = "验证码错误" });
            }
            bool result = AdminUserService.CheckLogin(model.PhoneNum, model.Password);
            if (result)
            {
                Session["LoginUserId"] = AdminUserService.GetByPhoneNum(model.PhoneNum).Id;
                return Json(new AjaxResult { Status = "ok" });
            }
            else
            {
                return Json(new AjaxResult { Status = "error", ErrorMsg = "用户名或者密码错误" });
            }
        }
        public ActionResult CreateVerifyCode()
        {
            string verifyCode = CommonHelper.CreateVerifyCode(4);
            TempData["VerifyCode"] = verifyCode;
            MemoryStream ms = ImageFactory.GenerateImage(verifyCode, 40, 100, 14, 1);
            return File(ms, "image/jpeg");
        }
    }
}