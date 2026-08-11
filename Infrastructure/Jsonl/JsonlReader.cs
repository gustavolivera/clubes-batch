using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Threading;
using clubes_batch.Application.Interfaces;
using clubes_batch.Application.Models.Input;

namespace clubes_batch.Infrastructure.Jsonl;

public class JsonlReader : IJsonlReader
{
    public async IAsyncEnumerable<ClubInputModel> ReadAsync(string filePath, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        using var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read, bufferSize: 4096, FileOptions.Asynchronous | FileOptions.SequentialScan);
        using var reader = new StreamReader(stream);

        string? line;
        while ((line = await reader.ReadLineAsync(cancellationToken)) != null)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                continue;
            }

            ClubInputModel? club = null;
            try
            {
                club = JsonSerializer.Deserialize<ClubInputModel>(line);
            }
            catch (JsonException)
            {
                // Ignora silenciosamente o JSON malformado e continua para o próximo registro.
                continue;
            }

            if (club != null)
            {
                yield return club;
            }
        }
    }
}
