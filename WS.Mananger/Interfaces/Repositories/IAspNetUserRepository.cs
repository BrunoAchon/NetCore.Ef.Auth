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
        Task<string> GetNormalizedUserNameAsync(AspNetUser user, CancellationToken cancellationToken);
        Task<string> GetPasswordHashAsync(AspNetUser user, CancellationToken cancellationToken);
        Task<string> GetUserIdAsync(AspNetUser user, CancellationToken cancellationToken);
        Task<string> GetUserNameAsync(AspNetUser user, CancellationToken cancellationToken);

        Task<AspNetUser> FindByIdAsync(string userId, CancellationToken cancellationToken);
        Task<AspNetUser> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken);

        Task<IdentityResult> CreateAsync(AspNetUser user, CancellationToken cancellationToken);
        Task<IdentityResult> UpdateAsync(AspNetUser user, CancellationToken cancellationToken);
        Task<IdentityResult> DeleteAsync(AspNetUser user, CancellationToken cancellationToken);

        Task SetNormalizedUserNameAsync(AspNetUser user, string normalizedName, CancellationToken cancellationToken);
        Task SetPasswordHashAsync(AspNetUser user, string passwordHash, CancellationToken cancellationToken);
        Task SetUserNameAsync(AspNetUser user, string userName, CancellationToken cancellationToken);

        Task<bool> HasPasswordAsync(AspNetUser user, CancellationToken cancellationToken);

        Task<IEnumerable<AspNetUser>> GetAspNetUserAsync();
        Task<AspNetUser> GetAspNetUserAsync(string login);
        Task<AspNetUser> InsertAspNetUserAsync(AspNetUser usuario);
        Task<AspNetUser> UpdateAspNetUserAsync(AspNetUser usuario);
    }
}
