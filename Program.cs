using System;
using System.IO;
using Microsoft.Extensions.DependencyInjection;
using clubes_batch.Application;
using clubes_batch.Application.Interfaces;
using clubes_batch.Infrastructure.Csv;
using clubes_batch.Infrastructure.Jsonl;

if (args.Length == 0)
{
    Console.WriteLine("Uso esperado: dotnet run -- <caminho_do_arquivo_jsonl>");
    return;
}

var filePath = args[0];

if (!File.Exists(filePath))
{
    Console.WriteLine($"Erro: O arquivo '{filePath}' não foi encontrado.");
    return;
}

// Configuração da Injeção de Dependência
var serviceCollection = new ServiceCollection();

// Registro dos serviços
serviceCollection.AddScoped<IJsonlReader, JsonlReader>();
serviceCollection.AddScoped<ClubProcessor>();

// Build do container
var serviceProvider = serviceCollection.BuildServiceProvider();

// Orquestração (Application Pipeline)
var reader = serviceProvider.GetRequiredService<IJsonlReader>();
var processor = serviceProvider.GetRequiredService<ClubProcessor>();

// Instancia e garante o descarte correto do CsvWriter ao final do escopo (IDisposable)
using var writer = new CsvWriter("clubs.csv", "players.csv");

// Processamento Incremental em Streaming
await foreach (var clubInput in reader.ReadAsync(filePath))
{
    var (clubRecord, playerRecords) = processor.Process(clubInput);
    
    // Se o clubRecord for nulo, significa que foi descartado pelo filtro de campeonatos.
    if (clubRecord != null)
    {
        writer.WriteClub(clubRecord);
        
        // Escreve os jogadores, caso existam.
        // Se a lista estiver vazia, o método de escrever iterará 0 vezes e nada será escrito.
        if (playerRecords != null)
        {
            writer.WritePlayers(playerRecords);
        }
    }
}

Console.WriteLine("Processamento em lote finalizado com sucesso.");
