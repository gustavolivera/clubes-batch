using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using clubes_batch.Application.Models.Input;

namespace clubes_batch.Application.Interfaces;

public interface IJsonlReader
{
    IAsyncEnumerable<ClubInputModel> ReadAsync(string filePath, CancellationToken cancellationToken = default);
}
