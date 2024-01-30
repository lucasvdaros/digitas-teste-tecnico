using Digitas.Quotes.Worker.Infra.Persistence.Mappings;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace Digitas.Quotes.Worker.Infra.Persistence;

[ExcludeFromCodeCoverage]
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new BtcAskMap());
        modelBuilder.ApplyConfiguration(new BtcBidMap());
        modelBuilder.ApplyConfiguration(new EthAskMap());
        modelBuilder.ApplyConfiguration(new EthBidMap());

        base.OnModelCreating(modelBuilder);
    }
}
