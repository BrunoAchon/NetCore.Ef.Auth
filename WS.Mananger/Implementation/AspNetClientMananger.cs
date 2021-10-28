using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using WS.Core.Domain;
using WS.Core.Shared.ModelViews;
using WS.Core.Shared.ModelViews.AspNetClient;
using WS.Mananger.Interfaces;

namespace WS.Mananger.Implementation
{
    public class AspNetClientMananger : IAspNetClientMananger
    {
        private readonly IAspNetClientRepository _aspNetClientRepository;
        private readonly IMapper _mapper;

        public AspNetClientMananger(IAspNetClientRepository aspNetClientRepository, IMapper mapper)
        {
            _aspNetClientRepository = aspNetClientRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AspNetClientView>> GetAspNetClientsAsync() 
        {
            return _mapper.Map<IEnumerable<AspNetClient>,IEnumerable<AspNetClientView>>(await _aspNetClientRepository.GetAspNetClientsAsync());
            //return await _aspNetClientRepository.GetAspNetClientsAsync();
        }

        public async Task<AspNetClientView> GetAspNetClientAsync(int id)
        {
            return _mapper.Map<AspNetClientView>(await _aspNetClientRepository.GetAspNetClientAsync(id));
            //return await _aspNetClientRepository.GetAspNetClientAsync(id);
        }

        public async Task DeleteAspNetClient(int id)
        {
            await _aspNetClientRepository.DeleteAspNetClient(id);
        }

        public async Task<AspNetClientView> InsertAspNetClient(AspNetClientNovo aspNetClientNovo)
        {
            var Client = _mapper.Map<AspNetClient>(aspNetClientNovo);
            return _mapper.Map<AspNetClientView>(await _aspNetClientRepository.InsertAspNetClient(Client));
            //return await _aspNetClientRepository.InsertAspNetClient(aspNetClient);
        }

        public async Task<AspNetClientView> UpdateAspNetClient(AspNetClientAlterar aspNetClientAlterar)
        {
            var aspNetClient = _mapper.Map<AspNetClient>(aspNetClientAlterar);
            return _mapper.Map<AspNetClientView>(await _aspNetClientRepository.UpdateAspNetClient(aspNetClient));
        }
    }
}
