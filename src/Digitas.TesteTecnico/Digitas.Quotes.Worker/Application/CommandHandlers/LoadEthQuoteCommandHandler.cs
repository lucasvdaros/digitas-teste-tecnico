using Digitas.Quotes.Worker.Application.Commands;
using Digitas.Quotes.Worker.Domain.Interfaces.Services;
using MediatR;

namespace Digitas.Quotes.Worker.Application.CommandHandlers;

public class LoadEthQuoteCommandHandler : IRequestHandler<LoadQuoteEthCommand, bool>
{
    private readonly ILiveOrderBookService liveOrderBookService;
    private readonly IEthService ethService;
    private readonly string btcChannel = "order_book_ethusd";

    public LoadEthQuoteCommandHandler(ILiveOrderBookService liveOrderBookService, IEthService ethService)
    {
        this.liveOrderBookService = liveOrderBookService;
        this.ethService = ethService;
    }

    public async Task<bool> Handle(LoadQuoteEthCommand request, CancellationToken cancellationToken)
    {
        if (await liveOrderBookService.IsSubscribedChannel(btcChannel))
        {
            await ethService.PersistCurrentEthBookOffers(btcChannel);
        }

        return true;
    }
}
