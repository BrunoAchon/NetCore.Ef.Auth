using System.Data;
using WS.Core.Domain;
using WS.Data.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace WS.Data.Context
{
    public class WsContext : IdentityDbContext<AspNetUser, AspNetRole, int,
                                              IdentityUserClaim<int>, AspNetUserRole, IdentityUserLogin<int>,
                                              IdentityRoleClaim<int>, IdentityUserToken<int>>
    //public class WsContext : DbContext
    {
        public IDbConnection Connection => Database.GetDbConnection();
        public DbSet<AspNetClient> aspNetClients { get; set; }
        public DbSet<AspNetClientModule> aspNetClientModules { get; set; }
        public DbSet<AspNetClientMenu> aspNetClientMenus { get; set; }
        public DbSet<AspNetModule> aspNetModules { get; set; }
        public DbSet<AspNetMenu> aspNetMenus { get; set; }
        public DbSet<AspNetUser> aspNetUsers { get; set; }

        public WsContext(DbContextOptions options): base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new AspNetClientConfiguration());
            builder.ApplyConfiguration(new AspNetClientModuleConfiguration());
            builder.ApplyConfiguration(new AspNetClientMenuConfiguration());

            builder.ApplyConfiguration(new AspNetModuleConfiguration());

            builder.ApplyConfiguration(new AspNetMenuConfiguration());

            builder.ApplyConfiguration(new AspNetRoleConfiguration());
            builder.ApplyConfiguration(new AspNetRoleModuleConfiguration());
            builder.ApplyConfiguration(new AspNetRoleMenuConfiguration());

            builder.ApplyConfiguration(new AspNetUserConfiguration());
            builder.ApplyConfiguration(new AspNetUserModuleConfiguration());
            builder.ApplyConfiguration(new AspNetUserMenuConfiguration());
            builder.ApplyConfiguration(new AspNetUserRoleConfiguration());

        }
    }
}
