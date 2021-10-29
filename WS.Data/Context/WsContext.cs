using Microsoft.EntityFrameworkCore;
using WS.Core.Domain;
using WS.Data.Configuration;

namespace WS.Data.Context
{
    public class WsContext: DbContext
    {
        public DbSet<AspNetClient> aspNetClients { get; set; }
        public DbSet<AspNetClientModule> aspNetClientModules { get; set; }
        public DbSet<AspNetClientMenu> aspNetClientMenus { get; set; }
        public DbSet<AspNetModule> aspNetModules { get; set; }
        public DbSet<AspNetMenu> aspNetMenus { get; set; }

        public WsContext(DbContextOptions options): base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new AspNetClientConfiguration());
            builder.ApplyConfiguration(new AspNetClientModuleConfiguration());
            builder.ApplyConfiguration(new AspNetClientMenuConfiguration());

            builder.ApplyConfiguration(new AspNetModuleConfiguration());

            builder.ApplyConfiguration(new AspNetMenuConfiguration());

        }
    }
}
