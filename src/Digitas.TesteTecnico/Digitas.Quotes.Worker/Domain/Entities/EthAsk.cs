namespace Digitas.Quotes.Worker.Domain.Entities;

public class EthAsk
{
    public int EthAskId { get; set; }
    public long Microtimestamp { get; set; }
    public decimal UsdValue { get; set; }
    public decimal Amount { get; set; }
}
