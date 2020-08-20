using System.Text.Json.Serialization;

namespace Godwit.HandleUserInsertedEvent.Model {
    public class SessionVariables {
        [JsonPropertyName("x-hasura-role")] public string HasuraRole { get; set; }
    }
}