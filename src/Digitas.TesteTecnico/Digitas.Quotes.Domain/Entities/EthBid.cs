namespace Digitas.Quotes.Domain.Entities;

public class EthBid
{
    public int EthBidId { get; set; }
    public long Microtimestamp { get; set; }
    public int UsdValue { get; set; }
    public decimal Amount { get; set; }
}
