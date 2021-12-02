using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WS.Core.Domain;

namespace WS.Data.Configuration
{
    class AspNetClientMenuConfiguration : IEntityTypeConfiguration<AspNetClientMenu>
    {
        public void Configure(EntityTypeBuilder<AspNetClientMenu> builder)
        {
            builder.ToTable("AspNetClientMenu");
            builder.HasKey(u => new { u.ClientId, u.MenuId });

            builder.Property(x => x.ClientId).IsRequired();
            builder.Property(x => x.ModuleId).IsRequired();
            builder.Property(x => x.MenuId).IsRequired();

            builder.Property(x => x.Exibir).HasDefaultValue(true);
            builder.Property(x => x.Inserir).HasDefaultValue(true);
            builder.Property(x => x.Editar).HasDefaultValue(true);
            builder.Property(x => x.Excluir).HasDefaultValue(true);

            builder.HasOne(x => x.aspNetClient).WithMany(c => c.aspNetClientMenus).HasForeignKey(c => c.ClientId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(x => x.aspNetModule).WithMany(c => c.aspNetClientMenus).HasForeignKey(c => c.ModuleId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(x => x.aspNetMenu).WithMany(c => c.aspNetClientMenus).HasForeignKey(c => c.MenuId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
