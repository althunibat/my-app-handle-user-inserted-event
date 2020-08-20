using System.Text.Json.Serialization;

namespace Godwit.HandleUserInsertedEvent.Model {
    public class SessionVariables {
        [JsonPropertyName("x-hasura-role")] public string Role { get; set; }
        [JsonPropertyName("x-hasura-user-id")] public string UserId { get; set; }
    }
}