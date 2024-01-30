using Digitas.QuotesMetrics.Function.Domain.Entities;
using Digitas.QuotesMetrics.Function.Domain.Interfaces.Repositories;
using Digitas.QuotesMetrics.Function.Domain.Interfaces.Services;
using Digitas.QuotesMetrics.Function.Domain.ValueObject;

namespace Digitas.QuotesMetrics.Function.Domain.Services;

public class BtcQuotesService : IBtcQuotesService
{
    private readonly IBtcAskRepository askRepository;
    private const int FIVE_SECONDS_ON_MICROTIME = 500000;

    public BtcQuotesService(IBtcAskRepository askRepository)
    {
        this.askRepository = askRepository;        
    }

    public async Task<Metrics> GetBtcMetrics()
    {
        var btcAsks = await askRepository.GetBtcQuotesFromLargestToSmallestMicrotimestamp();

        var biggestUsdValue = BiggestUsdValue(btcAsks);
        var smallestUsdValue = SmallestUsdValue(btcAsks);
        var avgUsdValue = AvgUsdValue(btcAsks);
        var avgLastFiveMinutesUsdValue = AvgLastFiveMinutesUsdValue(btcAsks);
        var avgAmount = AvgAmount(btcAsks);

        return new Metrics()
        {
            BiggestUsdValue = biggestUsdValue,
            SmallestUsdValue = smallestUsdValue,
            AvgUsdValue = avgUsdValue,
            AvgLastFiveMinutesUsdValue = avgLastFiveMinutesUsdValue,
            AvgAmount = avgAmount
        };
    }

    private static int BiggestUsdValue(List<BtcAsk> ethAsks) => ethAsks.Max(eth => eth.UsdValue);
    private static int SmallestUsdValue(List<BtcAsk> ethAsks) => ethAsks.Min(eth => eth.UsdValue);
    private static decimal AvgUsdValue(List<BtcAsk> ethAsks)
    {
        var totalUsdValue = ethAsks.Sum(eth => eth.UsdValue);

        return ethAsks.Count != 0 ? totalUsdValue / ethAsks.Count : 0;
    }
    private static decimal AvgLastFiveMinutesUsdValue(List<BtcAsk> ethAsks)
    {
        var maxMicroTimeStamp = ethAsks.Max(eth => eth.Microtimestamp);

        var lastFiveMinutesAsks = ethAsks.Where(eths => maxMicroTimeStamp - eths.Microtimestamp <= FIVE_SECONDS_ON_MICROTIME).ToList();
        var maxLastFiveMinutesAsks = lastFiveMinutesAsks.Sum(eth => eth.UsdValue);

        return lastFiveMinutesAsks.Count != 0 ? maxLastFiveMinutesAsks / lastFiveMinutesAsks.Count : 0;
    }
    private static decimal AvgAmount(List<BtcAsk> ethAsks)
    {
        var totalAmount = ethAsks.Sum(eth => eth.Amount);

        return ethAsks.Count != 0 ? totalAmount / ethAsks.Count : 0;
    }
}
