using Digitas.Quotes.Domain.Entities;
using Digitas.Quotes.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Digitas.Quotes.Infra.Persistence.Repositories;

public class BtcBidRepository : IBtcBidRepository
{
    protected readonly SimulationDbContext Db;
    protected readonly DbSet<BtcBid> DbSet;

    public BtcBidRepository(SimulationDbContext Db)
    {
        this.Db = Db;
        this.DbSet = Db.Set<BtcBid>();
    }

    public async Task<IEnumerable<BtcBid>> GetLastUpdatedQuoutes()
    {
        return await DbSet.OrderByDescending(btc => btc.Microtimestamp)
                          .ThenByDescending(btc => btc.UsdValue)
                          .Take(100)
                          .ToListAsync();
    }
}
