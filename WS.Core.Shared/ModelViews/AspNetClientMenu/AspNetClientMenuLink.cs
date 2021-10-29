using System;
using System.Collections.Generic;
using System.Text;

namespace WS.Core.Shared.ModelViews.AspNetClientMenu
{
    public class AspNetClientMenuLink
    {
        /// <summary>
        /// ID do Menu
        /// </summary>
        /// <example>1</example>>
        public int MenuId { get; set; }
        
        /// <summary>
        /// Permissão de Visualização
        /// </summary>
        /// <example>true</example>>
        public bool Exibir { get; set; }

        /// <summary>
        /// Permissão de Inclusão
        /// </summary>
        /// <example>true</example>>
        public bool Inserir { get; set; }

        /// <summary>
        /// Permissão de Alteração
        /// </summary>
        /// <example>true</example>>
        public bool Editar { get; set; }

        /// <summary>
        /// Permissão de Deleção
        /// </summary>
        /// <example>false</example>>
        public bool Excluir { get; set; }
    }
}
