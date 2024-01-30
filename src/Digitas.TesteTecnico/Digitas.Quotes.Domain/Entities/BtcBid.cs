namespace Digitas.Quotes.Domain.Entities;

public class BtcBid
{
    public int BtcBidId { get; set; }
    public long Microtimestamp { get; set; }
    public int UsdValue { get; set; }
    public decimal Amount { get; set; }
}
