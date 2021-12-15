using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace WS.Core.Domain
{
    public class AspNetRole : IdentityRole<int>
    {
        public virtual ICollection<AspNetUserRole> aspNetUserRoles { get; set; }
        public virtual ICollection<AspNetRoleModule> aspNetRoleModule { get; set; }
        public virtual ICollection<AspNetRoleMenu> aspNetRoleMenus { get; set; }

        public AspNetRole()
        {
            aspNetUserRoles = new HashSet<AspNetUserRole>();
            aspNetRoleModule = new HashSet<AspNetRoleModule>();
            aspNetRoleMenus = new HashSet<AspNetRoleMenu>();
        }
    }
}
