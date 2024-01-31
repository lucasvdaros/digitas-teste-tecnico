using Digitas.Quotes.Worker.Application.Commands;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Digitas.Quotes.Worker;

internal class WorkerInitializerJob
{
    private readonly ILogger<WorkerInitializerJob> logger;
    private readonly ISender mediator;

    public WorkerInitializerJob(ILogger<WorkerInitializerJob> logger, ISender mediator)
    {
        this.logger = logger;
        this.mediator = mediator;
    }

    public async Task ExecuteAsync()
    {
        logger.LogInformation(message: $"Start process");

        var IsWebSocketConnected = await mediator.Send(new OpenBitstampConnectionCommand());

        if (IsWebSocketConnected)
        {
            logger.LogInformation(message: $"Web Socket Connected!");

            //await Task.WhenAll(mediator.Send(new LoadQuoteBtcCommand()),
            //                   mediator.Send(new LoadQuoteEthCommand()));

            //await mediator.Send(new LoadQuoteBtcCommand());

            await mediator.Send(new LoadQuoteEthCommand());
        }
        else
        {
            logger.LogError(message: "Falha ao se conectar com o Bitstamp websocket");
        }
    }
}
