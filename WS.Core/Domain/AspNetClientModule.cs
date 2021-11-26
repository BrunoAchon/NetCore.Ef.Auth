using System;
using System.Collections.Generic;

namespace WS.Core.Domain
{
    public class AspNetClientModule
    {
        public int ClientId { get; set; }
        public virtual AspNetClient Client { get; set; }
        public int ModuleId { get; set; }
        public virtual AspNetModule Module { get; set; }
        public DateTime? Vencimento { get; set; }
    }
}
