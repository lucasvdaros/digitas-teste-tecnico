using Digitas.Quotes.Worker.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Digitas.Quotes.Worker.Infra.Persistence.Mappings;

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
