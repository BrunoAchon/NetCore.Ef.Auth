using AutoMapper;
using WS.Core.Domain;
using WS.Core.Shared.ModelViews.AspNetModule;

namespace WS.Mananger.Mappings.Novo
{
    public class AspNetModuleNovoMappingProfile : Profile
    {
        public AspNetModuleNovoMappingProfile()
        {
            CreateMap<AspNetModule, AspNetModuleView>();
            CreateMap<AspNetModuleNovo, AspNetModule>();
            

        }
    }
}
