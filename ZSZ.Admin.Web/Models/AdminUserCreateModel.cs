﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ZSZ.Admin.Web.Models
{
    public class AdminUserCreateModel
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        public string PhoneNum { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [Compare(nameof(Password))]
        public string Password2 { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public long? CityId { get; set; }
        public long[] roleIds { get; set; }
    }
}