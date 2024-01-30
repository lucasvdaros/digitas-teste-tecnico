using Digitas.QuotesMetrics.Function.Domain.Interfaces.Services;
using Digitas.QuotesMetrics.Function.Domain.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Digitas.QuotesMetrics.Function.Domain.Extension.DependencyInjection;

public static class ConfigureServices
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IBtcQuotesService, BtcQuotesService>();
        services.AddScoped<IEthQuotesService, EthQuotesService>();

        return services;
    }   
}
