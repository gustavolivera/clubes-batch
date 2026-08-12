# Clubes Batch Processor

Aplicação Console desenvolvida em **.NET 9** para processamento de dados em lote (Batch) utilizando processamento incremental (*streaming*).

## Propósito

A aplicação lê arquivos no formato **JSONL** contendo dados de clubes de futebol e seus jogadores. Durante o processamento, aplica as regras de negócio e formatação necessárias e gera arquivos CSV contendo os dados dos clubes e jogadores dos campeonatos `SERIE A` e `SERIE B`.

O processamento é realizado de forma incremental, evitando carregar todo o arquivo em memória e permitindo trabalhar com arquivos de grande volume.

## Como Executar

O caminho do arquivo JSONL de entrada deve ser informado como parâmetro na execução da aplicação.

```bash
dotnet run -- <caminho_do_arquivo_jsonl>
```

**Exemplo:**

```bash
dotnet run -- "sample/sample_clubes.jsonl"
```

Ao final do processamento, os arquivos clubs.csv e players.csv serão gerados no diretório de execução da aplicação.

## Arquivos de Saída

**clubs.csv**

Contém um registro para cada clube pertencente à SERIE A ou SERIE B.

**players.csv**

Contém um registro para cada jogador dos clubes processados, incluindo o club_id do clube ao qual pertence.

Os arquivos são gerados em UTF-8, separados por vírgula, com cabeçalho e escaping de campos conforme RFC 4180.

## Processamento

O arquivo JSONL é processado de forma incremental, permitindo trabalhar com arquivos grandes sem carregar todo o conteúdo em memória.

Registros JSON inválidos são ignorados individualmente para que o processamento continue com os registros seguintes.

Durante a transformação:

* clubes fora da SERIE A e SERIE B são descartados;
* cores são unidas utilizando |;
* datas válidas são formatadas como yyyy-MM-dd;
* datas inválidas, campos nulos ou ausentes resultam em campos vazios;
* campos que contenham vírgulas, aspas ou quebras de linha são escapados conforme RFC 4180.