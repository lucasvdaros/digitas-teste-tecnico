using Digitas.Quotes.Worker.Domain.Interfaces.Services;
using System.Text;
using System.Text.Json;
using Digitas.Quotes.Worker.Infra.ExternalServices.BitStamp.Interface;
using Digitas.Quotes.Worker.Infra.ExternalServices.BitStamp.Responses;
using Digitas.Quotes.Worker.Infra.ExternalServices.BitStamp.Requests;

namespace Digitas.Quotes.Worker.Domain.Services;

public class LiveOrderBookService : ILiveOrderBookService
{
    private readonly IBitStampService bitStamp;        
    
    public LiveOrderBookService(IBitStampService bitStamp)
    {
        this.bitStamp = bitStamp;
    }

    public async Task<bool> ConnectToBitStampWebSocket() => await bitStamp.IsConnectToBitStampWebSocket();

    public async Task<bool> IsSubscribedChannel(string channel)
    {
        var buffer = FormatSubscribePayload(channel);

        var subscriptionResponse = await bitStamp.SubscribeChannel(buffer);

        var subscriptionStatus = new SubscriptionResponse().FormatBitStampResponse(subscriptionResponse);

        Console.WriteLine($"channel:{subscriptionStatus.Channel}");

        return subscriptionStatus.IsSubscriptionSucceeded();
    }    
    
    private static byte[] FormatSubscribePayload(string channel)
    {
        var data = new SubscriptionData(channel);
        var subscription = new SubscriptionRequest("bts:subscribe", data);
        var subscribeMsg = JsonSerializer.Serialize(subscription);

        return Encoding.UTF8.GetBytes(subscribeMsg);
    }
}
