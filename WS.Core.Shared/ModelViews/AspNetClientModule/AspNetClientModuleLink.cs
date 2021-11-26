using System;
using System.Collections.Generic;
using WS.Core.Shared.ModelViews.AspNetClient;
using WS.Core.Shared.ModelViews.AspNetModule;

namespace WS.Core.Shared.ModelViews.AspNetClientModule
{
    public class AspNetClientModuleLink
    {
        /// <summary>
        /// ID do Modulo
        /// </summary>
        /// <example>1</example>>
        public int ModuleId { get; set; }
        /// <summary>
        /// Vencimento do acesso do ao modulo do cliente
        /// </summary>
        /// <example>2021-01-01</example>>
        public DateTime? Vencimento { get; set; }
    }
}
