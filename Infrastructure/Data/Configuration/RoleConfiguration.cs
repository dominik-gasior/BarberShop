using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Src.Domain;

namespace Src.Data.Configuration;

internal class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Name).IsRequired();

        builder.HasMany(p => p.Employees)
            .WithOne(p => p.Role)
            .HasForeignKey(p => p.RoleId);
    }
}