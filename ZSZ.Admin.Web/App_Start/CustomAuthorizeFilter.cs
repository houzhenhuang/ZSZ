using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZSZ.CommonMVC;
using ZSZ.IService;

namespace ZSZ.Admin.Web
{
    public class CustomAuthorizeFilter : IAuthorizationFilter
    {
        //public IAdminUserService AdminUserService { get; set; }
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            IAdminUserService AdminUserService = DependencyResolver.Current.GetService<IAdminUserService>();
            CustomPermissionAttribute[] permAttr = (CustomPermissionAttribute[])filterContext.ActionDescriptor.GetCustomAttributes(typeof(CustomPermissionAttribute), false);
            if (permAttr.Length == 0)
            {
                return;
            }
            long? userId = (long?)filterContext.HttpContext.Session["LoginUserId"];
            if (userId == null)
            {
                //filterContext.Result = new ContentResult() { Content = "没有登录" };
                if (filterContext.HttpContext.Request.IsAjaxRequest())
                {
                    AjaxResult ajaxResult = new AjaxResult();
                    ajaxResult.Status = "redirect";
                    ajaxResult.Data = "/Main/Login";
                    ajaxResult.ErrorMsg = "没有登录";
                    filterContext.Result = new JsonNetResult() { Data = ajaxResult };
                }
                else
                {
                    filterContext.Result = new RedirectResult("/Main/Login");
                }
                return;
            }
            foreach (var attr in permAttr)
            {
                if (!AdminUserService.HasPermission(userId.Value, attr.Permission))
                {
                    if (filterContext.HttpContext.Request.IsAjaxRequest())
                    {
                        AjaxResult ajaxResult = new AjaxResult();
                        ajaxResult.Status = "error";
                        ajaxResult.ErrorMsg = "没有权限" + attr.Permission;
                        filterContext.Result = new JsonNetResult() { Data = ajaxResult };
                    }
                    else
                    {
                        filterContext.Result = new ContentResult() { Content = "对不起，你没有权限" };
                    }
                    return;
                }
            }
        }
    }
}