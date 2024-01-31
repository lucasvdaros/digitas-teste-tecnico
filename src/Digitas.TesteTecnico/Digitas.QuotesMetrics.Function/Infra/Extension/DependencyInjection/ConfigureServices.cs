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
             options.UseSqlServer("Data Source=N1003729\\SQLEXPRESS;Initial Catalog=quotes;TrustServerCertificate=True;Persist Security Info=True;User ID=user_digitas_db;Password=senhalegal123",
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
