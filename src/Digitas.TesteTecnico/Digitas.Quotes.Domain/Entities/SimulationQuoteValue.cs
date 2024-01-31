using Digitas.Quotes.Domain.ValueObjects;

namespace Digitas.Quotes.Domain.Entities;

public class SimulationQuoteValue
{
    public int SimulationQuoteValueId { get; set; }
    public int SimulationQuoteId { get; set; }
    public decimal Amount { get; set; }
    public decimal UsdValue { get; set; }

    public virtual SimulationQuote? SimulationQuote { get; set; }

    public SimulationQuoteValue(int simulationQuoteId, decimal amount, decimal usdValue)
    {
        SimulationQuoteId = simulationQuoteId;
        Amount = amount;
        UsdValue = usdValue;
    }

    public static List<SimulationQuoteValue> SimulationValues(List<Quote> quotes, int simulationQuoteId)
    {
        var simulationQuouteValues = new List<SimulationQuoteValue>();

        foreach(var quote in quotes)
        {
            var newQuouteValues = new SimulationQuoteValue(simulationQuoteId, quote.Amount, quote.UsdValue);
            simulationQuouteValues.Add(newQuouteValues);
        }

        return simulationQuouteValues;
    }
}
