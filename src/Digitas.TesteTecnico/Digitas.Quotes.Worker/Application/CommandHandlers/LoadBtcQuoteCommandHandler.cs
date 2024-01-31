using Digitas.Quotes.Worker.Application.Commands;
using Digitas.Quotes.Worker.Domain.Interfaces.Services;
using MediatR;

namespace Digitas.Quotes.Worker.Application.CommandHandlers;

public class LoadBtcQuoteCommandHandler : IRequestHandler<LoadQuoteBtcCommand, bool>
{
    private readonly ILiveOrderBookService liveOrderBookService;
    private readonly IBtcService btcService;
    private readonly string btcChannel = "order_book_btcusd";

    public LoadBtcQuoteCommandHandler(ILiveOrderBookService liveOrderBookService, IBtcService btcService)
    {
        this.liveOrderBookService = liveOrderBookService;
        this.btcService = btcService;
    }

    public async Task<bool> Handle(LoadQuoteBtcCommand request, CancellationToken cancellationToken)
    {
        if (await liveOrderBookService.IsSubscribedChannel(btcChannel))
        {
            await btcService.PersistCurrentBtcBookOffers(btcChannel);
        }

        return true;
    }
}
