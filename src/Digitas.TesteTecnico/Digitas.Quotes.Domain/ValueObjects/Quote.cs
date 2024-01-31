namespace Digitas.Quotes.Domain.ValueObjects;

public class Quote
{
    public decimal UsdValue { get; set; }
    public decimal Amount { get; set; }

    public Quote(decimal amount, decimal usdValue)
    {
        Amount = amount;
        UsdValue = usdValue;
    }
}
