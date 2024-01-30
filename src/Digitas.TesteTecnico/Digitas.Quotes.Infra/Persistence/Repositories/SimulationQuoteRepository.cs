using Digitas.Quotes.Domain.Entities;
using Digitas.Quotes.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Digitas.Quotes.Infra.Persistence.Repositories;

public class SimulationQuoteRepository : ISimulationQuoteRepository
{
    protected readonly SimulationDbContext Db;
    protected readonly DbSet<SimulationQuote> DbSet;

    public SimulationQuoteRepository(SimulationDbContext Db)
    {
        this.Db = Db;
        DbSet = Db.Set<SimulationQuote>();
    }

    public async Task AddAsync(SimulationQuote simulationQuoute) => await DbSet.AddAsync(simulationQuoute);

    public async Task<SimulationQuote?> GetSimulationQuoteByQuoteHashIdentifier(string idSimulation) =>    
        await DbSet.Where(sq => sq.SimulationQuoteHashIdentifier.Equals(idSimulation)).FirstOrDefaultAsync();    
}
