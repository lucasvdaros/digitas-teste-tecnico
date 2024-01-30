using Digitas.Quotes.Worker.Domain.Interfaces.Repositories;
using Digitas.Quotes.Worker.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Digitas.Quotes.Worker.Infra.Persistence;
using Microsoft.EntityFrameworkCore;
using Digitas.Quotes.Worker.Infra.ExternalServices.BitStamp.Interface;
using Digitas.Quotes.Worker.Infra.ExternalServices.BitStamp;
using Digitas.Quotes.Worker.Infra.Persistence.Uow;
using Digitas.Quotes.Worker.Infra.Persistence.Repositories;

namespace Digitas.Quotes.Worker.Infra.Extension.DependencyInjection;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDatabaseServices(configuration);
        services.AddExternalServices();
        services.AddRepositories();

        return services;
    }

    private static IServiceCollection AddDatabaseServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
             options.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=quotes;TrustServerCertificate=True;Persist Security Info=True;",
         builder => builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

        return services;
    }

    private static IServiceCollection AddExternalServices(this IServiceCollection services)
    {
        services.AddSingleton<IBitStampService, BitStampService>();
        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IBtcAskRepository, BtcAskRepository>();

        return services;
    }
}
