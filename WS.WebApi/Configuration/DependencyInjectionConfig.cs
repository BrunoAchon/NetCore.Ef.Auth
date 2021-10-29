using Microsoft.Extensions.DependencyInjection;
using WS.Data.Repository;
using WS.Mananger.Implementation;
using WS.Mananger.Interfaces;
using WS.Mananger.Interfaces.Repositories;

namespace WS.WebApi.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void AddDependencyInjectionConfiguration(this IServiceCollection services)
        {
            services.AddScoped<IAspNetClientMananger, AspNetClientMananger>();
            services.AddScoped<IAspNetModuleMananger, AspNetModuleMananger>();

            services.AddScoped<IAspNetClientRepository, AspNetClientRepository>();
            services.AddScoped<IAspNetModuleRepository, AspNetModuleRepository>();
            
            services.AddScoped<IAspNetMenuRepository, AspNetMenuRepository>();
        }
    }
}
