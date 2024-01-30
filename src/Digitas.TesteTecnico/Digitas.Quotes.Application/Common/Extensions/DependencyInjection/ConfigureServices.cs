using Digitas.Quotes.Domain.Interfaces.Services;
using Digitas.Quotes.Domain.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Digitas.Quotes.Application.Common.Extensions.DependencyInjection;

public static class ConfigureServices
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {   
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        services.AddServices();

        return services;
    }

    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IBtcOperationService, BtcOperationService>();
        services.AddScoped<IEthOperationService, EthOperationService>();
        services.AddScoped<IQuoteService, QuoteService>();
        services.AddScoped<ISimulationService, SimulationService>();

        return services;
    }
}
