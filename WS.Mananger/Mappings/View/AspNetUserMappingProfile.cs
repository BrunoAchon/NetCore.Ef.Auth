using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using WS.Core.Domain;
using WS.Core.Shared.ModelViews.AspNetUser;

namespace WS.Manager.Mappings.Novo
{
    public class AspNetUserMappingProfile : Profile
    {
        public AspNetUserMappingProfile()
        {
            CreateMap<AspNetUser, AspNetUserView>().ReverseMap();
            CreateMap<AspNetUser, AspNetUserRegister>().ReverseMap();
            CreateMap<AspNetUser, AspNetUserLogado>().ReverseMap();
        }
    }
}
