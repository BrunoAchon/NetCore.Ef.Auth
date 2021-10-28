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
    }
}
