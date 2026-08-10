namespace clubes_batch.Application.Models.Output;

public record PlayerCsvRecord(
    string ClubId,
    string PlayerId,
    string Name,
    string Age,
    string Goals,
    string DebutDate,
    string Position,
    string ShirtNumber
);
