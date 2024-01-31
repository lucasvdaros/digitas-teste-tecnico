using Digitas.Quotes.Worker.Domain.Entities;
using Digitas.Quotes.Worker.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Digitas.Quotes.Worker.Infra.Persistence.Repositories;

public class BtcBidRepository: IBtcBidRepository
{
    protected readonly ApplicationDbContext Db;
    protected readonly DbSet<BtcBid> DbSet;

    public BtcBidRepository(ApplicationDbContext Db)
    {
        this.Db = Db;
        this.DbSet = Db.Set<BtcBid>();
    }

    public async Task AddRange(IEnumerable<BtcBid> bids) => await DbSet.AddRangeAsync(bids);
}
