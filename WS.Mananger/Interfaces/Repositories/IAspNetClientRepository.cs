using System.Collections.Generic;
using System.Threading.Tasks;
using WS.Core.Domain;

namespace WS.Mananger.Interfaces
{
    public interface IAspNetClientRepository
    {
        Task DeleteAspNetClient(int id);
        Task<AspNetClient> GetAspNetClientAsync(int id);
        Task<IEnumerable<AspNetClient>> GetAspNetClientsAsync();
        Task<AspNetClient> InsertAspNetClient(AspNetClient aspNetClient);
        Task<AspNetClient> UpdateAspNetClient(AspNetClient aspNetClient);
    }
}
