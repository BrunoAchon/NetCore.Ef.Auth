using AutoMapper;
using WS.Core.Domain;
using WS.Core.Shared.ModelViews.AspNetClientModule;
using WS.Core.Shared.ModelViews.AspNetModule;

namespace WS.Manager.Mappings.Novo
{
    public class AspNetModuleNovoMappingProfile : Profile
    {
        public AspNetModuleNovoMappingProfile()
        {
            CreateMap<AspNetModule, AspNetModuleView>();
            CreateMap<AspNetModuleNovo, AspNetModule>().ReverseMap();

            CreateMap<AspNetModule, AspNetClientModuleView>();
        }
    }
}
