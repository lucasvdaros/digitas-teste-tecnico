using Digitas.QuotesMetrics.Function.Domain.ValueObject;

namespace Digitas.QuotesMetrics.Function.Domain.Interfaces.Services;

public interface IEthQuotesService
{
    Task<Metrics> GetEthMetrics();
}
