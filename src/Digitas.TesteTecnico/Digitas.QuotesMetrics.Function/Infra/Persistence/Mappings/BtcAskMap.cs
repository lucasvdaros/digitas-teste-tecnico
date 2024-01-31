using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Digitas.QuotesMetrics.Function.Domain.Entities;

namespace Digitas.QuotesMetrics.Function.Infra.Persistence.Mappings;

public class BtcAskMap : IEntityTypeConfiguration<BtcAsk>
{
    public void Configure(EntityTypeBuilder<BtcAsk> builder)
    {
        builder.ToTable("BtcAsk");

        builder.HasKey(b => b.BtcAskId);

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
