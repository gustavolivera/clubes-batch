using System;
using System.IO;
using Microsoft.Extensions.DependencyInjection;
using clubes_batch.Application.Interfaces;
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

// Registro dos serviços (Infrastructure)
serviceCollection.AddScoped<IJsonlReader, JsonlReader>();

// Build do container
var serviceProvider = serviceCollection.BuildServiceProvider();

// O processamento orquestrado (Application) começará aqui nas próximas etapas.
var reader = serviceProvider.GetRequiredService<IJsonlReader>();

await foreach (var club in reader.ReadAsync(filePath))
{
    Console.WriteLine(club);
}

Console.WriteLine("Processamento em lote finalizado com sucesso.");
