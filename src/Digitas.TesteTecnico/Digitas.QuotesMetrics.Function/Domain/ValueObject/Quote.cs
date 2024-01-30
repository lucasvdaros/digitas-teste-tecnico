namespace Digitas.QuotesMetrics.Function.Domain.ValueObject;

public class Quote
{
    public int UsdValue { get; set; }
    public decimal Amount { get; set; }

    public Quote(decimal amount, int usdValue)
    {
        Amount = amount;
        UsdValue = usdValue;
    }
}
