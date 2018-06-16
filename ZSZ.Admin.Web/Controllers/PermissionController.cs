using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZSZ.Admin.Web.Models;
using ZSZ.CommonMVC;
using ZSZ.IService;

namespace ZSZ.Admin.Web.Controllers
{
    public class PermissionController : Controller
    {
        public IPermissionService PermService { get; set; }
        // GET: Permission
        public ActionResult List()
        {
            var perm = PermService.GetAll();
            return View(perm);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(PermissionCreateModel model)
        {
            PermService.AddPermission(model.Name, model.Description);
            return Json(new AjaxResult { Status = "ok" });
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var perm = PermService.GetById(id);
            return View(perm);
        }
        [HttpPost]
        public ActionResult Edit(PermissionEditModel model)
        {
            PermService.UpdatePermission(model.Id, model.Name, model.Description);
            return Json(new AjaxResult { Status = "ok" });
        }
        public JsonResult Delete(long id)
        {
            PermService.MarkDeleted(id);

            return Json(new AjaxResult { Status = "ok" });
        }
    }
}