using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace WS.Core.Domain
{
    public class AspNetUserRole : IdentityUserRole<int>
    {
        public override int UserId { get; set; }
        public AspNetUser aspNetUser { get; set; }
        public override int RoleId { get; set; }
        public AspNetRole aspNetRole { get; set; }
    }
}
