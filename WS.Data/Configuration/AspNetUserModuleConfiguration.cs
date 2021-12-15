using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using WS.Core.Domain;

namespace WS.Data.Configuration
{
    public class AspNetUserModuleConfiguration : IEntityTypeConfiguration<AspNetUserModule>
    {
        public void Configure(EntityTypeBuilder<AspNetUserModule> builder)
        {
            builder.ToTable("AspNetUserModule");
            builder.HasKey(u => new { u.UserId, u.ModuleId });

            builder.HasOne(x => x.aspNetUser).WithMany(c => c.aspNetUserModules).HasForeignKey(c => c.UserId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(x => x.aspNetModule).WithMany(c => c.aspNetUserModules).HasForeignKey(c => c.ModuleId).OnDelete(DeleteBehavior.Cascade);

        }
    }
}
