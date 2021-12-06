using System.Collections.Generic;
using System.Threading.Tasks;
using WS.Core.Domain;

namespace WS.Mananger.Interfaces.Repositories
{
    public interface IAspNetClientRepository
    {
        Task<IEnumerable<AspNetClient>> GetAspNetClientsAsync();
        Task<AspNetClient> GetAspNetClientAsync(int id);
        Task<AspNetClient> InsertAspNetClientAsync(AspNetClient aspNetClient);
        Task<AspNetClient> UpdateAspNetClientAsync(AspNetClient aspNetClient);
        Task<AspNetClient> DeleteAspNetClientAsync(int id);
    }
}
