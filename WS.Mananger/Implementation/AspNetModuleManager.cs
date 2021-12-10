using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using WS.Core.Domain;
using WS.Core.Shared.ModelViews.AspNetModule;
using WS.Mananger.Interfaces.Managers;
using WS.Mananger.Interfaces.Repositories;

namespace WS.Mananger.Implementation
{
    public class AspNetModuleManager : IAspNetModuleManager
    {
        private readonly IAspNetModuleRepository _aspNetModuleRepository;
        private readonly IMapper _mapper;

        public AspNetModuleManager(IAspNetModuleRepository aspNetModuleRepository, IMapper mapper)
        {
            _aspNetModuleRepository = aspNetModuleRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AspNetModuleView>> GetAspNetModulesAsync()
        {
            return _mapper.Map<IEnumerable<AspNetModule>, IEnumerable<AspNetModuleView>>(await _aspNetModuleRepository.GetAspNetModulesAsync());
        }

        public async Task<AspNetModuleView> GetAspNetModuleAsync(int id)
        {
            return _mapper.Map<AspNetModuleView>(await _aspNetModuleRepository.GetAspNetModuleAsync(id));

        }

        public async Task<AspNetModuleView> DeleteAspNetModuleAsync(int id)
        {
            var aspNetModule = await _aspNetModuleRepository.DeleteAspNetModuleAsync(id);
            return _mapper.Map<AspNetModuleView>(aspNetModule);
        }

        public async Task<AspNetModuleView> InsertAspNetModuleAsync(AspNetModuleNovo aspNetModuleNovo)
        {
            var aspNetModule = _mapper.Map<AspNetModule>(aspNetModuleNovo);
            return _mapper.Map<AspNetModuleView>(await _aspNetModuleRepository.InsertAspNetModuleAsync(aspNetModule));
        }

        public async Task<AspNetModuleView> UpdateAspNetModuleAsync(AspNetModuleAlterar aspNetModuleAlterar)
        {
            var aspNetModule = _mapper.Map<AspNetModule>(aspNetModuleAlterar);
            return _mapper.Map<AspNetModuleView>(await _aspNetModuleRepository.UpdateAspNetModuleAsync(aspNetModule));
        }
    }
}
