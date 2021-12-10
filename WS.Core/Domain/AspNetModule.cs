using System.Collections.Generic;

namespace WS.Core.Domain
{
    public class AspNetModule
    {
        public int ModuleId { get; set; }
        public string Module { get; set; }
        public string ImgMenu { get; set; }
        public int Ordem { get; set; }

        public virtual ICollection<AspNetClientModule> aspNetClientModules { get; set; }
        public virtual ICollection<AspNetClientMenu> aspNetClientMenus { get; set; }

        public virtual ICollection<AspNetUserModule> aspNetUserModules { get; set; }
        public virtual ICollection<AspNetUserMenu> aspNetUserMenus { get; set; }

        public virtual ICollection<AspNetRoleModule> aspNetRoleModules { get; set; }
        public virtual ICollection<AspNetRoleMenu> aspNetRoleMenus { get; set; }

        public virtual ICollection<AspNetMenu> aspNetMenus { get; set; }

        public AspNetModule()
        {
            aspNetClientModules = new HashSet<AspNetClientModule>();
            aspNetClientMenus = new HashSet<AspNetClientMenu>();
            aspNetMenus = new HashSet<AspNetMenu>();
        }
    }
}
