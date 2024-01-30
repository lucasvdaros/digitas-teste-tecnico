using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

var builder = new HostBuilder()
    .ConfigureAppConfiguration((hostContext, config) =>
    {
        config
            .AddJsonFile($"appsettings.json", false, true)
            .AddEnvironmentVariables();
    })    
    .ConfigureLogging((context, builder) =>
    {
        //builder.AddConsole();
        //builder.AddEventSourceLogger();      
        builder.AddFilter("Microsoft.EntityFrameworkCore", LogLevel.None);
        builder.AddFilter("System.Net.Http", LogLevel.None);
        builder.AddFilter("Microsoft.Azure.WebJobs", LogLevel.None);       
    })
    .ConfigureServices((context, services) =>
    {
        //var config = hostContext.Configuration;



    })
    .UseConsoleLifetime();

var host = builder.Build();
using (host)
{
    await host.RunAsync();
}