using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using clubes_batch.Application.Models.Output;

namespace clubes_batch.Infrastructure.Csv;

public class CsvWriter : IDisposable
{
    private readonly StreamWriter _clubsWriter;
    private readonly StreamWriter _playersWriter;

    public CsvWriter(string clubsFilePath, string playersFilePath)
    {
        var utf8 = new UTF8Encoding(true);
        
        // Abre os arquivos e os mantém abertos
        _clubsWriter = new StreamWriter(clubsFilePath, append: false, encoding: utf8);
        _playersWriter = new StreamWriter(playersFilePath, append: false, encoding: utf8);

        // Escreve os cabeçalhos exigidos
        _clubsWriter.WriteLine("Id do Clube,Nome,Campeonato,Data de Fundação,Cidade,Estado,País,Estádio,Presidente,Apelido,Cores");
        _playersWriter.WriteLine("Id do Clube,Id do Jogador,Nome,Idade,Gols,Data de Estreia,Posição,Número da Camisa");
    }

    public void WriteClub(ClubCsvRecord club)
    {
        var line = string.Join(",",
            Escape(club.ClubId),
            Escape(club.Name),
            Escape(club.Championship),
            Escape(club.FoundingDate),
            Escape(club.City),
            Escape(club.State),
            Escape(club.Country),
            Escape(club.Stadium),
            Escape(club.President),
            Escape(club.Nickname),
            Escape(club.Colors)
        );
        _clubsWriter.WriteLine(line);
    }

    public void WritePlayers(IEnumerable<PlayerCsvRecord> players)
    {
        foreach (var player in players)
        {
            var line = string.Join(",",
                Escape(player.ClubId),
                Escape(player.PlayerId),
                Escape(player.Name),
                Escape(player.Age),
                Escape(player.Goals),
                Escape(player.DebutDate),
                Escape(player.Position),
                Escape(player.ShirtNumber)
            );
            _playersWriter.WriteLine(line);
        }
    }

    /// <summary>
    /// Aplica o escaping de acordo com a RFC 4180.
    /// </summary>
    private static string Escape(string field)
    {
        if (string.IsNullOrEmpty(field))
            return string.Empty;

        // Se contiver vírgula, aspas ou quebras de linha, deve ser envolvido em aspas.
        if (field.Contains(',') || field.Contains('"') || field.Contains('\r') || field.Contains('\n'))
        {
            // Substitui aspas duplas internas por duas aspas duplas consecutivas ("")
            return $"\"{field.Replace("\"", "\"\"")}\"";
        }

        return field;
    }

    public void Dispose()
    {
        // Importante: Dispose fecha as streams e libera os arquivos
        _clubsWriter.Dispose();
        _playersWriter.Dispose();
    }
}
