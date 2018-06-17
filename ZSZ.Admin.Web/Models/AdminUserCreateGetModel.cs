using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZSZ.DTO;

namespace ZSZ.Admin.Web.Models
{
    public class AdminUserCreateGetModel
    {
        public CityDTO[] Citys { get; set; }
        public RoleDTO[] Roles { get; set; }
    }
}