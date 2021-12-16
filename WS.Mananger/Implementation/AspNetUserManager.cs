using AutoMapper;
using Microsoft.AspNetCore.Identity;
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
        private readonly IMapper _mapper;
        private readonly IJWTService _jwt;

        public AspNetUserManager(IAspNetUserRepository aspNetUserRepository, IMapper mapper, IJWTService jwt)
        {
            _aspNetUserRepository = aspNetUserRepository;
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

        public async Task<AspNetUserView> InsertAspNetUserAsync(AspNetUserRegister aspNetUserRegister)
        {
            var usuario = _mapper.Map<AspNetUser>(aspNetUserRegister);
            ConverteSenhaEmHash(usuario);
            return _mapper.Map<AspNetUserView>(await _aspNetUserRepository.InsertAspNetUserAsync(usuario));

        }

        public async Task<AspNetUserView> UpdateAspNetUserAsync(AspNetUser aspNetUser)
        {
            ConverteSenhaEmHash(aspNetUser);
            return _mapper.Map<AspNetUserView>(await _aspNetUserRepository.UpdateAspNetUserAsync(aspNetUser));

        }

        public async Task<AspNetUserLogado> ValidaUsuarioEGeraTokenAsync(AspNetUser aspNetUser)
        {
            var usuarioConsultado = await _aspNetUserRepository.GetAspNetUserAsync(aspNetUser.UserName);
            if (usuarioConsultado == null)
            {
                return null;
            }
            if (await ValidaEAtualizaHashAsync(aspNetUser, usuarioConsultado.PasswordHash))
            {
                var usuarioLogado = _mapper.Map<AspNetUserLogado>(usuarioConsultado);
                usuarioLogado.Token = _jwt.CreateToken(usuarioConsultado);
                return usuarioLogado;
            }
            return null;
        }

        private async Task<bool> ValidaEAtualizaHashAsync(AspNetUser usuario, string hash)
        {
            var passwordHasher = new PasswordHasher<AspNetUser>();
            var status = passwordHasher.VerifyHashedPassword(usuario, hash, usuario.PasswordHash);
            switch (status)
            {
                case PasswordVerificationResult.Failed:
                    return false;

                case PasswordVerificationResult.Success:
                    return true;

                case PasswordVerificationResult.SuccessRehashNeeded:
                    await UpdateAspNetUserAsync(usuario);
                    return true;

                default:
                    throw new InvalidOperationException();
            }
        }
        
        private void ConverteSenhaEmHash(AspNetUser usuario)
        {
            var passwordHasher = new PasswordHasher<AspNetUser>();
            usuario.PasswordHash = passwordHasher.HashPassword(usuario, usuario.PasswordHash);
        }
    }
}
