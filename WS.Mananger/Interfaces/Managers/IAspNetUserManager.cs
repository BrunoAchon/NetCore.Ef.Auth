using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WS.Core.Domain;
using WS.Core.Shared.ModelViews.AspNetUser;

namespace WS.Manager.Interfaces.Managers
{
    public interface IAspNetUserManager
    {

        Task<IEnumerable<AspNetUserView>> GetAspNetUserAsync();
        Task<AspNetUserView> GetAspNetUserAsync(string login);

        Task<AspNetUserLogado> ValidaUsuarioEGeraTokenAsync(AspNetUserLogin usuario);
        Task<AspNetUserLogado> RegistraUsuarioEGeraTokenAsync(AspNetUserRegister usuario);
    }
}
