using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using WS.Core.Shared.ModelViews.AspNetUser;
using Microsoft.AspNetCore.Authorization;
using WS.Core.Domain;
using WS.Manager.Interfaces.Managers;
using WS.Manager.Interfaces.Services;

namespace WS.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AspNetUserController : ControllerBase
    {
        private readonly IAspNetUserManager _aspNetUserMananger;
        private readonly IJWTService _jwt;

        /*
         * Para fazer testes no swagger é necessário ter o token de usuario logado.
         * Para inserir um token Use o metodo de Login (logue) para pegar o token
         * Após pegar o token clique no botão authorizer e insira "Barrer " + "SeuToken"
         */


        public AspNetUserController(IAspNetUserManager aspNetUserMananger,IJWTService jwt)
        {
            _aspNetUserMananger = aspNetUserMananger;
            _jwt = jwt;
        }

        /// <summary>
        /// Autenticação de Login de Usuário
        /// </summary>
        /// <param name="aspNetUserLogin"></param>
        //[HttpGet] // Manter como Get para o ambiente de produção
        [HttpPost] // usar somente para testes. pois o swagger nao deve exibir essas informações
        [Route("Login")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Login([FromBody] AspNetUserLogin aspNetUserLogin)
        {
            var usuarioLogado = await _aspNetUserMananger.ValidaUsuarioEGeraTokenAsync(aspNetUserLogin);
            if (usuarioLogado != null)
            {
                return Ok(usuarioLogado);
            }
            return Unauthorized();
        }

        /// <summary>
        /// Retorna dados do usuario
        /// </summary>
        [Authorize]
        [HttpGet]
        [ProducesResponseType(typeof(AspNetUserView), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get()
        {
            string login = User.Identity.Name;
            var usuario = await _aspNetUserMananger.GetAspNetUserAsync(login);
            if (usuario != null)
            {
                return Ok(usuario);
            }
            return NotFound();
        }

        /// <summary>
        /// Criar Usuário
        /// </summary>
        /// <param name="aspNetUserRegister"></param>
        [Authorize]
        [HttpPost]
        [ProducesResponseType(typeof(AspNetUserView), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post(AspNetUserRegister aspNetUserRegister)
        {
            var usuarioLogado = await _aspNetUserMananger.RegistraUsuarioEGeraTokenAsync(aspNetUserRegister);
            if (usuarioLogado != null)
            {
                return CreatedAtAction(nameof(Get), new { login = aspNetUserRegister.UserName }, aspNetUserRegister);
            }
            return Unauthorized();
        }
        /// <summary>
        /// Alterar um Usuário existente
        /// </summary>
        /// <param name="aspNetUser"></param>
        /// <returns></returns>
        //[Authorize]
        //[HttpPut]
        //[ProducesResponseType(typeof(AspNetUserView), StatusCodes.Status200OK)]
        //[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
        //[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        //[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        //public async Task<IActionResult> Put(AspNetUser aspNetUser)
        //{
        //    AspNetUserView aspNetUserAtualizado;
        //    aspNetUserAtualizado = await _aspNetUserMananger.UpdateAspNetUserAsync(aspNetUser);
        //    if (aspNetUserAtualizado == null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(aspNetUserAtualizado);
        //}
    }
}
