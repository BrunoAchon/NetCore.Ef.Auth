using Microsoft.Extensions.DependencyInjection;
using WS.Manager.Mappings.Novo;
using WS.Manager.Mappings.Alterar;

namespace WS.WebApi.Configuration
{
    public static class AutoMappingConfig
    {
        public static void AddAutoMappingConfiguration(this IServiceCollection services) 
        {
            services.AddAutoMapper(
                        typeof(AspNetClientNovoMappingProfile),
                        typeof(AspNetClientAlterarMappingProfile),

                        typeof(AspNetModuleNovoMappingProfile),
                        typeof(AspNetModuleAlterarMappingProfile),

                        typeof(AspNetMenuNovoMappingProfile),
                        typeof(AspNetMenuAlterarMappingProfile),

                        typeof(AspNetUserMappingProfile)
                        );
        }
    }
}
