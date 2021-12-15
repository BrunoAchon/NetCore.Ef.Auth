using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using WS.Core.Domain;

namespace WS.Data.Configuration
{
    public class AspNetRoleMenuConfiguration : IEntityTypeConfiguration<AspNetRoleMenu>
    {
        public void Configure(EntityTypeBuilder<AspNetRoleMenu> builder)
        {
            builder.ToTable("AspNetRoleMenu");
            builder.HasKey(u => new { u.RoleId, u.MenuId });

            builder.Property(x => x.RoleId).IsRequired();
            builder.Property(x => x.ModuleId).IsRequired();
            builder.Property(x => x.MenuId).IsRequired();

            builder.Property(x => x.Exibir).HasDefaultValue(true);
            builder.Property(x => x.Inserir).HasDefaultValue(true);
            builder.Property(x => x.Editar).HasDefaultValue(true);
            builder.Property(x => x.Excluir).HasDefaultValue(true);

            builder.HasOne(x => x.aspNetRole).WithMany(c => c.aspNetRoleMenus).HasForeignKey(c => c.RoleId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(x => x.aspNetModule).WithMany(c => c.aspNetRoleMenus).HasForeignKey(c => c.ModuleId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(x => x.aspNetMenu).WithMany(c => c.aspNetRoleMenus).HasForeignKey(c => c.MenuId).OnDelete(DeleteBehavior.Cascade);

        }
    }
}
