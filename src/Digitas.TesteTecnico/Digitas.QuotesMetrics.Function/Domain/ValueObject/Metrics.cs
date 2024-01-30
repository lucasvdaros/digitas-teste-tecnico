namespace Digitas.QuotesMetrics.Function.Domain.ValueObject;

public class Metrics
{
    public int BiggestUsdValue { get; set; }
    public int SmallestUsdValue { get; set; }
    public decimal AvgUsdValue { get; set; }
    public decimal AvgLastFiveMinutesUsdValue { get; set; }
    public decimal AvgAmount { get; set; }
}
