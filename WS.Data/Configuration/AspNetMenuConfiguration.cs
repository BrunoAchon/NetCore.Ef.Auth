using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using WS.Core.Domain;

namespace WS.Data.Configuration
{
    public class AspNetMenuConfiguration : IEntityTypeConfiguration<AspNetMenu>
    {
        public void Configure(EntityTypeBuilder<AspNetMenu> builder)
        {
            builder.ToTable("AspNetMenu");
            builder.HasKey(x => x.MenuId);
            builder.Property(x => x.Menu).HasMaxLength(50).IsRequired();
            builder.Property(x => x.Ordem).HasDefaultValue(0).IsRequired();

            builder.HasOne(x => x.Module).WithMany(c => c.aspNetMenus).HasForeignKey(c => c.ModuleId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}