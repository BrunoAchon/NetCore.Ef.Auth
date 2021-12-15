using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using WS.Core.Domain;

namespace WS.Data.Configuration
{
    public class AspNetUserRoleConfiguration : IEntityTypeConfiguration<AspNetUserRole>
    {
        public void Configure(EntityTypeBuilder<AspNetUserRole> builder)
        {
            builder.ToTable("AspNetUserRole");
            builder.HasKey(ur => new { ur.UserId, ur.RoleId });

            builder.HasOne(ur => ur.aspNetUser).WithMany(r => r.aspNetUserRoles).HasForeignKey(ur => ur.UserId).IsRequired();
            builder.HasOne(ur => ur.aspNetRole).WithMany(r => r.aspNetUserRoles).HasForeignKey(ur => ur.RoleId).IsRequired();
        }
    }
}
