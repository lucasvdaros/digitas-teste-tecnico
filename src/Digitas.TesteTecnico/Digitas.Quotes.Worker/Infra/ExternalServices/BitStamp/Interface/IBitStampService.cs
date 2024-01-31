namespace Digitas.Quotes.Worker.Infra.ExternalServices.BitStamp.Interface;

public interface IBitStampService
{
    Task<bool> IsConnectToBitStampWebSocket();    
    Task<string> SubscribeChannel(byte[] buffer);
    Task<string> ReceivedMessage();
    bool IsConnectionStillOpen();
}
