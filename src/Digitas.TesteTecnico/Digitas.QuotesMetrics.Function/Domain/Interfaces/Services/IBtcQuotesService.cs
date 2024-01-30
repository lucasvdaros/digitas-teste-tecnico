using Digitas.QuotesMetrics.Function.Domain.ValueObject;

namespace Digitas.QuotesMetrics.Function.Domain.Interfaces.Services;

public interface IBtcQuotesService
{
    Task<Metrics> GetBtcMetrics();
}
