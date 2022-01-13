using System;
using System.Collections.Generic;
using System.Text;

namespace WS.Core.Shared.ModelViews.AspNetMenu
{
    public class AspNetMenuView :ICloneable
    {
        /// <summary>
        /// ID do Menu
        /// </summary>
        /// <example>1</example>>
        public int MenuId { get; set; }
        /// <summary>
        /// Titulo do Menu
        /// </summary>
        /// <example>Menu</example>>
        public string Menu { get; set; }

        /// <summary>
        /// Sequencial do modulo (em qual ordem ele será disposto para visualização)
        /// </summary>
        /// <example>1</example>>
        public int Ordem { get; set; }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
