using Microsoft.Extensions.DependencyInjection;
using WS.Data.Repository;
using WS.Mananger.Implementation;
using WS.Mananger.Interfaces;

namespace WS.WebApi.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void AddDependencyInjectionConfiguration(this IServiceCollection services)
        {
            services.AddScoped<IAspNetClientRepository, AspNetClientRepository>();
            services.AddScoped<IAspNetClientMananger, AspNetClientMananger>();

            services.AddScoped<IAspNetModuleRepository, AspNetModuleRepository>();
            services.AddScoped<IAspNetModuleMananger, AspNetModuleMananger>();
        }
    }
}
