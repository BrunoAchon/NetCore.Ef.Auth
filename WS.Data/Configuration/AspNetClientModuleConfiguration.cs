using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WS.Core.Domain;

namespace WS.Data.Configuration
{
    class AspNetClientModuleConfiguration : IEntityTypeConfiguration<AspNetClientModule>
    {
        public void Configure(EntityTypeBuilder<AspNetClientModule> builder)
        {
            builder.ToTable("AspNetClientModule");
            builder.HasKey(u => new { u.ClientId, u.ModuleId });

            builder.Property(x => x.ClientId).IsRequired();
            builder.Property(x => x.ModuleId).IsRequired();

            builder.Property(x => x.Vencimento).HasColumnType("Date").HasDefaultValue(null);

            builder.HasOne(x => x.aspNetClient).WithMany(c => c.aspNetClientModules).HasForeignKey(c => c.ClientId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(x => x.aspNetModule).WithMany(c => c.aspNetClientModules).HasForeignKey(c => c.ModuleId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
