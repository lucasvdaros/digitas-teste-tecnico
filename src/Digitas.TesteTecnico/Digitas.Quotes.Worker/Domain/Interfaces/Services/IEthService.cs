namespace Digitas.Quotes.Worker.Domain.Interfaces.Services;

public interface IEthService
{
    Task PersistCurrentEthBookOffers(string channel);
}
