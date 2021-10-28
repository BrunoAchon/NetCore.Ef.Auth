using System.Collections.Generic;

namespace WS.Core.Domain
{
    public class AspNetModule
    {
        public int ModuleId { get; set; }
        public string Module { get; set; }
        public string ImgMenu { get; set; }
        public int Ordem { get; set; }

        //muitos AspNetModule pra muitos AspNetClientModule
        public virtual ICollection<AspNetClientModule> aspNetClientModules { get; set; }

        //um AspNetModule pra muitos AspNetMenu
        public virtual ICollection<AspNetMenu> aspNetMenus { get; set; }

        public AspNetModule()
        {
            aspNetClientModules = new HashSet<AspNetClientModule>();
            aspNetMenus = new HashSet<AspNetMenu>();
        }
    }
}
