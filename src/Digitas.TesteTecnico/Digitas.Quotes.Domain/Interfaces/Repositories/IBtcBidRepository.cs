using Digitas.Quotes.Domain.Entities;

namespace Digitas.Quotes.Domain.Interfaces.Repositories;

public interface IBtcBidRepository
{
    Task<IEnumerable<BtcBid>> GetLastUpdatedQuoutes();
}
