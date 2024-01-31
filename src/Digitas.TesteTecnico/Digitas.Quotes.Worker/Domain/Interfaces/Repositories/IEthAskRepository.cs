using Digitas.Quotes.Worker.Domain.Entities;

namespace Digitas.Quotes.Worker.Domain.Interfaces.Repositories;

public interface IEthAskRepository
{
    Task AddRange(IEnumerable<EthAsk> asks);
}
