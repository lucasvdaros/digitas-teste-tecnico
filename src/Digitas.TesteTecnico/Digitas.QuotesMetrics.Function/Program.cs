using Digitas.QuotesMetrics.Function.Domain.Extension.DependencyInjection;
using Digitas.QuotesMetrics.Function.Infra.Extension.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

var builder = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureLogging(logging =>
    {
        logging.ClearProviders();
        logging.AddConsole();
        logging.AddFilter("Microsoft.EntityFrameworkCore.Database.Command", LogLevel.Warning);
    })
    .ConfigureServices((hostContext, services) =>
    {
        var config = hostContext.Configuration;   

        services.AddApplicationServices();
        services.AddInfrastructureServices(config);
    });

var host = builder.Build();

host.Run();
