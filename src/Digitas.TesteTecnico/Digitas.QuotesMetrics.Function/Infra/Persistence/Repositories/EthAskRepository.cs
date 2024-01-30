using Digitas.QuotesMetrics.Function.Domain.Entities;
using Digitas.QuotesMetrics.Function.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Digitas.QuotesMetrics.Function.Infra.Persistence.Repositories;

public class EthAskRepository : IEthAskRepository
{
    protected readonly MetricDbContext Db;
    protected readonly DbSet<EthAsk> DbSet;

    public EthAskRepository(MetricDbContext Db)
    {
        this.Db = Db;
        this.DbSet = Db.Set<EthAsk>();
    }

    public async Task<List<EthAsk>> GetEthQuotesFromLargestToSmallestMicrotimestamp(int size = 100) =>    
        await DbSet.OrderByDescending(eths => eths.Microtimestamp).ToListAsync();    
}
