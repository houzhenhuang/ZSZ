using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZSZ.DTO;

namespace ZSZ.Admin.Web.Models
{
    public class RoleEditGetModel
    {
        public RoleDTO Role { get; set; }
        public PermissionDTO[] RolePerm { get; set; }
        public PermissionDTO[] Permissions { get; set; }
    }
}