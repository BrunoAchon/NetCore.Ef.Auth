using AutoMapper;
using WS.Core.Domain;
using WS.Core.Shared.ModelViews.AspNetClientMenu;
using WS.Core.Shared.ModelViews.AspNetMenu;

namespace WS.Manager.Mappings.Novo
{
    public class AspNetMenuNovoMappingProfile : Profile
    {
        public AspNetMenuNovoMappingProfile()
        {
            CreateMap<AspNetMenu, AspNetMenuView>(); 
            CreateMap<AspNetMenuNovo, AspNetMenu>().ReverseMap(); 
        }
    }
}
