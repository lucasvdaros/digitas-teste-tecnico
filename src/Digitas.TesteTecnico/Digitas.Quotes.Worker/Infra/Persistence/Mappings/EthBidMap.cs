using Digitas.Quotes.Worker.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Digitas.Quotes.Worker.Infra.Persistence.Mappings;

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
                .HasColumnName("Amount");
    }
}
