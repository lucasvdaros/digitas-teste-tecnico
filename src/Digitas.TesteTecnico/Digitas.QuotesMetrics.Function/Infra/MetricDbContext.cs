using Digitas.QuotesMetrics.Function.Infra.Persistence.Mappings;
using Microsoft.EntityFrameworkCore;

namespace Digitas.QuotesMetrics.Function.Infra;

public class MetricDbContext : DbContext
{
    public MetricDbContext(DbContextOptions<MetricDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new BtcAskMap());
        modelBuilder.ApplyConfiguration(new BtcBidMap());
        modelBuilder.ApplyConfiguration(new EthAskMap());
        modelBuilder.ApplyConfiguration(new EthBidMap());       

        base.OnModelCreating(modelBuilder);
    }
}
