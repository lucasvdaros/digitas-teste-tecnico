using Digitas.QuotesMetrics.Function.Domain.Interfaces.Services;
using Digitas.QuotesMetrics.Function.Domain.ValueObject;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace Digitas.QuotesMetrics.Function
{
    public class QuouteMetricFunction
    {
        private readonly ILogger logger;
        private readonly IBtcQuotesService btcQuoteService;
        private readonly IEthQuotesService ethQuoteService;

        public QuouteMetricFunction(ILoggerFactory loggerFactory, IBtcQuotesService btcQuoteService, IEthQuotesService ethQuoteService)
        {
            logger = loggerFactory.CreateLogger<QuouteMetricFunction>();
            this.btcQuoteService = btcQuoteService;
            this.ethQuoteService = ethQuoteService;
        }

        [Function("QuouteMetricFunction")]
        public async Task RunAsync([TimerTrigger("*/5 * * * * *")] MyInfo myTimer)
        {
            logger.LogInformation($"Quotes at: {DateTime.Now}");
            Metrics btcMetrics;

            btcMetrics = await btcQuoteService.GetBtcMetrics();
            logger.LogInformation($"Biggest BTC Price: {btcMetrics.BiggestUsdValue}");
            logger.LogInformation($"Smallest BTC Price: {btcMetrics.SmallestUsdValue}");
            logger.LogInformation($"Avg BTC Price: {btcMetrics.AvgUsdValue}");
            logger.LogInformation($"Avg BTC Price: {btcMetrics.AvgLastFiveMinutesUsdValue}");
            logger.LogInformation($"Avg BTC Price: {btcMetrics.AvgAmount}");


            btcMetrics = await ethQuoteService.GetEthMetrics();
            logger.LogInformation($"Biggest ETH Price: {btcMetrics.BiggestUsdValue}");
            logger.LogInformation($"Smallest ETH Price: {btcMetrics.SmallestUsdValue}");
            logger.LogInformation($"Avg ETH Price: {btcMetrics.AvgUsdValue}");
            logger.LogInformation($"Avg ETH Price: {btcMetrics.AvgLastFiveMinutesUsdValue}");
            logger.LogInformation($"Avg ETH Price: {btcMetrics.AvgAmount}");
        }
    }

    public class MyInfo
    {
        public MyScheduleStatus ScheduleStatus { get; set; }

        public bool IsPastDue { get; set; }
    }

    public class MyScheduleStatus
    {
        public DateTime Last { get; set; }

        public DateTime Next { get; set; }

        public DateTime LastUpdated { get; set; }
    }
}
