using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZSZ.DTO;

namespace ZSZ.Admin.Web.Models
{
    public class AdminUserEditModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhoneNum { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public int? CityId { get; set; }
        public long[] RoleIds { get; set; }
    }
}