using Digitas.Quotes.Worker.Infra.ExternalServices.BitStamp.Interface;
using System.Text.Json;

namespace Digitas.Quotes.Worker.Infra.ExternalServices.BitStamp.Responses;

public class SubscriptionResponse : IBitStampFactory
{
    public BookOrderResponse FormatBitStampResponse(string receivedMessage) =>
        JsonSerializer.Deserialize<BookOrderResponse>(receivedMessage)!;
}
