namespace Digitas.Quotes.Domain.ValueObjects;

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
