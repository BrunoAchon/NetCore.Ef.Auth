using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WS.Mananger.Interfaces.Repositories
{
    public interface IAspNetMenuRepository
    {
        Task<bool> ExistsAsync(int id);
    }
}
