using System;
using System.Collections.Generic;
using System.Text;

namespace WS.Core.Domain
{
    public class AspNetUserModule
    {
        public int UserId { get; set; }
        public virtual AspNetUser aspNetUser { get; set; }
        public int ModuleId { get; set; }
        public virtual AspNetModule aspNetModule { get; set; }
    }
}
