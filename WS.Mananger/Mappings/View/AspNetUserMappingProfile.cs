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
            CreateMap<AspNetUser, AspNetUserView>().ReverseMap()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.UserId));
            CreateMap<AspNetUser, AspNetUserView>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id));

            CreateMap<AspNetUser, AspNetUserLogado>().ReverseMap()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.UserId));
            CreateMap<AspNetUser, AspNetUserLogado>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id));

            CreateMap<AspNetUser, AspNetUserRegister>().ReverseMap();
        }
    }
}
