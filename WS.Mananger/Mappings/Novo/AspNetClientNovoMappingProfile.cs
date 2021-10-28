﻿using AutoMapper;
using WS.Core.Domain;
using WS.Core.Shared.ModelViews;
using WS.Core.Shared.ModelViews.AspNetClient;
using WS.Core.Shared.ModelViews.AspNetClientModule;

namespace WS.Mananger.Mappings.Novo
{
    public class AspNetClientNovoMappingProfile: Profile
    {
        public AspNetClientNovoMappingProfile()
        {
            CreateMap<AspNetClient, AspNetClientView>();
            CreateMap<AspNetClientNovo, AspNetClient>().ForMember(d => d.Vencimento, o => o.MapFrom(x => x.Vencimento.Date));

            CreateMap<AspNetClientModule, AspNetClientModuleLink>().ReverseMap();
        }
    }
}