using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using WS.Core.Domain;
using WS.Core.Shared.ModelViews.AspNetMenu;

namespace WS.Mananger.Mappings.Alterar
{
    public class AspNetMenuAlterarMappingProfile : Profile
    {
        public AspNetMenuAlterarMappingProfile()
        {
            CreateMap<AspNetMenuAlterar, AspNetMenu>();
        }
    }
}
