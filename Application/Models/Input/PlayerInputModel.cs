using System.Text.Json.Serialization;

namespace clubes_batch.Application.Models.Input;

public record PlayerInputModel(
    [property: JsonPropertyName("player_id")] string? PlayerId,
    [property: JsonPropertyName("name")] string? Name,
    [property: JsonPropertyName("age")] int? Age,
    [property: JsonPropertyName("goals")] int? Goals,
    [property: JsonPropertyName("debut_date")] string? DebutDate,
    [property: JsonPropertyName("position")] string? Position,
    [property: JsonPropertyName("shirt_number")] int? ShirtNumber
);
