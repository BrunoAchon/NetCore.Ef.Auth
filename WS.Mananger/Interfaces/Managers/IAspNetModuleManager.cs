using System.Collections.Generic;
using System.Threading.Tasks;
using WS.Core.Domain;
using WS.Core.Shared.ModelViews.AspNetModule;

namespace WS.Mananger.Interfaces.Managers
{
    public interface IAspNetModuleManager
    {
        Task<IEnumerable<AspNetModuleView>> GetAspNetModulesAsync();
        Task<AspNetModuleView> GetAspNetModuleAsync(int id);
        Task<AspNetModuleView> InsertAspNetModuleAsync(AspNetModuleNovo aspNetClient);
        Task<AspNetModuleView> UpdateAspNetModuleAsync(AspNetModuleAlterar aspNetClient);
        Task<AspNetModuleView> DeleteAspNetModuleAsync(int id);
    }
}
