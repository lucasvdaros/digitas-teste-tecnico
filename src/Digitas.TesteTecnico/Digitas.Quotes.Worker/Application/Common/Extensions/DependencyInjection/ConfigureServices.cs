using Digitas.Quotes.Worker.Domain.Interfaces.Services;
using Digitas.Quotes.Worker.Domain.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Digitas.Quotes.Worker.Application.Common.Extensions.DependencyInjection;

public static class ConfigureServices
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddTransient<WorkerInitializerJob>();
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));        

        services.AddScoped<ILiveOrderBookService, LiveOrderBookService>();
        services.AddScoped<IBtcService, BtcService>();
        services.AddScoped<IEthService, EthService>();

        return services;
    }
}
