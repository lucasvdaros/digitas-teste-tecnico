using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace Digitas.QuotesMetrics.Worker;

public class Function
{
    private readonly ILogger<Function> logger;

    public Function(ILogger<Function> logger)
    {
        this.logger = logger;
    }

    public async Task ReportQuotesMetrics([TimerTrigger("*/5 * * * * *")] TimerInfo timerInfo)
    {

    }
}
