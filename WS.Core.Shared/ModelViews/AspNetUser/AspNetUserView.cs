using System;
using System.Collections.Generic;
using System.Text;

namespace WS.Core.Shared.ModelViews.AspNetUser
{
    public class AspNetUserView : ICloneable
    {
        /// <summary>
        /// Identificador do Usuário
        /// </summary>
        /// <example>7075</example>>
        public int UserId { get; set; }
        /// <summary>
        /// login do usuário
        /// </summary>
        /// <example>bruno.achon</example>>
        public string UserName { get; set; }
        /// <summary>
        /// login do usuário Normalizado
        /// </summary>
        /// <example>BRUNO.ACHON</example>>
        public string NormalizedUserName { get; set; }
        /// <summary>
        /// Email do usuário
        /// </summary>
        /// <example>brunoachon@cldsbc.com.br</example>>
        public string Email { get; set; }
        /// <summary>
        /// Email do usuário Normalizado
        /// </summary>
        /// <example>BRUNOACHON@CLDSBC.COM.BR</example>>
        public string NormalizedEmail { get; set; }
        /// <summary>
        /// Usuário confirmou o email
        /// </summary>
        /// <example>True</example>>
        public bool EmailConfirmed { get; set; }
        /// <summary>
        /// Telefone do usuário
        /// </summary>
        /// <example>(11)91111-2222</example>>
        public bool PhoneNumber { get; set; }
        /// <summary>
        /// Usuário confirmou o telefone
        /// </summary>
        /// <example>True</example>>
        public bool PhoneNumberConfirmed { get; set; }
        /// <summary>
        /// Usuário habilitou o login por 2 fatores
        /// </summary>
        /// <example>False</example>>
        public bool TwoFactorEnabled { get; set; }
        /// <summary>
        /// Tempo restante de bloqueio da conta
        /// </summary>
        /// <example>Null</example>>
        public int AccessFailedCount { get; set; }
        /// <summary>
        /// Quantidade de Acessos fracassados
        /// </summary>
        /// <example>False</example>>
        public bool LockoutEnabled { get; set; }
        /// <summary>
        /// Tempo restante de bloqueio da conta
        /// </summary>
        /// <example>Null</example>>
        public DateTimeOffset LockoutEnd { get; set; }
        /// <summary>
        /// Ainda não sei oque é
        /// </summary>
        /// <example>Pesquisar oque é</example>>
        public bool SecurityStamp { get; set; }
        /// <summary>
        /// Ainda não sei oque é
        /// </summary>
        /// <example>Pesquisar oque é</example>>
        public bool ConcurrencyStamp { get; set; }

        public object Clone()
        {
            throw new NotImplementedException();
        }
    }
}
