using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace WS.Core.Domain
{
    public class AspNetUser : IdentityUser<int>
    {
        public virtual ICollection<AspNetUserRole> aspNetUserRoles { get; set; }
        public virtual ICollection<AspNetUserModule> aspNetUserModules { get; set; }
        public virtual ICollection<AspNetUserMenu> aspNetUserMenus { get; set; }
        public AspNetUser()
        {
            aspNetUserRoles = new HashSet<AspNetUserRole>();
            aspNetUserModules = new HashSet<AspNetUserModule>();
            aspNetUserMenus = new HashSet<AspNetUserMenu>();
        }
    }
}
