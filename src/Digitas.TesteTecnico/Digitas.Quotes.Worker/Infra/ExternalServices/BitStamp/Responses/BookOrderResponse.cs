using System.Text.Json.Serialization;

namespace Digitas.Quotes.Worker.Infra.ExternalServices.BitStamp.Responses;

public class BookOrderResponse
{
    [JsonPropertyName("event")]
    public required string Event { get; set; }

    [JsonPropertyName("channel")]
    public required string Channel { get; set; }

    [JsonPropertyName("data")]
    public BookOrderDataResponse? Data { get; set; }

    public bool IsSubscriptionSucceeded() => Event.Equals("bts:subscription_succeeded");
}
