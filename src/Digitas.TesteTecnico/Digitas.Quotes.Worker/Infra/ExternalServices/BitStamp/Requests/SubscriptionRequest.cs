using System.Text.Json.Serialization;

namespace Digitas.Quotes.Worker.Infra.ExternalServices.BitStamp.Requests;

record SubscriptionRequest([property: JsonPropertyName("event")] string Event,
                           [property: JsonPropertyName("data")] SubscriptionData Data);
