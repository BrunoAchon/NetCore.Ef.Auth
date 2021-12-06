using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using WS.Core.Domain;
using WS.Core.Shared.ModelViews.AspNetClient;
using WS.Mananger.Interfaces.Managers;
using WS.Mananger.Interfaces.Repositories;

namespace WS.Mananger.Implementation
{
    public class AspNetClientManager : IAspNetClientManager
    {
        private readonly IAspNetClientRepository _aspNetClientRepository;
        private readonly IMapper _mapper;

        public AspNetClientManager(IAspNetClientRepository aspNetClientRepository, IMapper mapper)
        {
            _aspNetClientRepository = aspNetClientRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AspNetClientView>> GetAspNetClientsAsync() 
        {
            return _mapper.Map<IEnumerable<AspNetClient>,IEnumerable<AspNetClientView>>(await _aspNetClientRepository.GetAspNetClientsAsync());
        }

        public async Task<AspNetClientView> GetAspNetClientAsync(int id)
        {
            return _mapper.Map<AspNetClientView>(await _aspNetClientRepository.GetAspNetClientAsync(id));
        }

        public async Task<AspNetClientView> DeleteAspNetClientAsync(int id)
        {
            await _aspNetClientRepository.DeleteAspNetClientAsync(id);
            var aspNetClient = await _aspNetClientRepository.DeleteAspNetClientAsync(id);
            return _mapper.Map<AspNetClientView>(aspNetClient);
        }

        public async Task<AspNetClientView> InsertAspNetClientAsync(AspNetClientNovo aspNetClientNovo)
        {
            var AspNetClient = _mapper.Map<AspNetClient>(aspNetClientNovo);
            AspNetClient = await _aspNetClientRepository.InsertAspNetClientAsync(AspNetClient);
            return _mapper.Map<AspNetClientView>(AspNetClient);
        }

        public async Task<AspNetClientView> UpdateAspNetClientAsync(AspNetClientAlterar aspNetClientAlterar)
        {
            var aspNetClient = _mapper.Map<AspNetClient>(aspNetClientAlterar);
            aspNetClient = await _aspNetClientRepository.UpdateAspNetClientAsync(aspNetClient);
            return _mapper.Map<AspNetClientView>(aspNetClient);
        }
    }
}
