using System;
using System.Collections.Generic;
using System.Text;

namespace WS.Core.Shared.ModelViews.AspNetModule
{
    public class AspNetModuleBase
    {
        /// <summary>
        /// ID do Modulo
        /// </summary>
        /// <example>1</example>>
        public int ModuleId { get; set; }

        /// <summary>
        /// Titulo do Modulo
        /// </summary>
        /// <example>Modulo</example>>
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
    }
}
