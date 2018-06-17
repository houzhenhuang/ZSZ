using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ZSZ.Admin.Web.Models
{
    public class RoleCreateModel
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        public long[] permissionIds { get; set; }
    }
}