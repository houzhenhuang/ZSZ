using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZSZ.DTO;

namespace ZSZ.Admin.Web.Models
{
    public class AdminUserEditGetModel
    {
        public AdminUserDTO AdminUser { get; set; }
        public RoleDTO[] AdminUserRole { get; set; }
        public RoleDTO[] Roles { get; set; }
        public List<CityDTO> Citys { get; set; }
    }
}