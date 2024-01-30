using Digitas.Quotes.Domain.Entities;

namespace Digitas.Quotes.Domain.Interfaces.Repositories;

public interface IEthBidRepository
{
    Task<IEnumerable<EthBid>> GetLastUpdatedQuoutes();
}
