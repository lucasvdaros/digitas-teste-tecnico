using Digitas.Quotes.Domain.Entities;
using Digitas.Quotes.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Digitas.Quotes.Infra.Persistence.Repositories;

public class SimulationQuoteValueRepository : ISimulationQuoteValueRepository
{
    protected readonly SimulationDbContext Db;
    protected readonly DbSet<SimulationQuoteValue> DbSet;

    public SimulationQuoteValueRepository(SimulationDbContext Db)
    {
        this.Db = Db;
        DbSet = Db.Set<SimulationQuoteValue>();
    }

    public async Task AddRangeAsync(IEnumerable<SimulationQuoteValue> simulationQuoute) => await DbSet.AddRangeAsync(simulationQuoute);

    public async Task<List<SimulationQuoteValue>> GetQuoutesValuesBySimulationId(int simulationQuoteId) =>
        await DbSet.Where(sqv => sqv.SimulationQuoteId == simulationQuoteId).ToListAsync();
}
