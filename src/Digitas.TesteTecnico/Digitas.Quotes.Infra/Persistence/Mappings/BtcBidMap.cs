using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Digitas.Quotes.Domain.Entities;

namespace Digitas.Quotes.Infra.Persistence.Mappings;

public class BtcBidMap : IEntityTypeConfiguration<BtcBid>
{
    public void Configure(EntityTypeBuilder<BtcBid> builder)
    {
        builder.ToTable("BtcBid");

        builder.HasKey(b => b.BtcBidId);

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
