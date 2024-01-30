using Digitas.Quotes.Worker.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Digitas.Quotes.Worker.Infra.Persistence.Mappings;

public class EthAskMap : IEntityTypeConfiguration<EthAsk>
{
    public void Configure(EntityTypeBuilder<EthAsk> builder)
    {
        builder.ToTable("EthAsk");

        builder.HasKey(b => b.EthAskId);

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
