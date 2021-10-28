using System.Collections.Generic;
using WS.Core.Shared.ModelViews.AspNetMenu;

namespace WS.Core.Shared.ModelViews.AspNetModule
{
    public class AspNetModuleView
    {
        /// <summary>
        /// ID do Modulo
        /// </summary>
        /// <example>1</example>>
        public int ModuleId { get; set; }

        /// <summary>
        /// Titulo do Modulo
        /// </summary>
        /// <example>Cadastros</example>>
        public string Module { get; set; }

        /// <summary>
        /// Imagem do Modulo
        /// </summary>
        /// <example>bi bi-table</example>>
        public string ImgMenu { get; set; }

        /// <summary>
        /// Sequencial do Modulo (em qual ordem ele será disposto para visualização)
        /// </summary>
        /// <example>1</example>>
        public int Ordem { get; set; }

        /// <summary>
        /// Lista de menus de cada modulo
        /// </summary>
        public ICollection<AspNetMenuView> aspNetMenus { get; set; }
    }
}