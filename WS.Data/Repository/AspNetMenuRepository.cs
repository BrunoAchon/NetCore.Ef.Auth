using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WS.Data.Context;
using WS.Mananger.Interfaces.Repositories;

namespace WS.Data.Repository
{
    public class AspNetMenuRepository: IAspNetMenuRepository
    {
        private readonly WsContext _context;

        public AspNetMenuRepository(WsContext context)
        {
            _context = context;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.aspNetMenus.AsNoTracking().AnyAsync(p => p.MenuId == id);
        }
    }
}
