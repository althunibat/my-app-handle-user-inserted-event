using System.Text.Json.Serialization;

namespace Godwit.HandleUserInsertedEvent.Model {
    public class Data {
        [JsonPropertyName("old")] public User OldValue { get; set; }

        [JsonPropertyName("new")] public User NewValue { get; set; }
    }
}