using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using WS.Core.Domain;

namespace WS.Data.Configuration
{
    public class AspNetClientConfiguration : IEntityTypeConfiguration<AspNetClient>
    {
        public void Configure(EntityTypeBuilder<AspNetClient> builder)
        {
            builder.ToTable("AspNetClient");
            builder.HasKey(x => x.ClientId);
            builder.Property(x => x.Orgao).HasMaxLength(5).IsRequired();
            builder.Property(x => x.RazaoSocial).HasMaxLength(150).IsRequired();
            builder.Property(x => x.Server).HasMaxLength(50).IsRequired();
            builder.Property(x => x.Banco).HasMaxLength(50).IsRequired();
            builder.Property(x => x.Vencimento).HasColumnType("Date").HasDefaultValue(DateTime.Now.AddYears(4));
        }
    }
}
