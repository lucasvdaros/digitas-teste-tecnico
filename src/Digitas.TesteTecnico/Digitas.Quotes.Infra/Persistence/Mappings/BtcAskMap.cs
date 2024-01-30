using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Digitas.Quotes.Domain.Entities;

namespace Digitas.Quotes.Infra.Persistence.Mappings;

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
                .HasColumnName("Amount");
    }
}
