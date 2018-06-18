using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZSZ.Admin.Web
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class CustomPermissionAttribute : Attribute
    {
        public string Permission { get; set; }
        public CustomPermissionAttribute(string perm)
        {
            this.Permission = perm;
        }
    }
}