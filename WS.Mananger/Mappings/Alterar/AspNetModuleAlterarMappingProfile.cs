using AutoMapper;
using WS.Core.Domain;
using WS.Core.Shared.ModelViews.AspNetModule;

namespace WS.Manager.Mappings.Alterar
{
    public class AspNetModuleAlterarMappingProfile : Profile
    {
        public AspNetModuleAlterarMappingProfile()
        {
            CreateMap<AspNetModuleAlterar, AspNetModule>();
        }
    }
}
