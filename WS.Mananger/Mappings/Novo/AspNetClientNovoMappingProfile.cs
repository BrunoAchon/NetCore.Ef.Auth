using AutoMapper;
using WS.Core.Domain;
using WS.Core.Shared.ModelViews;
using WS.Core.Shared.ModelViews.AspNetClient;
using WS.Core.Shared.ModelViews.AspNetClientMenu;
using WS.Core.Shared.ModelViews.AspNetClientModule;
using WS.Core.Shared.ModelViews.AspNetMenu;
using WS.Core.Shared.ModelViews.AspNetModule;

namespace WS.Manager.Mappings.Novo
{
    public class AspNetClientNovoMappingProfile: Profile
    {
        public AspNetClientNovoMappingProfile()
        {
            CreateMap<AspNetClient, AspNetClientView>();
            CreateMap<AspNetClientNovo, AspNetClient>().ForMember(d => d.Vencimento, o => o.MapFrom(x => x.Vencimento.Date));

            CreateMap<AspNetClientModule, AspNetClientModuleView>();
            CreateMap<AspNetClientModuleView,AspNetClientModule>().ReverseMap();

            CreateMap<AspNetModule, AspNetModuleBase>().ReverseMap();

            CreateMap<AspNetClientMenu, AspNetClientMenuView>();
            CreateMap<AspNetClientMenuView, AspNetClientMenu>().ReverseMap();

            CreateMap<AspNetClientModuleNovo, AspNetClientModule>().ReverseMap();
            CreateMap<AspNetClientMenuNovo, AspNetClientMenu>().ReverseMap();
        }
    }
}
