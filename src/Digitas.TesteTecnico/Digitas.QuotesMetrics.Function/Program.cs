using Digitas.QuotesMetrics.Function.Domain.Extension.DependencyInjection;
using Digitas.QuotesMetrics.Function.Infra.Extension.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureServices((hostContext, services) =>
    {
        var config = hostContext.Configuration;   

        services.AddApplicationServices();
        services.AddInfrastructureServices(config);
    });

var host = builder.Build();

host.Run();
