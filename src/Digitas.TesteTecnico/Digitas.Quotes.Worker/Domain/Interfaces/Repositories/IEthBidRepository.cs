using Digitas.Quotes.Worker.Domain.Entities;

namespace Digitas.Quotes.Worker.Domain.Interfaces.Repositories;

public interface IEthBidRepository
{
    Task AddRange(IEnumerable<EthBid> bids);
}
