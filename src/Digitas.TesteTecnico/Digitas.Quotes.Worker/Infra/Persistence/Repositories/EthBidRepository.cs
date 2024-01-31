using Digitas.Quotes.Worker.Domain.Entities;
using Digitas.Quotes.Worker.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Digitas.Quotes.Worker.Infra.Persistence.Repositories;

public class EthBidRepository : IEthBidRepository
{
    protected readonly ApplicationDbContext Db;
    protected readonly DbSet<EthBid> DbSet;

    public EthBidRepository(ApplicationDbContext Db)
    {
        this.Db = Db;
        this.DbSet = Db.Set<EthBid>();
    }

    public async Task AddRange(IEnumerable<EthBid> bids) => await DbSet.AddRangeAsync(bids);
}
