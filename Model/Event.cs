﻿using System.Text.Json.Serialization;

namespace Godwit.HandleUserInsertedEvent.Model {
    public class Event {
        [JsonPropertyName("session_variables")]
        public SessionVariables SessionVariables { get; set; }

        [JsonPropertyName("op")] public string Operation { get; set; }

        [JsonPropertyName("data")] public Data Data { get; set; }
    }
}