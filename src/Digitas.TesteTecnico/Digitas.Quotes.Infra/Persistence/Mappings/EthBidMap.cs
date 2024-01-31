using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Digitas.Quotes.Domain.Entities;

namespace Digitas.Quotes.Infra.Persistence.Mappings;

public class EthBidMap : IEntityTypeConfiguration<EthBid>
{
    public void Configure(EntityTypeBuilder<EthBid> builder)
    {
        builder.ToTable("EthBid");

        builder.HasKey(b => b.EthBidId);

        builder.Property(c => c.Microtimestamp)
                .IsRequired()
                .HasColumnName("Microtimestamp");

        builder.Property(c => c.UsdValue)
                .IsRequired()
                .HasColumnName("UsdValue");

        builder.Property(c => c.Amount)
                .IsRequired()
                .HasPrecision(11, 8)
                .HasColumnName("Amount");
    }
}
