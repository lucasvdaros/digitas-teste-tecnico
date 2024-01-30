using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Digitas.Quotes.Domain.Entities;

namespace Digitas.Quotes.Infra.Persistence.Mappings;

public class SimulationQuoteValueMap : IEntityTypeConfiguration<SimulationQuoteValue>
{
    public void Configure(EntityTypeBuilder<SimulationQuoteValue> builder)
    {
        builder.ToTable("SimulationQuoteValue");

        builder.HasKey(b => b.SimulationQuoteValueId);        

        builder.Property(c => c.UsdValue)
                .IsRequired()
                .HasColumnName("UsdValue");

        builder.Property(c => c.Amount)
                .IsRequired()
                .HasColumnName("Amount");

        builder.HasOne(o => o.SimulationQuote)
                .WithMany(c => c.SimulationQuotesValues)
                .HasForeignKey(o => o.SimulationQuoteId);
    }
}
