using Digitas.QuotesMetrics.Function.Domain.Interfaces.Repositories;
using Digitas.QuotesMetrics.Function.Infra.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Digitas.QuotesMetrics.Function.Infra.Extension.DependencyInjection;

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
        services.AddDbContext<MetricDbContext>(options =>
             options.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=quotes;TrustServerCertificate=True;Persist Security Info=True;",
         builder => builder.MigrationsAssembly(typeof(MetricDbContext).Assembly.FullName)));

        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IBtcAskRepository, BtcAskRepository>();
        services.AddScoped<IEthAskRepository, EthAskRepository>();              

        return services;
    }
}
