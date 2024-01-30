using System.Text.Json.Serialization;

namespace Digitas.Quotes.Worker.Infra.ExternalServices.BitStamp.Requests;

record SubscriptionData([property: JsonPropertyName("channel")] string Channel);
