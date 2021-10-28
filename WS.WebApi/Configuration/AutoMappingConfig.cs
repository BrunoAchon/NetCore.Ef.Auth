using Microsoft.Extensions.DependencyInjection;
using WS.Mananger.Mappings.Alterar;
using WS.Mananger.Mappings.Novo;

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
                        typeof(AspNetMenuAlterarMappingProfile)
                        );
        }
    }
}
