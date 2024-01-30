using Digitas.Quotes.Infra.Persistence.Mappings;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace Digitas.Quotes.Infra;

[ExcludeFromCodeCoverage]
public class SimulationDbContext : DbContext
{
    public SimulationDbContext(DbContextOptions<SimulationDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new BtcAskMap());
        modelBuilder.ApplyConfiguration(new BtcBidMap());
        modelBuilder.ApplyConfiguration(new EthAskMap());
        modelBuilder.ApplyConfiguration(new EthBidMap());
        modelBuilder.ApplyConfiguration(new SimulationQuoteMap());
        modelBuilder.ApplyConfiguration(new SimulationQuoteValueMap());

        base.OnModelCreating(modelBuilder);
    }
}