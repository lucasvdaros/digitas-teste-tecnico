using Digitas.Quotes.Domain.Entities;
using Digitas.Quotes.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Digitas.Quotes.Infra.Persistence.Repositories;

public class EthAskRepository : IEthAskRepository
{
    protected readonly SimulationDbContext Db;
    protected readonly DbSet<EthAsk> DbSet;

    public EthAskRepository(SimulationDbContext Db)
    {
        this.Db = Db;
        this.DbSet = Db.Set<EthAsk>();
    }

    public async Task<IEnumerable<EthAsk>> GetLastUpdatedQuoutes()
    {
        return await DbSet.OrderByDescending(eth => eth.Microtimestamp)
                          .ThenBy(btc => btc.UsdValue)
                          .Take(100)
                          .ToListAsync();
    }
}
