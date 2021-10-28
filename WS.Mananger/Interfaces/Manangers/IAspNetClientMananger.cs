using System.Collections.Generic;
using System.Threading.Tasks;
using WS.Core.Shared.ModelViews;
using WS.Core.Shared.ModelViews.AspNetClient;

namespace WS.Mananger.Interfaces
{
    public interface IAspNetClientMananger
    {
        Task DeleteAspNetClient(int id);
        Task<AspNetClientView> GetAspNetClientAsync(int id);
        Task<IEnumerable<AspNetClientView>> GetAspNetClientsAsync();
        Task<AspNetClientView> InsertAspNetClient(AspNetClientNovo aspNetClient);
        Task<AspNetClientView> UpdateAspNetClient(AspNetClientAlterar aspNetClient);
    }
}
