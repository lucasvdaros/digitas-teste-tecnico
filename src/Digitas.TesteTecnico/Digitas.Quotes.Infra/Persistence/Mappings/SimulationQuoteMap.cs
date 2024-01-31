using Digitas.Quotes.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Digitas.Quotes.Infra.Persistence.Mappings;

public class SimulationQuoteMap : IEntityTypeConfiguration<SimulationQuote>
{
    public void Configure(EntityTypeBuilder<SimulationQuote> builder)
    {
        builder.ToTable("SimulationQuote");

        builder.HasKey(b => b.SimulationQuoteId);

        builder.Property(c => c.SimulationQuoteHashIdentifier)
                .IsRequired()
                .HasColumnName("SimulationQuoteHashIdentifier");

        builder.Property(c => c.OperationChoice)
                .IsRequired()
                .HasColumnName("OperationChoice");

        builder.Property(c => c.Coin)
                .IsRequired()
                .HasColumnName("Coin");

        builder.Property(c => c.RequestAmount)
                .IsRequired()
                .HasPrecision(11, 8)
                .HasColumnName("RequestAmount");        

        builder.Property(c => c.FinalResult)
                .IsRequired()
                .HasPrecision(12, 8)
                .HasColumnName("FinalResult");        
    }
}
