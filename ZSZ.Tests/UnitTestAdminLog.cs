using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ZSZ.Service;

namespace ZSZ.Tests
{
    [TestClass]
    public class UnitTestAdminLog
    {

        [TestMethod]
        public void TestAddNew()
        {
            AdminLogService service = new AdminLogService();
            long logId = service.AddNew(5, "测试消息");
            Assert.AreEqual(service.GetById(logId).Message, "测试消息");
        }
    }
}
