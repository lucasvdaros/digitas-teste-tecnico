namespace Digitas.Quotes.Worker.Domain.Interfaces.Services;

public interface IBtcService
{
    Task PersistCurrentBtcBookOffers(string channel);
}
