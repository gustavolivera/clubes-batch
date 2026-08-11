using System;
using System.Collections.Generic;
using System.Globalization;
using clubes_batch.Application.Models.Input;
using clubes_batch.Application.Models.Output;

namespace clubes_batch.Application;

public class ClubProcessor
{
    public (ClubCsvRecord? Club, List<PlayerCsvRecord> Players) Process(ClubInputModel input)
    {
        // Regra de Filtro: Apenas SERIE A e SERIE B
        if (input.Championship != "SERIE A" && input.Championship != "SERIE B")
        {
            return (null, new List<PlayerCsvRecord>());
        }

        string clubId = input.ClubId ?? string.Empty;

        // Regra de Cores: Join com |
        string colors = input.Colors != null && input.Colors.Length > 0 
            ? string.Join("|", input.Colors) 
            : string.Empty;

        // Processamento do Clube
        var clubRecord = new ClubCsvRecord(
            ClubId: clubId,
            Name: input.Name ?? string.Empty,
            Championship: input.Championship ?? string.Empty,
            FoundingDate: FormatDate(input.FoundingDate),
            City: input.City ?? string.Empty,
            State: input.State ?? string.Empty,
            Country: input.Country ?? string.Empty,
            Stadium: input.Stadium ?? string.Empty,
            President: input.President ?? string.Empty,
            Nickname: input.Nickname ?? string.Empty,
            Colors: colors
        );

        // Processamento dos Jogadores
        var playerRecords = new List<PlayerCsvRecord>();
        if (input.Players != null)
        {
            foreach (var p in input.Players)
            {
                playerRecords.Add(new PlayerCsvRecord(
                    ClubId: clubId,
                    PlayerId: p.PlayerId ?? string.Empty,
                    Name: p.Name ?? string.Empty,
                    Age: p.Age?.ToString() ?? string.Empty,
                    Goals: p.Goals?.ToString() ?? string.Empty,
                    DebutDate: FormatDate(p.DebutDate),
                    Position: p.Position ?? string.Empty,
                    ShirtNumber: p.ShirtNumber?.ToString() ?? string.Empty
                ));
            }
        }

        return (clubRecord, playerRecords);
    }

    private static string FormatDate(string? dateString)
    {
        if (string.IsNullOrWhiteSpace(dateString))
            return string.Empty;

        return DateOnly.TryParseExact(
            dateString,
            "yyyy-MM-dd",
            CultureInfo.InvariantCulture,
            DateTimeStyles.None,
            out var date)
            ? date.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)
            : string.Empty;
    }
}
