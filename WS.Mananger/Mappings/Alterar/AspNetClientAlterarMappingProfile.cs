using AutoMapper;
using WS.Core.Domain;
using WS.Core.Shared.ModelViews.AspNetClient;

namespace WS.Manager.Mappings.Alterar
{
    public class AspNetClientAlterarMappingProfile : Profile
    {
        public AspNetClientAlterarMappingProfile()
        {
            CreateMap<AspNetClientAlterar, AspNetClient>().ReverseMap();
            CreateMap<AspNetClientAlterar, AspNetClient>()
                .ForMember(d => d.Vencimento, o => o.MapFrom(x => x.Vencimento.Date));


        }
    }
}
