using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using WS.Core.Domain;

namespace WS.Data.Configuration
{
    public class AspNetRoleModuleConfiguration : IEntityTypeConfiguration<AspNetRoleModule>
    {
        public void Configure(EntityTypeBuilder<AspNetRoleModule> builder)
        {
            builder.ToTable("AspNetRoleModule");
            builder.HasKey(u => new { u.RoleId, u.ModuleId });

            builder.HasOne(x => x.aspNetRole).WithMany(c => c.aspNetRoleModule).HasForeignKey(c => c.RoleId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(x => x.aspNetModule).WithMany(c => c.aspNetRoleModules).HasForeignKey(c => c.ModuleId).OnDelete(DeleteBehavior.Cascade);

        }
    }
}
