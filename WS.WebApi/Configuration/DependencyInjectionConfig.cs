using Microsoft.Extensions.DependencyInjection;
using WS.Data.Repository;
using WS.Manager.Implementation;
using WS.Manager.Interfaces.Managers;
using WS.Manager.Interfaces.Repositories;
using WS.Mananger.Implementation;
using WS.Mananger.Interfaces.Managers;
using WS.Mananger.Interfaces.Repositories;

namespace WS.WebApi.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void AddDependencyInjectionConfiguration(this IServiceCollection services)
        {
            services.AddScoped<IAspNetClientManager, AspNetClientManager>();
            services.AddScoped<IAspNetClientRepository, AspNetClientRepository>();

            services.AddScoped<IAspNetModuleManager, AspNetModuleManager>();
            services.AddScoped<IAspNetModuleRepository, AspNetModuleRepository>();
            services.AddScoped<IAspNetMenuRepository, AspNetMenuRepository>();

            services.AddScoped<IAspNetUserManager, AspNetUserManager>();
            services.AddScoped<IAspNetUserRepository, AspNetUserRepository>();
        }
    }
}
