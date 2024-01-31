using Digitas.Quotes.Worker.Domain.Entities;

namespace Digitas.Quotes.Worker.Domain.Interfaces.Repositories;

public interface IBtcBidRepository
{
    Task AddRange(IEnumerable<BtcBid> bids);
}
