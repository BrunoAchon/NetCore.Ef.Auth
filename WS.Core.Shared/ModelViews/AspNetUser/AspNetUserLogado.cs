using System;
using System.Collections.Generic;
using System.Text;

namespace WS.Core.Shared.ModelViews.AspNetUser
{
    public class AspNetUserLogado
    {
        /// <summary>
        /// Identificador do Usuário
        /// </summary>
        /// <example>1</example>>
        public int UserId { get; set; }
        /// <summary>
        /// login do usuário
        /// </summary>
        /// <example>bruno.achon</example>>
        public string UserName { get; set; }
        /// <summary>
        /// TokenDeValidacao
        /// </summary>
        /// <example></example>>
        public string Token { get; set; }

    }
}
