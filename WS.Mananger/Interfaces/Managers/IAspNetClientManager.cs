using System.Collections.Generic;
using System.Threading.Tasks;
using WS.Core.Shared.ModelViews;
using WS.Core.Shared.ModelViews.AspNetClient;

namespace WS.Mananger.Interfaces.Managers
{
    public interface IAspNetClientManager
    {
        Task<IEnumerable<AspNetClientView>> GetAspNetClientsAsync();
        Task<AspNetClientView> GetAspNetClientAsync(int id);
        Task<AspNetClientView> InsertAspNetClientAsync(AspNetClientNovo aspNetClient);
        Task<AspNetClientView> UpdateAspNetClientAsync(AspNetClientAlterar aspNetClient);
        Task<AspNetClientView> DeleteAspNetClientAsync(int id);
    }
}
