using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using WS.Core.Domain;
using WS.Core.Shared.ModelViews.AspNetClientModule;
using WS.Data.Context;
using WS.Mananger.Interfaces;

namespace WS.Data.Repository
{
    public class AspNetClientRepository : IAspNetClientRepository
    {
        private readonly WsContext _context;
        public AspNetClientRepository(WsContext context)
        {
            _context = context;
        }

        #region Get 
        public async Task<IEnumerable<AspNetClient>> GetAspNetClientsAsync()
        {
            return await _context.aspNetClients
                .Include(cm => cm.aspNetClientModules)
                .ThenInclude(m => m.Module)
                .AsNoTracking().ToListAsync();
        }

        public async Task<AspNetClient> GetAspNetClientAsync(int id)
        {
            return await _context.aspNetClients
                .Include(cm => cm.aspNetClientModules)
                .ThenInclude(m => m.Module)
                .AsNoTracking().SingleOrDefaultAsync(c => c.ClientId == id);
        }
        #endregion

        #region Insert
        public async Task<AspNetClient> InsertAspNetClient(AspNetClient aspNetClient)
        {
            await _context.aspNetClients.AddAsync(aspNetClient);
            await InsertAspNetClientModules(aspNetClient);
            await _context.SaveChangesAsync();
            return aspNetClient;
        }

        private async Task InsertAspNetClientModules(AspNetClient aspNetClient)
        {
            foreach (var module in aspNetClient.aspNetClientModules)
            {
                var moduleConsultado = await _context.aspNetModules.AsNoTracking().FirstAsync(m => m.ModuleId == module.ModuleId);
                _context.Entry(module).CurrentValues.SetValues(moduleConsultado);
            }
        }
        #endregion

        #region Update
        public async Task<AspNetClient> UpdateAspNetClient(AspNetClient aspNetClient)
        {
            var aspNetClientConsultado = await _context.aspNetClients
                                               .Include(c => c.aspNetClientModules)
                                               .ThenInclude(m => m.Module)
                                               .SingleOrDefaultAsync(c => c.ClientId == aspNetClient.ClientId);
            if (aspNetClientConsultado == null)
            {
                return null;
            }
            _context.Entry(aspNetClientConsultado).CurrentValues.SetValues(aspNetClient);

            await UpdateAspNetClientModule(aspNetClient, aspNetClientConsultado);
            await _context.SaveChangesAsync();
            return aspNetClientConsultado;
        }

        private async Task UpdateAspNetClientModule(AspNetClient aspNetClient, AspNetClient aspNetClientConsultado)
        {
            aspNetClientConsultado.aspNetClientModules.Clear();
            foreach (var module in aspNetClient.aspNetClientModules)
            {
                var moduleConsultado = await _context.aspNetModules.FindAsync(module.ModuleId);
                aspNetClientConsultado.aspNetClientModules.Add(
                    new AspNetClientModule
                    {
                        ModuleId = moduleConsultado.ModuleId,
                        Vencimento = module.Vencimento
                    });
            }
        }
        #endregion

        #region Delete
        public async Task DeleteAspNetClient(int id)
        {
            var aspNetClientConsultado = await _context.aspNetClients.FindAsync(id);
            _context.aspNetClients.Remove(aspNetClientConsultado);
            await _context.SaveChangesAsync();
        }
        #endregion
    }
}
