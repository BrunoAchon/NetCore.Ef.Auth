using System;
using System.Collections.Generic;
using System.Text;

namespace WS.Core.Domain
{
    public class AspNetMenu
    {
        public int MenuId { get; set; }
        public int ModuleId { get; set; }
        public virtual AspNetModule Module { get; set; }
        public string Menu { get; set; }
        public int Ordem { get; set; }
        public virtual ICollection<AspNetClientMenu> aspNetClientMenus { get; set; }
        public virtual ICollection<AspNetUserMenu> aspNetUserMenus { get; set; }
        public virtual ICollection<AspNetRoleMenu> aspNetRoleMenus { get; set; }

        public AspNetMenu()
        {
            aspNetClientMenus = new HashSet<AspNetClientMenu>();
            aspNetUserMenus = new HashSet<AspNetUserMenu>();
            aspNetRoleMenus = new HashSet<AspNetRoleMenu>();
        }
    }
}
