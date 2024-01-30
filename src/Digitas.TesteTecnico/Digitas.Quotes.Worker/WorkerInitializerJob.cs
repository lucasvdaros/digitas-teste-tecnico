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

        await mediator.Send(new LoadQuoteBtcCommand());

        //await mediator.Send(new LoadQuoteBTCCommand(Coin.ETH));        
    }
}
