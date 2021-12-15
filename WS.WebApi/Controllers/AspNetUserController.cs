using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WS.Core.Shared.ModelViews.AspNetUser;
using AutoMapper;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;
using WS.Core.Domain;

namespace WS.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AspNetUserController : ControllerBase
    {
        private readonly UserManager<AspNetUser> _aspNetUserMananger;
        private readonly SignInManager<AspNetUser> _signInManager;
        private readonly ILogger<AspNetUserController> _logger;
        private readonly IUserClaimsPrincipalFactory<AspNetUser> _userClaimsPrincipalFactory;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;

        public AspNetUserController(UserManager<AspNetUser> aspNetUserMananger,
                        SignInManager<AspNetUser> signInManager,
                        ILogger<AspNetUserController> logger,
                        IUserClaimsPrincipalFactory<AspNetUser> userClaimsPrincipalFactory,
                        IMapper mapper,
                        IConfiguration config)
        {
            _userClaimsPrincipalFactory = userClaimsPrincipalFactory;
            _aspNetUserMananger = aspNetUserMananger;
            _signInManager = signInManager;
            _logger = logger;
            _mapper = mapper;
            _config = config;
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
                var user = await _aspNetUserMananger.FindByNameAsync(Login.UserName);
                if (user == null)
                {
                    return NotFound();
                }

                var result = await _signInManager.CheckPasswordSignInAsync(user, Login.Password, false);

                if (result.Succeeded)
                {
                    var appUser = await _aspNetUserMananger.Users
                            .FirstOrDefaultAsync(u => u.NormalizedUserName == user.UserName.ToUpper());

                    var userToReturn = _mapper.Map<AspNetUser>(appUser);

                    return Ok(new
                    {
                        token = GenerateJWToken(appUser).Result,
                        user = userToReturn
                    });
                }

                return Unauthorized();
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
                var user = await _aspNetUserMananger.FindByNameAsync(Register.UserName);

                if (user == null)
                {
                    user = new AspNetUser
                    {
                        UserName = Register.UserName,
                        Email = Register.UserName
                    };

                    var result = await _aspNetUserMananger.CreateAsync(user, Register.Password);

                    if (result.Succeeded)
                    {
                        var appUser = await _aspNetUserMananger.Users
                            .FirstOrDefaultAsync(u => u.NormalizedUserName == user.UserName.ToUpper());

                        var token = GenerateJWToken(appUser).Result;
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

        private async Task<string> GenerateJWToken(AspNetUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName)
            };

            var roles = await _aspNetUserMananger.GetRolesAsync(user);

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(
                _config.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescription);

            return tokenHandler.WriteToken(token);
        }
    }
}
