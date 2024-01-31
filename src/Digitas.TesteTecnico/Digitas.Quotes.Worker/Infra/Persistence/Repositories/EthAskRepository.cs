using Digitas.Quotes.Worker.Domain.Entities;
using Digitas.Quotes.Worker.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Digitas.Quotes.Worker.Infra.Persistence.Repositories;

public class EthAskRepository : IEthAskRepository
{
    protected readonly ApplicationDbContext Db;
    protected readonly DbSet<EthAsk> DbSet;

    public EthAskRepository(ApplicationDbContext Db)
    {
        this.Db = Db;
        this.DbSet = Db.Set<EthAsk>();
    }

    public async Task AddRange(IEnumerable<EthAsk> asks) => await DbSet.AddRangeAsync(asks);
}
