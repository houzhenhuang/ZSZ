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
    public class RoleController : Controller
    {
        public IRoleService RoleService { get; set; }
        public IPermissionService PermService { get; set; }
        // GET: Role
        public ActionResult List()
        {
            var perm = RoleService.GetAll();
            return View(perm);
        }
        [HttpGet]
        public ActionResult Create()
        {
            var perms = PermService.GetAll();
            return View(perms);
        }
        [HttpPost]
        public ActionResult Create(RoleCreateModel model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new AjaxResult { Status = "error",ErrorMsg=MVCHelper.GetValidMsg(ModelState) });
            }
            long roleId = RoleService.AddNew(model.Name);
            PermService.AddPermIds(roleId, model.permissionIds);
            return Json(new AjaxResult { Status = "ok" });
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var role = RoleService.GetById(id);
            var perms = PermService.GetAll();
            var rolePerms = PermService.GetByRoleId(role.Id);

            RoleEditGetModel model = new RoleEditGetModel();
            model.Role = role;
            model.RolePerm = rolePerms;
            model.Permissions = perms;
            return View(model);
        }
        [HttpPost]
        public ActionResult Edit(RoleEditModel model)
        {
            RoleService.Update(model.Id, model.Name);
            PermService.UpdatePermIds(model.Id,model.PermissionIds);
            return Json(new AjaxResult { Status = "ok" });
        }
        public JsonResult Delete(long id)
        {
            RoleService.MarkDeleted(id);

            return Json(new AjaxResult { Status = "ok" });
        }
        public JsonResult BatchDelete(long[] selectId)
        {
            foreach (var roleId in selectId)
            {
                RoleService.MarkDeleted(roleId);
            }
            return Json(new AjaxResult { Status = "ok" });
        }
    }
}