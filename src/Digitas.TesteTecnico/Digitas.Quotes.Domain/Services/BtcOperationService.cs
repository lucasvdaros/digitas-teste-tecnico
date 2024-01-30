using Digitas.Quotes.Domain.Enums;
using Digitas.Quotes.Domain.Interfaces.Repositories;
using Digitas.Quotes.Domain.Interfaces.Services;
using Digitas.Quotes.Domain.ValueObjects;

namespace Digitas.Quotes.Domain.Services;

public class BtcOperationService : IBtcOperationService
{
    private readonly IQuoteService quouteService;
    private readonly IBtcAskRepository btcAskRepository;
    private readonly IBtcBidRepository btcBidRepository;

    public BtcOperationService(IQuoteService quouteService,
                               IBtcAskRepository btcAskRepository,
                               IBtcBidRepository btcBidRepository)
    {
        this.quouteService = quouteService;
        this.btcAskRepository = btcAskRepository;
        this.btcBidRepository = btcBidRepository;
    }

    public async Task<Simulation> CalculateBtcBuyOperation(OperationChoice operation, decimal amount)
    {
        var lastsBtcQuotes = await btcAskRepository.GetLastUpdatedQuoutes();
        var quotes = new List<Quote>();

        foreach (var btcQuote in lastsBtcQuotes)
        {
            var quote = new Quote(btcQuote.Amount, btcQuote.UsdValue);
            quotes.Add(quote);
        }

        return CalculateBtcOperation(quotes, CoinOperation.BuyBTC, operation, amount);
    }

    public async Task<Simulation> CalculateBtcSellOperation(OperationChoice operation, decimal amount)
    {
        var lastsBtcQuotes = await btcBidRepository.GetLastUpdatedQuoutes();
        var quotes = new List<Quote>();

        foreach (var btcQuote in lastsBtcQuotes)
        {
            var quote = new Quote(btcQuote.Amount, btcQuote.UsdValue);
            quotes.Add(quote);
        }

        return CalculateBtcOperation(quotes, CoinOperation.SellBTC, operation, amount);
    }

    private Simulation CalculateBtcOperation(List<Quote> quotes, CoinOperation coinOperation, OperationChoice operation, decimal amount)
    {
        var selectedQuotes = quouteService.SelectQuoutesByAmountRequested(quotes, amount);
        var finalPrice = quouteService.CalculateFinalPriceOffer(selectedQuotes, amount);

        var hashSimuacao = Simulation.GenerateSHA256Hash(coinOperation.ToString() + amount + finalPrice + DateTime.Now);

        return new Simulation()
        {
            Operation = operation,
            Quotes = selectedQuotes,
            RequestAmount = amount,
            Result = finalPrice,
            hashIdentificacao = hashSimuacao,
            Coin = Coin.BTC
        };
    }
}
