using System;
using System.Collections.Generic;

namespace WS.Core.Domain
{
    public class AspNetClient
    {
        public int ClientId { get; set; }
        public int Orgao { get; set; }
        public string RazaoSocial { get; set; }
        public string Server { get; set; }
        public string Banco { get; set; }
        public DateTime? Vencimento { get; set; }    
        public virtual ICollection<AspNetClientModule> aspNetClientModules { get; set; }
        public virtual ICollection<AspNetClientMenu> aspNetClientMenus { get; set; }

        public AspNetClient()
        {
            aspNetClientModules = new HashSet<AspNetClientModule>();
            aspNetClientMenus = new HashSet<AspNetClientMenu>();
        }
    }
}
