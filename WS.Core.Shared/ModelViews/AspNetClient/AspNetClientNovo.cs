using System;
using System.Collections.Generic;
using System.Linq;
using WS.Core.Shared.ModelViews.AspNetClientMenu;
using WS.Core.Shared.ModelViews.AspNetClientModule;

namespace WS.Core.Shared.ModelViews.AspNetClient
{
    /// <summary>
    /// Objeto utilizado para inserção de um novo cliente para utilizar as ferramentas
    /// </summary>
    public class AspNetClientNovo
    {
        /// <summary>
        /// Orgao autuador do cliente
        /// </summary>
        /// <example>0000</example>>
        public int Orgao { get; set; }
        /// <summary>
        /// Razao Social do cliente 
        /// </summary>
        /// <example>Razao Social do cliente</example>>
        public string RazaoSocial { get; set; }
        /// <summary>
        /// Servidor a qual a aplicação deve olhar para buscar os dados do cliente 
        /// Sever: IP ou SERVENAME
        /// </summary>
        /// <example>225.356.512.123</example>>
        public string Server { get; set; }
        /// <summary>
        /// Banco de dados a qual a aplicação deve olhar para buscar os dados do cliente 
        /// </summary>
        /// <example>DDSN0000</example>>
        public string Banco { get; set; }
        /// <summary>
        /// Vencimento do acesso do cliente - interrupção do uso da aplicação total
        /// </summary>
        /// <example>2021-01-01</example>>
        public DateTime Vencimento { get; set; }

        /// <summary>
        /// Lista de Modulos disponiveis do cliente
        /// </summary>
        public ICollection<AspNetClientModuleNovo> aspNetClientModules { get; set; }

        /// <summary>
        /// Lista de Permissoes dos Menus disponiveis do cliente
        /// </summary>
        public ICollection<AspNetClientMenuNovo> aspNetClientMenus { get; set; }

    }
}
