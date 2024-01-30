using Digitas.Quotes.Worker.Domain.Entities;

namespace Digitas.Quotes.Worker.Domain.Interfaces.Repositories;

public interface IBtcAskRepository
{
    Task AddRange(IEnumerable<BtcAsk> asks);
}
