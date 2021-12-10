using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WS.Core.Domain;
using WS.Data.Context;
using WS.Mananger.Interfaces.Repositories;


namespace WS.Data.Repository
{
    public class AspNetModuleRepository : IAspNetModuleRepository
    {
        private readonly WsContext _context;
        public AspNetModuleRepository(WsContext context)
        {
            _context = context;
        }

        #region Get 
        public async Task<bool> ExistsAsync(int id)
        {
            //return await _context.aspNetModules.FindAsync(id) != null;
            return await _context.aspNetModules.AsNoTracking().AnyAsync(p => p.ModuleId == id);
        }

        public async Task<IEnumerable<AspNetModule>> GetAspNetModulesAsync()
        {
            return await _context.aspNetModules
                .Include(m => m.aspNetMenus)
                .AsNoTracking().ToListAsync();
        }

        public async Task<AspNetModule> GetAspNetModuleAsync(int id)
        {
            return await _context.aspNetModules
                .Include(m => m.aspNetMenus)
                .AsNoTracking().SingleOrDefaultAsync(p => p.ModuleId == id);
        }
        #endregion

        #region Insert
        public async Task<AspNetModule> InsertAspNetModuleAsync(AspNetModule aspNetModule)
        {
            await _context.aspNetModules.AddAsync(aspNetModule);
            await _context.SaveChangesAsync();
            return aspNetModule;
        }
        #endregion

        #region Update
        public async Task<AspNetModule> UpdateAspNetModuleAsync(AspNetModule aspNetModule)
        {
            var aspNetModuleConsultado = await _context.aspNetModules
                                               .Include(c => c.aspNetMenus)
                                               .SingleOrDefaultAsync(c => c.ModuleId == aspNetModule.ModuleId);
            if (aspNetModuleConsultado == null)
            {
                return null;
            }
            _context.Entry(aspNetModuleConsultado).CurrentValues.SetValues(aspNetModule);

            await UpdateAspNetMenuAsync(aspNetModule, aspNetModuleConsultado);
            await _context.SaveChangesAsync();
            return aspNetModuleConsultado;
        }

        private async Task UpdateAspNetMenuAsync(AspNetModule aspNetModule, AspNetModule aspNetModuleConsultado)
        {
            // serparando a lista original para verificar deleções ao final do update
            List<AspNetMenu> menuExcluir = aspNetModuleConsultado.aspNetMenus.ToList();

            foreach (var menu in aspNetModule.aspNetMenus)
            {
                //var menuConsultado = await _context.aspNetMenus.FindAsync(menu.MenuId); // Não funciona em teste
                var menuConsultado = aspNetModuleConsultado.aspNetMenus.FirstOrDefault(c => c.MenuId == menu.MenuId);

                if (menuConsultado == null)
                {
                    //Incluir
                    aspNetModuleConsultado.aspNetMenus.Add(
                        new AspNetMenu
                        {
                            ModuleId = menu.ModuleId,
                            Menu = menu.Menu,
                            Ordem = menu.Ordem
                        });
                    await _context.SaveChangesAsync();
                }
                else
                {
                    //Alterar
                    menuConsultado.Menu = menu.Menu;
                    menuConsultado.Ordem = menu.Ordem;
                    await _context.SaveChangesAsync();
                }
            }

            List<AspNetMenu> Excluir = menuExcluir.ToList()
                  .Where(s => !aspNetModule.aspNetMenus.ToList().Any(p => p.MenuId == s.MenuId)).ToList();

            foreach (var menu in Excluir)
            {
                //Excluir
                aspNetModuleConsultado.aspNetMenus.Remove(menu);
                await _context.SaveChangesAsync();
            }
        }
        #endregion

        #region Delete
        public async Task<AspNetModule> DeleteAspNetModuleAsync(int id)
        {
            var aspNetModuleConsultado = await _context.aspNetModules.FindAsync(id);
            if (aspNetModuleConsultado == null)
            {
                return null;
            }
            var aspNetModuleRemovido = _context.aspNetModules.Remove(aspNetModuleConsultado);
            await _context.SaveChangesAsync();

            return aspNetModuleRemovido.Entity;
        }
        #endregion
    }
}
