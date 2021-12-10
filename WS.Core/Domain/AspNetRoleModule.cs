using System;
using System.Collections.Generic;
using System.Text;

namespace WS.Core.Domain
{
    public class AspNetRoleModule
    {
        public int RoleId { get; set; }
        public AspNetRole aspNetRole { get; set; }
        public int ModuleId { get; set; }
        public AspNetModule aspNetModule { get; set; }
    }
}
