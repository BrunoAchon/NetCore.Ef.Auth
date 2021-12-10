using System;
using System.Collections.Generic;
using System.Text;

namespace WS.Core.Domain
{
    public class AspNetRoleMenu
    {
        public int RoleId { get; set; }
        public virtual AspNetRole aspNetRole { get; set; }
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
