using Digitas.Quotes.Domain.Entities;
using Digitas.Quotes.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Digitas.Quotes.Infra.Persistence.Repositories;

public class BtcAskRepository : IBtcAskRepository
{
    protected readonly SimulationDbContext Db;
    protected readonly DbSet<BtcAsk> DbSet;

    public BtcAskRepository(SimulationDbContext Db)
    {
        this.Db = Db;
        this.DbSet = Db.Set<BtcAsk>();
    }

    public async Task<IEnumerable<BtcAsk>> GetLastUpdatedQuoutes()
    {
        return await DbSet.OrderByDescending(btc => btc.Microtimestamp)
                          .ThenBy(btc => btc.UsdValue)
                          .Take(100)
                          .ToListAsync();
    }
}
