using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using WS.Core.Domain;

namespace WS.Data.Configuration
{
    public class AspNetUserMenuConfiguration : IEntityTypeConfiguration<AspNetUserMenu>
    {
        public void Configure(EntityTypeBuilder<AspNetUserMenu> builder)
        {
            builder.ToTable("AspNetUserMenu");
            builder.HasKey(u => new { u.UserId, u.MenuId });

            builder.Property(x => x.UserId).IsRequired();
            builder.Property(x => x.ModuleId).IsRequired();
            builder.Property(x => x.MenuId).IsRequired();

            builder.Property(x => x.Exibir).HasDefaultValue(true);
            builder.Property(x => x.Inserir).HasDefaultValue(true);
            builder.Property(x => x.Editar).HasDefaultValue(true);
            builder.Property(x => x.Excluir).HasDefaultValue(true);

            builder.HasOne(x => x.aspNetUser).WithMany(c => c.aspNetUserMenus).HasForeignKey(c => c.UserId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(x => x.aspNetModule).WithMany(c => c.aspNetUserMenus).HasForeignKey(c => c.ModuleId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(x => x.aspNetMenu).WithMany(c => c.aspNetUserMenus).HasForeignKey(c => c.MenuId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
