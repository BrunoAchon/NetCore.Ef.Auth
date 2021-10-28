using AutoMapper;
using WS.Core.Domain;
using WS.Core.Shared.ModelViews.AspNetModule;

namespace WS.Mananger.Mappings.Alterar
{
    public class AspNetModuleAlterarMappingProfile : Profile
    {
        public AspNetModuleAlterarMappingProfile()
        {
            CreateMap<AspNetModuleAlterar, AspNetModule>();
        }
    }
}
