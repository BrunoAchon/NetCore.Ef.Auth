using System.Collections.Generic;
using System.Threading.Tasks;
using WS.Core.Domain;
using WS.Core.Shared.ModelViews.AspNetModule;

namespace WS.Mananger.Interfaces
{
    public interface IAspNetModuleMananger
    {
        Task DeleteAspNetModule(int id);
        Task<AspNetModuleView> GetAspNetModuleAsync(int id);
        Task<IEnumerable<AspNetModuleView>> GetAspNetModulesAsync();
        Task<AspNetModuleView> InsertAspNetModule(AspNetModuleNovo aspNetClient);
        Task<AspNetModuleView> UpdateAspNetModule(AspNetModuleAlterar aspNetClient);
    }
}
