using System.Collections.Generic;
using System.Threading.Tasks;
using WS.Core.Domain;

namespace WS.Mananger.Interfaces.Repositories
{
    public interface IAspNetModuleRepository
    {
        Task<IEnumerable<AspNetModule>> GetAspNetModulesAsync();
        Task<AspNetModule> GetAspNetModuleAsync(int id);
        Task<AspNetModule> InsertAspNetModuleAsync(AspNetModule aspNetModule);
        Task<AspNetModule> UpdateAspNetModuleAsync(AspNetModule aspNetModule);
        Task<AspNetModule> DeleteAspNetModuleAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}
