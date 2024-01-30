using Digitas.Quotes.Domain.Entities;
using Digitas.Quotes.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Digitas.Quotes.Infra.Persistence.Repositories;

public class EthBidRepository : IEthBidRepository
{
    protected readonly SimulationDbContext Db;
    protected readonly DbSet<EthBid> DbSet;

    public EthBidRepository(SimulationDbContext Db)
    {
        this.Db = Db;
        DbSet = Db.Set<EthBid>();
    }

    public async Task<IEnumerable<EthBid>> GetLastUpdatedQuoutes()
    {
        return await DbSet.OrderByDescending(eth => eth.Microtimestamp)
                          .ThenByDescending(btc => btc.UsdValue)
                          .Take(100)
                          .ToListAsync();
    }
}
