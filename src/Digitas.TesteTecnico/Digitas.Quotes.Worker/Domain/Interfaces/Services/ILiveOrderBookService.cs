namespace Digitas.Quotes.Worker.Domain.Interfaces.Services;

public interface ILiveOrderBookService
{
    Task<bool> ConnectToBitStampWebSocket();
    Task<bool> IsSubscribedChannel(string channel);
}
