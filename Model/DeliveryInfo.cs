using System.Text.Json.Serialization;

namespace Godwit.HandleUserInsertedEvent.Model {
    public class DeliveryInfo {
        [JsonPropertyName("current_retry")] public int CurrentRetry { get; set; }

        [JsonPropertyName("max_retries")] public int MaxRetries { get; set; }
    }
}