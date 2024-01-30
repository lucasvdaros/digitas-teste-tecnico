using Digitas.QuotesMetrics.Function.Domain.Entities;

namespace Digitas.QuotesMetrics.Function.Domain.Interfaces.Repositories;

public interface IEthAskRepository
{
    Task<List<EthAsk>> GetEthQuotesFromLargestToSmallestMicrotimestamp(int size = 100);    
}
