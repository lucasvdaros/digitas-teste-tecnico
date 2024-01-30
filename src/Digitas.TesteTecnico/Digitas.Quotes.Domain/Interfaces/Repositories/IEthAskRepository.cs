using Digitas.Quotes.Domain.Entities;

namespace Digitas.Quotes.Domain.Interfaces.Repositories;

public interface IEthAskRepository
{
    Task<IEnumerable<EthAsk>> GetLastUpdatedQuoutes();
}
