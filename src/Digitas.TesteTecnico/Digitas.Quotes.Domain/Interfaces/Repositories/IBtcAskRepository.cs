using Digitas.Quotes.Domain.Entities;

namespace Digitas.Quotes.Domain.Interfaces.Repositories;

public interface IBtcAskRepository
{
    Task<IEnumerable<BtcAsk>> GetLastUpdatedQuoutes();
}
