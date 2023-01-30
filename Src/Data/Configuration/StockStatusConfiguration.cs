using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Src.Domain;

namespace Src.Data.Configuration;

public class StockStatusConfiguration : IEntityTypeConfiguration<StockStatus>
{
    public void Configure(EntityTypeBuilder<StockStatus> builder)
    {
        builder.Property(p => p.Amount).IsRequired();
    }
}