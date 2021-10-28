using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WS.Core.Domain;

namespace WS.Data.Configuration
{
    public class AspNetModuleConfiguration : IEntityTypeConfiguration<AspNetModule>
    {
        public void Configure(EntityTypeBuilder<AspNetModule> builder)
        {
            builder.ToTable("AspNetModule");
            builder.HasKey(x => x.ModuleId);
            builder.Property(x => x.Module).HasMaxLength(50).IsRequired();
            builder.Property(x => x.ImgMenu).HasMaxLength(100);
            builder.Property(x => x.Ordem).HasDefaultValue(0).IsRequired();
        }
    }
}
