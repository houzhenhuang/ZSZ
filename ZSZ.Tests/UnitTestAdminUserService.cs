using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZSZ.Service;

namespace ZSZ.Tests
{
    [TestClass]
    public class UnitTestAdminUserService
    {
        private AdminUserService service = new AdminUserService();
        [TestMethod]
        public void TestAddAdminUser()
        {
            long userId = service.AddAdminUser("hhz", "13680398478", "123", "123@qq.com", null);
            var userInfo = service.GetById(userId);
            Assert.AreEqual(userInfo.Name, "hhz");
            Assert.AreEqual(userInfo.PhoneNum, "13680398478");
            Assert.AreEqual(userInfo.Email, "123@qq.com");
            Assert.IsNull(userInfo.CityId);
            Assert.IsTrue(service.CheckLogin("13680398478", "123"));
            Assert.IsFalse(service.CheckLogin("13680398478", "1242"));

            service.GetAll();
            Assert.IsNotNull(service.GetByPhoneNum("13680398478"));

            service.MarkDeleted(userId);
        }
        [TestMethod]
        public void TestHasPerm()
        {
            try
            {
                PermissionService permService = new PermissionService();
                string permName1 = Guid.NewGuid().ToString();
                long permId1 = permService.AddPermission(permName1, permName1);
                string permName2 = Guid.NewGuid().ToString();
                long permId2 = permService.AddPermission(permName2, permName2);

                RoleService roleService = new RoleService();
                string roleName = Guid.NewGuid().ToString();
                long roleId = roleService.AddNew(roleName);

                string userPhone = "136138";
                long userId = new AdminUserService().AddAdminUser("aaa", userPhone, "123", "123@qq.com", null);

                roleService.AddRoleIds(userId, new long[] { roleId });
                permService.AddPermIds(roleId, new long[] { permId1 });

                Assert.IsTrue(service.HasPermission(userId, permName1));
                Assert.IsFalse(service.HasPermission(userId, permName2));

                service.MarkDeleted(userId);
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var item in ex.EntityValidationErrors.SelectMany(err => err.ValidationErrors))
                {
                    Console.WriteLine(item.ErrorMessage);
                }
            }

        }


    }
}
