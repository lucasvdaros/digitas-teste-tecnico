using Digitas.Quotes.Worker.Infra.ExternalServices.BitStamp.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Net.WebSockets;
using System.Text;

namespace Digitas.Quotes.Worker.Infra.ExternalServices.BitStamp;

public class BitStampService : IBitStampService
{
    private readonly ILogger<BitStampService> logger;
    private readonly string urlBitStamp;
    private readonly ClientWebSocket bitStampWs;

    public BitStampService(IConfiguration configuration, ILogger<BitStampService> logger)
    {
        this.logger = logger;

        urlBitStamp = configuration.GetValue<string>("urlBitStampWebSocket")!;
        bitStampWs = new ClientWebSocket();
    }    

    public async Task<bool> IsConnectToBitStampWebSocket()
    {
        try
        {
            if(bitStampWs.State != WebSocketState.Open)
            {
                var uri = new Uri(urlBitStamp);

                await bitStampWs.ConnectAsync(uri, CancellationToken.None);

                logger.LogInformation($"Connection State: {bitStampWs.State}\n");

                return bitStampWs.State == WebSocketState.Open;
            }

            return true;            
        }
        catch (Exception ex)
        {
            logger.LogError($"Fail to open connection. Problem: {ex.Message}");

            bitStampWs.Abort();
            return false;
        }
    }

    public async Task<string> SubscribeChannel(byte[] buffer)
    {
        string receivedMessage = string.Empty;

        try
        {
            await bitStampWs.SendAsync(buffer: buffer,
                                  messageType: WebSocketMessageType.Binary,
                                  endOfMessage: true,
                                  CancellationToken.None);

            if (bitStampWs.State == WebSocketState.Open)
            {
                byte[] bufferRespose = new byte[100];

                WebSocketReceiveResult result = await bitStampWs.ReceiveAsync(new ArraySegment<byte>(bufferRespose), CancellationToken.None);

                if (result.MessageType == WebSocketMessageType.Text)
                {
                    receivedMessage = Encoding.UTF8.GetString(bufferRespose, 0, result.Count);
                }
            }

            return receivedMessage;
        }
        catch (Exception ex)
        {
            logger.LogError($"Connection State is not open. Problem: {ex.Message}");
            return string.Empty;
        }
    }

    public bool IsConnectionStillOpen() => bitStampWs.State == WebSocketState.Open;

    public async Task<string> ReceivedMessage()
    {
        byte[] buffer = new byte[1024 * 4];
        StringBuilder receivedMessage = new(string.Empty);

        try
        {
            WebSocketReceiveResult result = await bitStampWs.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

            do
            {
                receivedMessage.Append(Encoding.UTF8.GetString(buffer, 0, result.Count));
                result = await bitStampWs.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

            } while (!result.EndOfMessage);

            //logger.LogInformation(receivedMessage.ToString());

            return receivedMessage.ToString();
        }
        catch (Exception ex)
        {
            bitStampWs.Abort();
            logger.LogError($"Connection State is not open. Problem: {ex.Message}");
            return receivedMessage.ToString();
        }
    }
}
