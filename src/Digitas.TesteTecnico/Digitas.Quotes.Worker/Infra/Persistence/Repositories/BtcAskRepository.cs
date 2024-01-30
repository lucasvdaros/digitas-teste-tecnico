using Digitas.Quotes.Worker.Domain.Entities;
using Digitas.Quotes.Worker.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Digitas.Quotes.Worker.Infra.Persistence.Repositories;

public class BtcAskRepository : IBtcAskRepository
{
    protected readonly ApplicationDbContext Db;
    protected readonly DbSet<BtcAsk> DbSet;

    public BtcAskRepository(ApplicationDbContext Db)
    {
        this.Db = Db;
        this.DbSet = Db.Set<BtcAsk>();
    }

    public async Task AddRange(IEnumerable<BtcAsk> asks) => await DbSet.AddRangeAsync(asks);
}
