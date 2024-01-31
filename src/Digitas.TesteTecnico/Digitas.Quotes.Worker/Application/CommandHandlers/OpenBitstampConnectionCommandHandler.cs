using Digitas.Quotes.Worker.Application.Commands;
using Digitas.Quotes.Worker.Domain.Interfaces.Services;
using MediatR;

namespace Digitas.Quotes.Worker.Application.CommandHandlers;

public class OpenBitstampConnectionCommandHandler : IRequestHandler<OpenBitstampConnectionCommand, bool>
{
    public readonly ILiveOrderBookService liveOrderBookService;

    public OpenBitstampConnectionCommandHandler(ILiveOrderBookService liveOrderBookService)
    {
        this.liveOrderBookService = liveOrderBookService;
    }

    public async Task<bool> Handle(OpenBitstampConnectionCommand request, CancellationToken cancellationToken)
    {
        return await liveOrderBookService.ConnectToBitStampWebSocket();
    }
}
