using Digitas.Quotes.Domain.Entities;
using Digitas.Quotes.Domain.ValueObjects;

namespace Digitas.Quotes.Domain.Interfaces.Services;

public interface IQuoteService
{
    decimal CalculateFinalPriceOffer(List<Quote> selectedQuotes, decimal amountRequested);
    List<Quote> SelectQuoutesByAmountRequested(IEnumerable<Quote> lastsQuotes, decimal amountRequested);
}
