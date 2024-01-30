using Digitas.Quotes.Domain.Interfaces;
using Digitas.Quotes.Domain.Interfaces.Repositories;
using Digitas.Quotes.Infra.Persistence.Repositories;
using Digitas.Quotes.Infra.Persistence.Uow;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Digitas.Quotes.Infra.Extension.DependencyInjection;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDatabaseServices(configuration);
        services.AddRepositories();

        return services;
    }

    private static IServiceCollection AddDatabaseServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<SimulationDbContext>(options =>
             options.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=quotes;TrustServerCertificate=True;Persist Security Info=True;",
         builder => builder.MigrationsAssembly(typeof(SimulationDbContext).Assembly.FullName)));

        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<IBtcAskRepository, BtcAskRepository>();
        services.AddScoped<IEthAskRepository, EthAskRepository>();
        services.AddScoped<IBtcBidRepository, BtcBidRepository>();
        services.AddScoped<IEthBidRepository, EthBidRepository>();
        services.AddScoped<ISimulationQuoteRepository, SimulationQuoteRepository>();
        services.AddScoped<ISimulationQuoteValueRepository, SimulationQuoteValueRepository>();

        return services;
    }
}
