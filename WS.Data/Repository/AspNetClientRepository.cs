using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using WS.Core.Domain;
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
            var aspNetClients = await _context.aspNetClients
                .Include(cm => cm.aspNetClientModules)
                 .Include(cme => cme.aspNetClientMenus)
                .AsNoTracking().ToListAsync();

            return aspNetClients;
        }

        public async Task<AspNetClient> GetAspNetClientAsync(int id)
        {
            return await _context.aspNetClients
                .Include(cm => cm.aspNetClientModules)
                .Include(cme => cme.aspNetClientMenus)
                .AsNoTracking().SingleOrDefaultAsync(c => c.ClientId == id);
        }
        #endregion

        #region Insert
        public async Task<AspNetClient> InsertAspNetClient(AspNetClient aspNetClient)
        {
            await _context.aspNetClients.AddAsync(aspNetClient);
            await InsertAspNetClientModules(aspNetClient);
            await InsertAspNetClientMenus(aspNetClient);
            await _context.SaveChangesAsync();
            return aspNetClient;
        }

        private async Task InsertAspNetClientModules(AspNetClient aspNetClient)
        {
            //var modulesConsultados = new List<AspNetClientModule>();
            foreach (var module in aspNetClient.aspNetClientModules)
            {
                var moduleConsultado = await _context.aspNetModules.AsNoTracking().FirstAsync(m => m.ModuleId == module.ModuleId);
                _context.Entry(module).CurrentValues.SetValues(moduleConsultado);
            }
            //aspNetClient.aspNetClientModules = modulesConsultados;
        }

        private async Task InsertAspNetClientMenus(AspNetClient aspNetClient)
        {
            foreach (var menu in aspNetClient.aspNetClientMenus)
            {
                var menuConsultado = await _context.aspNetMenus.AsNoTracking().FirstAsync(me => me.MenuId == menu.MenuId);
                _context.Entry(menu).CurrentValues.SetValues(menuConsultado);
            }
        }
        #endregion

        #region Update
        public async Task<AspNetClient> UpdateAspNetClient(AspNetClient aspNetClient)
        {
            var aspNetClientConsultado = await _context.aspNetClients
                                               .Include(c => c.aspNetClientModules)
                                               .Include(cme => cme.aspNetClientMenus)
                                               .SingleOrDefaultAsync(c => c.ClientId == aspNetClient.ClientId);
            if (aspNetClientConsultado == null)
            {
                return null;
            }
            _context.Entry(aspNetClientConsultado).CurrentValues.SetValues(aspNetClient);

            await UpdateAspNetClientModule(aspNetClient, aspNetClientConsultado);
            await UpdateAspNetClientMenu(aspNetClient, aspNetClientConsultado);

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

        private async Task UpdateAspNetClientMenu(AspNetClient aspNetClient, AspNetClient aspNetClientConsultado)
        {
            aspNetClientConsultado.aspNetClientMenus.Clear();
            foreach (var menu in aspNetClient.aspNetClientMenus)
            {
                var menuConsultado = await _context.aspNetMenus.FindAsync(menu.MenuId);
                aspNetClientConsultado.aspNetClientMenus.Add(
                    new AspNetClientMenu
                    {
                        MenuId = menuConsultado.MenuId,
                        Inserir = menu.Inserir,
                        Exibir = menu.Exibir,
                        Editar = menu.Editar,
                        Excluir = menu.Excluir
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
