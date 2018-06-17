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
    public class AdminUserController : Controller
    {
        public IAdminUserService AdminUserService { get; set; }
        public IRoleService RoleService { get; set; }
        public ICityService CityService { get; set; }
        // GET: Role
        public ActionResult List()
        {
            var adminUser = AdminUserService.GetAll();
            return View(adminUser);
        }
        [HttpGet]
        public ActionResult Create()
        {
            var role = RoleService.GetAll();
            var city = CityService.GetAll();
            AdminUserCreateGetModel model = new AdminUserCreateGetModel();
            model.Roles = role;
            model.Citys = city;
            return View(model);
        }
        [HttpPost]
        public ActionResult Create(AdminUserCreateModel model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new AjaxResult { Status = "error", ErrorMsg = MVCHelper.GetValidMsg(ModelState) });
            }
            long adminUserId = AdminUserService.AddAdminUser(model.Name, model.PhoneNum, model.Password, model.Email, model.CityId);
            RoleService.AddRoleIds(adminUserId, model.roleIds);
            return Json(new AjaxResult { Status = "ok" });
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var adminUser = AdminUserService.GetById(id);
            var roles = RoleService.GetAll();
            var adminUserRole = RoleService.GetByAdminUserId(adminUser.Id);
            var city = CityService.GetAll();

            AdminUserEditGetModel model = new AdminUserEditGetModel();
            model.AdminUser = adminUser;
            model.AdminUserRole = adminUserRole;
            model.Roles = roles;
            model.Citys = city;
            return View(model);
        }
        [HttpPost]
        public ActionResult Edit(AdminUserEditModel model)
        {
            AdminUserService.UpdateAdminUser(model.Id, model.Name, model.PhoneNum, model.Password, model.Email, model.CityId);
            RoleService.UpdateRoleIds(model.Id, model.RoleIds);
            return Json(new AjaxResult { Status = "ok" });
        }
        public JsonResult Delete(long id)
        {
            AdminUserService.MarkDeleted(id);

            return Json(new AjaxResult { Status = "ok" });
        }
        public JsonResult BatchDelete(long[] selectId)
        {
            foreach (var roleId in selectId)
            {
                AdminUserService.MarkDeleted(roleId);
            }
            return Json(new AjaxResult { Status = "ok" });
        }
    }
}