using Digitas.QuotesMetrics.Function.Domain.Entities;

namespace Digitas.QuotesMetrics.Function.Domain.Interfaces.Repositories;

public interface IBtcAskRepository
{
    Task<List<BtcAsk>> GetBtcQuotesFromLargestToSmallestMicrotimestamp(int size = 100);
}
