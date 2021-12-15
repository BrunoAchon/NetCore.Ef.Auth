using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WS.Core.Domain;
using WS.Data.Context;
using WS.Mananger.Interfaces.Services;
using WS.Mananger.Interfaces.Repositories;
using System.Security.Claims;

namespace WS.Data.Repository
{
    public class AspNetUserRepository : IUserStore<AspNetUser>, IUserPasswordStore<AspNetUser>
    {
        private readonly WsContext _context;
        public IApplicationReadDbConnection _readDbConnection { get; }
        public IApplicationWriteDbConnection _writeDbConnection { get; }

        public AspNetUserRepository(WsContext context, IApplicationReadDbConnection readDbConnection, IApplicationWriteDbConnection writeDbConnection)
        {
            _context = context;
            _readDbConnection = readDbConnection;
            _writeDbConnection = writeDbConnection;
        }

        public async Task<string> GetNormalizedUserNameAsync(AspNetUser user, CancellationToken cancellationToken)
        {
            return await Task.FromResult(user.NormalizedUserName);
        }
        public async Task<string> GetPasswordHashAsync(AspNetUser user, CancellationToken cancellationToken)
        {
            return await Task.FromResult(user.PasswordHash);
        }
        public async Task<string> GetUserIdAsync(AspNetUser user, CancellationToken cancellationToken)
        {
            return await Task.FromResult(user.Id.ToString());
        }
        public async Task<string> GetUserNameAsync(AspNetUser user, CancellationToken cancellationToken)
        {
            return await Task.FromResult(user.UserName);
        }

        public async Task<AspNetUser> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            return await _context.aspNetUsers
                            .AsNoTracking().SingleOrDefaultAsync(p => p.Id.ToString() == userId);
        }
        public async Task<AspNetUser> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            return await _context.aspNetUsers
                            .AsNoTracking().SingleOrDefaultAsync(p => p.NormalizedUserName == normalizedUserName);
        }

        public async Task<IdentityResult> CreateAsync(AspNetUser user, CancellationToken cancellationToken)
        {
            await _writeDbConnection.ExecuteAsync(
                "insert into AspNetUser([UserId]," +
                "[UserName]," +
                "[NormalizedUserName]," +
                "[PasswordHash]) " +
                "Values(@id,@userName,@normalizedUserName,@passwordHash)",
                new
                {
                    UserId = user.Id,
                    userName = user.UserName,
                    normalizedUserName = user.NormalizedUserName,
                    passwordHash = user.PasswordHash
                });
            return IdentityResult.Success;
        }
        public async Task<IdentityResult> UpdateAsync(AspNetUser user, CancellationToken cancellationToken)
        {
            await _writeDbConnection.ExecuteAsync(
                "update AspNetUser " +
                "set [UserName] = @userName," +
                "[NormalizedUserName] = @normalizedUserName," +
                "[PasswordHash] = @passwordHash " +
                "where [UserId] = @id",
                new
                {
                    UserId = user.Id,
                    userName = user.UserName,
                    normalizedUserName = user.NormalizedUserName,
                    passwordHash = user.PasswordHash
                });

            return IdentityResult.Success;
        }
        public async Task<IdentityResult> DeleteAsync(AspNetUser user, CancellationToken cancellationToken)
        {
            await _writeDbConnection.ExecuteAsync("" +
                "delete from AspNetUser where UserId = @id",
                new
                {
                    id = user.Id
                });

            return IdentityResult.Success;
        }

        public Task SetNormalizedUserNameAsync(AspNetUser user, string normalizedName, CancellationToken cancellationToken)
        {
            user.NormalizedUserName = normalizedName;
            return Task.CompletedTask;
        }
        public Task SetPasswordHashAsync(AspNetUser user, string passwordHash, CancellationToken cancellationToken)
        {
            user.PasswordHash = passwordHash;
            return Task.CompletedTask;
        }
        public Task SetUserNameAsync(AspNetUser user, string userName, CancellationToken cancellationToken)
        {
            user.UserName = userName;
            return Task.CompletedTask;
        }

        public Task<bool> HasPasswordAsync(AspNetUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PasswordHash != null);
        }

        public ClaimsPrincipal Store2FA(string userId, string provider)
        {
            var identity = new ClaimsIdentity(new List<Claim>
            {
                new Claim("sub", userId),
                new Claim("amr", provider)
            }, IdentityConstants.TwoFactorUserIdScheme);

            return new ClaimsPrincipal(identity);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
