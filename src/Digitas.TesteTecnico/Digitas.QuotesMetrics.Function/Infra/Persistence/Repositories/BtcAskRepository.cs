using Digitas.QuotesMetrics.Function.Domain.Entities;
using Digitas.QuotesMetrics.Function.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Digitas.QuotesMetrics.Function.Infra.Persistence.Repositories;

public class BtcAskRepository : IBtcAskRepository
{
    protected readonly MetricDbContext Db;
    protected readonly DbSet<BtcAsk> DbSet;

    public BtcAskRepository(MetricDbContext Db)
    {
        this.Db = Db;
        this.DbSet = Db.Set<BtcAsk>();
    }

    public async Task<List<BtcAsk>> GetBtcQuotesFromLargestToSmallestMicrotimestamp(int size = 100) =>
         await DbSet.OrderByDescending(btcs => btcs.Microtimestamp).ToListAsync();
}
