using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZSZ.DTO;

namespace ZSZ.Admin.Web.Models
{
    public class RoleEditModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public long[] PermissionIds { get; set; }
    }
}