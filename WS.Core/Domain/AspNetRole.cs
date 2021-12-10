using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace WS.Core.Domain
{
    public class AspNetRole : IdentityRole<int>
    {
        public virtual ICollection<AspNetUserRole> aspNetUserRole { get; set; }
    }
}
