namespace Digitas.Quotes.Worker.Domain.Entities;

public class BtcAsk
{
    public int BtcAskId { get; set; }
    public long Microtimestamp { get; set; }
    public int UsdValue { get; set; }
    public decimal Amount { get; set; }
}
