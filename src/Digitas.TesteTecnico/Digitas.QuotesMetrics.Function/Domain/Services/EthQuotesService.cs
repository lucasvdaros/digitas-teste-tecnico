using Digitas.QuotesMetrics.Function.Domain.Entities;
using Digitas.QuotesMetrics.Function.Domain.Interfaces.Repositories;
using Digitas.QuotesMetrics.Function.Domain.Interfaces.Services;
using Digitas.QuotesMetrics.Function.Domain.ValueObject;

namespace Digitas.QuotesMetrics.Function.Domain.Services;

public class EthQuotesService : IEthQuotesService
{
    private readonly IEthAskRepository askRepository;    
    private const int FIVE_SECONDS_ON_MICROTIME = 500000;

    public EthQuotesService(IEthAskRepository askRepository)
    {
        this.askRepository = askRepository;        
    }

    public async Task<Metrics> GetEthMetrics()
    {
        var ethAsks = await askRepository.GetEthQuotesFromLargestToSmallestMicrotimestamp();        

        var biggestUsdValue = BiggestUsdValue(ethAsks);
        var smallestUsdValue = SmallestUsdValue(ethAsks);
        var avgUsdValue = AvgUsdValue(ethAsks);
        var avgLastFiveMinutesUsdValue = AvgLastFiveMinutesUsdValue(ethAsks);
        var avgAmount = AvgAmount(ethAsks);        

        return new Metrics()
        {
            BiggestUsdValue = biggestUsdValue,
            SmallestUsdValue = smallestUsdValue,
            AvgUsdValue = avgUsdValue,
            AvgLastFiveMinutesUsdValue = avgLastFiveMinutesUsdValue,
            AvgAmount = avgAmount
        };
    }

    private static int BiggestUsdValue(List<EthAsk> ethAsks) => ethAsks.Max(eth => eth.UsdValue);
    private static int SmallestUsdValue(List<EthAsk> ethAsks) => ethAsks.Min(eth => eth.UsdValue);
    private static decimal AvgUsdValue(List<EthAsk> ethAsks) 
    {
        var totalUsdValue = ethAsks.Sum(eth => eth.UsdValue);

        return ethAsks.Count != 0 ? totalUsdValue / ethAsks.Count : 0;
    } 
    private static decimal AvgLastFiveMinutesUsdValue(List<EthAsk> ethAsks)
    {
        var maxMicroTimeStamp = ethAsks.Max(eth => eth.Microtimestamp);

        var lastFiveMinutesAsks = ethAsks.Where(eths => maxMicroTimeStamp - eths.Microtimestamp <= FIVE_SECONDS_ON_MICROTIME).ToList();
        var maxLastFiveMinutesAsks = lastFiveMinutesAsks.Sum(eth => eth.UsdValue);

        return lastFiveMinutesAsks.Count != 0 ? maxLastFiveMinutesAsks / lastFiveMinutesAsks.Count : 0;
    }
    private static decimal AvgAmount(List<EthAsk> ethAsks)
    {
        var totalAmount = ethAsks.Sum(eth => eth.Amount);

        return ethAsks.Count != 0 ? totalAmount / ethAsks.Count : 0;
    }
}
