namespace clubes_batch.Application.Models.Output;

public record ClubCsvRecord(
    string ClubId,
    string Name,
    string Championship,
    string FoundingDate,
    string City,
    string State,
    string Country,
    string Stadium,
    string President,
    string Nickname,
    string Colors
);
