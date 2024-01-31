namespace Digitas.QuotesMetrics.Function.Domain.Entities;

public class EthBid
{
    public int EthBidId { get; set; }
    public long Microtimestamp { get; set; }
    public decimal UsdValue { get; set; }
    public decimal Amount { get; set; }
}
