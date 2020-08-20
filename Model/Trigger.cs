using System.Text.Json.Serialization;

namespace Godwit.HandleUserInsertedEvent.Model {
    public class Trigger {
        [JsonPropertyName("name")] public string Name { get; set; }
    }
}