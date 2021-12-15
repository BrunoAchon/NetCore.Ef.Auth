using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WS.Core.Shared.ModelViews.AspNetUser
{
    public class AspNetUserLogin
    {
        /// <summary>
        /// Login do usuário
        /// </summary>
        /// <example>bruno.achon</example>>
        public string UserName { get; set; }

        /// <summary>
        /// Senha do usuário
        /// </summary>
        /// <example></example>>
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
