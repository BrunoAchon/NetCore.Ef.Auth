using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WS.Core.Domain;
using WS.Data.Context;

namespace WS.WebApi.Configuration
{
    public static class IdentityCoreConfig
    {
        public static void AddIdentityCoreConfigration(this IServiceCollection services)
        {
            services.TryAddSingleton<ISystemClock, SystemClock>();
            services.AddIdentityCore<AspNetUser>(options =>
            {
                // options.SignIn.RequireConfirmedEmail = true;

                options.Password.RequireDigit = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 4;

                options.Lockout.MaxFailedAccessAttempts = 3;
                options.Lockout.AllowedForNewUsers = true;
            })
            .AddRoles<AspNetRole>()
            .AddEntityFrameworkStores<WsContext>()
            .AddRoleValidator<RoleValidator<AspNetRole>>()
            .AddRoleManager<RoleManager<AspNetRole>>()
            .AddSignInManager<SignInManager<AspNetUser>>();
            //.AddDefaultTokenProviders();
        }
    }
}
