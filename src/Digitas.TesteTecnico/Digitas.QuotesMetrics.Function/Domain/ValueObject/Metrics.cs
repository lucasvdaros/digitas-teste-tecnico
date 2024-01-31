namespace Digitas.QuotesMetrics.Function.Domain.ValueObject;

public class Metrics
{
    public decimal BiggestUsdValue { get; set; }
    public decimal SmallestUsdValue { get; set; }
    public decimal AvgUsdValue { get; set; }
    public decimal AvgLastFiveMinutesUsdValue { get; set; }
    public decimal AvgAmount { get; set; }
}
