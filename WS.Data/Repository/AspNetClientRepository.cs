using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using WS.Core.Domain;
using WS.Data.Context;
using WS.Mananger.Interfaces;
using WS.Mananger.Interfaces.Services;

namespace WS.Data.Repository
{
    public class AspNetClientRepository : IAspNetClientRepository
    {
        private readonly WsContext _context;
        //public IApplicationReadDbConnection _readDbConnection { get; }
        //public IApplicationWriteDbConnection _writeDbConnection { get; }

        public AspNetClientRepository(WsContext context, IApplicationReadDbConnection readDbConnection, IApplicationWriteDbConnection writeDbConnection)
        {
            _context = context;
            //_readDbConnection = readDbConnection;
            //_writeDbConnection = writeDbConnection;
        }

        #region Get 
        public async Task<IEnumerable<AspNetClient>> GetAspNetClientsAsync()
        {
            return await _context.aspNetClients
                .Include(cm => cm.aspNetClientModules)
                  .ThenInclude(md => md.aspNetModule)
                .Include(cme => cme.aspNetClientMenus)
                  .ThenInclude(md => md.aspNetMenu)
                .AsNoTracking().ToListAsync();
        }

        public async Task<AspNetClient> GetAspNetClientAsync(int id)
        {
            return await _context.aspNetClients
                .Include(cm => cm.aspNetClientModules)
                  .ThenInclude(md => md.aspNetModule)
                .Include(cme => cme.aspNetClientMenus)
                  .ThenInclude(md => md.aspNetMenu)
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


            return await _context.aspNetClients
            .Include(cm => cm.aspNetClientModules)
              .ThenInclude(md => md.aspNetModule)
            .Include(cme => cme.aspNetClientMenus)
              .ThenInclude(md => md.aspNetMenu)
            .AsNoTracking().SingleOrDefaultAsync(c => c.ClientId == aspNetClient.ClientId);
            //return aspNetClient;
        }

        private async Task InsertAspNetClientModules(AspNetClient aspNetClient)
        {
            foreach (var module in aspNetClient.aspNetClientModules)
            {
                var moduleConsultado = await _context.aspNetModules.AsNoTracking().FirstAsync(m => m.ModuleId == module.ModuleId);
                _context.Entry(module).CurrentValues.SetValues(moduleConsultado);
            }
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
                                                .Include(cm => cm.aspNetClientModules)
                                                  .ThenInclude(md => md.aspNetModule)
                                                .Include(cme => cme.aspNetClientMenus)
                                                  .ThenInclude(md => md.aspNetMenu)
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
                if (moduleConsultado != null)
                {
                    aspNetClientConsultado.aspNetClientModules.Add(
                        new AspNetClientModule
                        {
                            ModuleId = moduleConsultado.ModuleId,
                            Vencimento = module.Vencimento
                        });
                }
                await UpdateAspNetClientMenu(aspNetClient, aspNetClientConsultado, moduleConsultado);
            }
        }

        private async Task UpdateAspNetClientMenu(AspNetClient aspNetClient, AspNetClient aspNetClientConsultado, AspNetModule moduleConsultado)
        {
            aspNetClientConsultado.aspNetClientMenus.Clear();
            foreach (var menu in aspNetClient.aspNetClientMenus)
            {
                var menuConsultado = await _context.aspNetMenus.FindAsync(menu.MenuId);
                aspNetClientConsultado.aspNetClientMenus.Add(
                    new AspNetClientMenu
                    {
                        ModuleId = moduleConsultado.ModuleId,
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
            if (aspNetClientConsultado != null)
            {
                _context.aspNetClients.Remove(aspNetClientConsultado);
                await _context.SaveChangesAsync();
            }
        }
        #endregion
    }
}
