namespace Digitas.Quotes.Worker.Domain.Interfaces.Services;

public interface ILiveOrderBookService
{
    Task SearchCurrentBtcBookOffers();
}
