using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WS.Core.Domain;

namespace WS.Manager.Interfaces.Repositories
{
    public interface IAspNetUserRepository
    {
        Task<IEnumerable<AspNetUser>> GetAspNetUserAsync();

        Task<AspNetUser> GetAspNetUserAsync(string login);

        Task<AspNetUser> InsertAspNetUserAsync(AspNetUser usuario);

        Task<AspNetUser> UpdateAspNetUserAsync(AspNetUser usuario);
    }
}
