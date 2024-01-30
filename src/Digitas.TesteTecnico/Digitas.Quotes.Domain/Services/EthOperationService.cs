using Digitas.Quotes.Domain.Enums;
using Digitas.Quotes.Domain.Interfaces.Repositories;
using Digitas.Quotes.Domain.Interfaces.Services;
using Digitas.Quotes.Domain.ValueObjects;

namespace Digitas.Quotes.Domain.Services;

public class EthOperationService : IEthOperationService
{
    private readonly IQuoteService quouteService;
    private readonly IEthAskRepository ethAskRepository;
    private readonly IEthBidRepository ethBidRepository;

    public EthOperationService(IQuoteService quouteService, IEthAskRepository ethAskRepository, IEthBidRepository ethBidRepository)
    {
        this.quouteService = quouteService;
        this.ethAskRepository = ethAskRepository;
        this.ethBidRepository = ethBidRepository;
    }

    public async Task<Simulation> CalculateEthBuyOperation(OperationChoice operation, decimal amount)
    {
        var lastsEthQuotes = await ethAskRepository.GetLastUpdatedQuoutes();
        var quotes = new List<Quote>();

        foreach (var ethQuote in lastsEthQuotes)
        {
            var quote = new Quote(ethQuote.Amount, ethQuote.UsdValue);
            quotes.Add(quote);
        }

        return CalculateEthOperation(quotes, CoinOperation.BuyETH, operation, amount);        
    }

    public async Task<Simulation> CalculateEthSellOperation(OperationChoice operation, decimal amount)
    {
        var lastsEthQuotes = await ethBidRepository.GetLastUpdatedQuoutes();
        var quotes = new List<Quote>();

        foreach (var ethQuote in lastsEthQuotes)
        {
            var quote = new Quote(ethQuote.Amount, ethQuote.UsdValue);
            quotes.Add(quote);
        }

        return CalculateEthOperation(quotes, CoinOperation.BuyETH, operation, amount);
    }

    private Simulation CalculateEthOperation(List<Quote> quotes, CoinOperation coinOperation, OperationChoice operation, decimal amount)
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
            Coin = Coin.ETH
        };
    }
}
