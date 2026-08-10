using System.Text.Json.Serialization;

namespace clubes_batch.Application.Models.Input;

public record ClubInputModel(
    [property: JsonPropertyName("club_id")] string? ClubId,
    [property: JsonPropertyName("name")] string? Name,
    [property: JsonPropertyName("championship")] string? Championship,
    [property: JsonPropertyName("founding_date")] string? FoundingDate,
    [property: JsonPropertyName("city")] string? City,
    [property: JsonPropertyName("state")] string? State,
    [property: JsonPropertyName("country")] string? Country,
    [property: JsonPropertyName("stadium")] string? Stadium,
    [property: JsonPropertyName("president")] string? President,
    [property: JsonPropertyName("nickname")] string? Nickname,
    [property: JsonPropertyName("colors")] string[]? Colors,
    [property: JsonPropertyName("players")] PlayerInputModel[]? Players
);
