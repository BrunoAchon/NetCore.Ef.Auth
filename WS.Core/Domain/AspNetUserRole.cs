using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace WS.Core.Domain
{
    public class AspNetUserRole : IdentityUserRole<int>
    {
        public AspNetUser aspNetUser { get; set; }
        public AspNetRole aspNetRole { get; set; }
    }
}
