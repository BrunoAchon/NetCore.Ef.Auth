using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WS.Core.Domain;
using WS.Core.Shared.ModelViews.AspNetUser;
using WS.Manager.Interfaces.Managers;
using WS.Manager.Interfaces.Repositories;
using WS.Manager.Interfaces.Services;

namespace WS.Manager.Implementation
{
    public class AspNetUserManager : IAspNetUserManager
    {
        private readonly IAspNetUserRepository _aspNetUserRepository;
        private readonly UserManager<AspNetUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IJWTService _jwt;

        public AspNetUserManager(IAspNetUserRepository aspNetUserRepository, UserManager<AspNetUser> userMananger, IMapper mapper, IJWTService jwt)
        {
            _aspNetUserRepository = aspNetUserRepository;
            _userManager = userMananger;
            _mapper = mapper;
            _jwt = jwt;
        }

        public async Task<IEnumerable<AspNetUserView>> GetAspNetUserAsync()
        {
            return _mapper.Map<IEnumerable<AspNetUser>, IEnumerable<AspNetUserView>>(await _aspNetUserRepository.GetAspNetUserAsync());
        }

        public async Task<AspNetUserView> GetAspNetUserAsync(string login)
        {
            return _mapper.Map<AspNetUserView>(await _aspNetUserRepository.GetAspNetUserAsync(login));
        }

        public async Task<AspNetUserLogado> ValidaUsuarioEGeraTokenAsync(AspNetUserLogin aspNetUserLogin)
        {
            //var usuarioConsultado = await _aspNetUserRepository.FindByNameAsync(aspNetUserLogin.UserName, CancellationToken.None);
            var usuarioConsultado = await _userManager.FindByNameAsync(aspNetUserLogin.UserName);
            if (usuarioConsultado == null)
            {
                return null;
            }

            if (usuarioConsultado != null && !await _userManager.IsLockedOutAsync(usuarioConsultado))
            {
                if (await _userManager.CheckPasswordAsync(usuarioConsultado, aspNetUserLogin.Password))
                {
                    await _userManager.ResetAccessFailedCountAsync(usuarioConsultado);

                    var usuarioLogado = _mapper.Map<AspNetUserLogado>(usuarioConsultado);
                    usuarioLogado.Token = _jwt.CreateToken(usuarioConsultado);
                    usuarioLogado.UserId = usuarioConsultado.Id;
                    return usuarioLogado;
                }

                await _userManager.AccessFailedAsync(usuarioConsultado);
                if (await _userManager.IsLockedOutAsync(usuarioConsultado))
                {
                    //Email deve ser enviando com sugestão de Mudança de Senha!
                    //var usuarioLogado = await _aspNetUserMananger.MandarEmailRedefinirSenha(user);
                }
            }
            return null;
        }

        public async Task<AspNetUserLogado> RegistraUsuarioEGeraTokenAsync(AspNetUserRegister aspNetUserRegister)
        {
            var usuarioConsultado = await _aspNetUserRepository.GetAspNetUserAsync(aspNetUserRegister.UserName);
            if (usuarioConsultado == null)
            {
                usuarioConsultado = new AspNetUser()
                {
                    UserName = aspNetUserRegister.UserName,
                    Email = aspNetUserRegister.UserName
                };
                var result = await _userManager.CreateAsync(usuarioConsultado, aspNetUserRegister.Password);

                if (result.Succeeded)
                {
                    var appUser = await _userManager.Users.FirstOrDefaultAsync(u => u.NormalizedUserName == usuarioConsultado.UserName.ToUpper());
                    var usuarioCriado = _mapper.Map<AspNetUserLogado>(appUser);
                    usuarioCriado.Token = _jwt.CreateToken(usuarioConsultado);
                    usuarioCriado.UserId = usuarioConsultado.Id;

                    //var confirmationEmail = Url.Action("ConfirmEmailAddress", "Home",
                    //    new { token = token, email = user.Email }, Request.Scheme);

                    return usuarioCriado;
                }
            }
            return null;

        }
    }
}
