using System.Text.Json.Serialization;

namespace Digitas.Quotes.Worker.Infra.ExternalServices.BitStamp.Responses;

public class BookOrderDataResponse
{
    [JsonPropertyName("timestamp")]
    public string? Timestamp { get; set; }

    [JsonPropertyName("microtimestamp")]
    public string? Microtimestamp { get; set; }

    [JsonPropertyName("bids")]
    public List<List<string>>? Bids { get; set; }

    [JsonPropertyName("asks")]
    public List<List<string>>? Asks { get; set; }
}
