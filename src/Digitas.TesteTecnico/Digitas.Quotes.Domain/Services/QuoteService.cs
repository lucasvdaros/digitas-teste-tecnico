using Digitas.Quotes.Domain.Interfaces.Services;
using Digitas.Quotes.Domain.ValueObjects;

namespace Digitas.Quotes.Domain.Services;

public class QuoteService : IQuoteService
{
    public decimal CalculateFinalPriceOffer(List<Quote> selectedQuotes, decimal amountRequested)
    {
        decimal finalPrice = 0;
        decimal amountExceptLastOne = 0;
        foreach (var selectedQuote in selectedQuotes)
        {
            if (selectedQuote.Equals(selectedQuotes[^1])) //last element on list
            {
                var amount = amountRequested - amountExceptLastOne;
                finalPrice += selectedQuote.UsdValue * amount;
            }
            else
            {
                finalPrice += selectedQuote.UsdValue * selectedQuote.Amount;
                amountExceptLastOne += selectedQuote.Amount;
            }
        }

        return finalPrice;
    }

    public List<Quote> SelectQuoutesByAmountRequested(IEnumerable<Quote> lastsQuotes, decimal amountRequested)
    {
        decimal acuumulatted = 0;
        var selectedQuotes = new List<Quote>();

        foreach (var quote in lastsQuotes)
        {
            var selectedQuote = new Quote(quote.Amount, quote.UsdValue);
            selectedQuotes.Add(selectedQuote);

            acuumulatted += quote.Amount;
            if (amountRequested <= acuumulatted) break;
        }

        return selectedQuotes;
    }
}
