using System;
using System.Collections.Generic;
using System.Text;

namespace WS.Core.Domain
{
    public class AspNetUserMenu
    {
        public int UserId { get; set; }
        public virtual AspNetUser aspNetUser { get; set; }
        public int ModuleId { get; set; }
        public virtual AspNetModule aspNetModule { get; set; }
        public int MenuId { get; set; }
        public virtual AspNetMenu aspNetMenu { get; set; }
        public bool Exibir { get; set; }
        public bool Inserir { get; set; }
        public bool Editar { get; set; }
        public bool Excluir { get; set; }
    }
}
