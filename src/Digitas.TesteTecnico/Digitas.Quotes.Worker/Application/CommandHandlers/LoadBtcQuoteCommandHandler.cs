using Digitas.Quotes.Worker.Application.Commands;
using Digitas.Quotes.Worker.Domain.Interfaces.Services;
using MediatR;

namespace Digitas.Quotes.Worker.Application.CommandHandlers;

public class LoadBtcQuoteCommandHandler : IRequestHandler<LoadQuoteBtcCommand, bool>
{
    private readonly ILiveOrderBookService liveOrderBookService;

    public LoadBtcQuoteCommandHandler(ILiveOrderBookService liveOrderBookService)
    {
        this.liveOrderBookService = liveOrderBookService;
    }

    public async Task<bool> Handle(LoadQuoteBtcCommand request, CancellationToken cancellationToken)
    {
        await liveOrderBookService.SearchCurrentBtcBookOffers();

        return true;
    }
}
