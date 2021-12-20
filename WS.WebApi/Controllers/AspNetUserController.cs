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
        private readonly UserManager<AspNetUser> _userMananger;
        private readonly IAspNetUserManager _aspNetUserMananger;
        private readonly IJWTService _jwt;

        public AspNetUserController(
                        UserManager<AspNetUser> userMananger,
                        IAspNetUserManager aspNetUserMananger,
                        IJWTService jwt)
        {
            _aspNetUserMananger = aspNetUserMananger;
            _userMananger = userMananger;
            _jwt = jwt;
        }

        /// <summary>
        /// Autenticação de Login de Usuário
        /// </summary>
        /// <param name="Login"></param>
        [HttpPost]
        [Route("Login")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Login([FromBody] AspNetUserLogin Login)
        {
            try
            {
                var user = await _userMananger.FindByNameAsync(Login.UserName);
                if (user == null)
                {
                    return NotFound();
                }
                else
                {
                    if (user != null && !await _userMananger.IsLockedOutAsync(user))
                    {
                        if (await _userMananger.CheckPasswordAsync(user, Login.Password))
                        {
                            await _userMananger.ResetAccessFailedCountAsync(user);

                            var appUser = await _userMananger.Users
                                    .FirstOrDefaultAsync(u => u.NormalizedUserName == user.UserName.ToUpper());

                            var usuarioLogado = await _aspNetUserMananger.ValidaUsuarioEGeraTokenAsync(user);
                            if (usuarioLogado != null)
                            {
                                return Ok(usuarioLogado);
                            }
                        }
                        else
                        {
                            await _userMananger.AccessFailedAsync(user);
                            if (await _userMananger.IsLockedOutAsync(user))
                            {
                                //Email deve ser enviando com sugestão de Mudança de Senha!
                                //var usuarioLogado = await _aspNetUserMananger.MandarEmailRedefinirSenha(user);
                            }
                            UnauthorizedObjectResult unauthorizedObjectResult = new UnauthorizedObjectResult(new
                            {
                                status = "UNAUTHORIZED",
                                message = "Usuário temporáriamente bloqueado.",
                                situation = "O usuário foi bloqueado por excesso de tentativas mal sucedidas durante o login."
                            });
                            return Unauthorized(unauthorizedObjectResult);
                        }
                    }
                    return Unauthorized();
                }
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"ERROR {ex.Message}");
            }
        }
        /// <summary>
        /// Criação de Login de Usuário
        /// </summary>
        /// <param name="Register"></param>
        [HttpPost]
        [Route("Register")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Register(AspNetUserRegister Register)
        {
            try
            {
                var user = await _userMananger.FindByNameAsync(Register.UserName);

                if (user == null)
                {
                    user = new AspNetUser
                    {
                        UserName = Register.UserName,
                        Email = Register.UserName
                    };

                    var result = await _userMananger.CreateAsync(user, Register.Password);

                    if (result.Succeeded)
                    {
                        var appUser = await _userMananger.Users
                            .FirstOrDefaultAsync(u => u.NormalizedUserName == user.UserName.ToUpper());

                        var token = _jwt.CreateToken(appUser);
                        return Ok(token);
                    }
                }

                return Unauthorized();
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"ERROR {ex.Message}");
            }
        }
    }
}
