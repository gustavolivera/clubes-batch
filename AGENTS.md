# Guia de Desenvolvimento (AGENTS.md)

## Contexto do Projeto
Este projeto é uma aplicação Console desenvolvida em **.NET 9**, focada no processamento em lote (Batch). O objetivo principal é receber um arquivo de entrada no formato **JSONL** (potencialmente muito grande) e processar esses dados para gerar dois arquivos de saída:
- `players.csv`
- `clubs.csv`

## Arquitetura
Para manter a organização sem adicionar complexidade desnecessária, o projeto adota princípios da **Clean Architecture** através de uma estrutura de pastas dentro de um **único projeto principal**, acompanhado de um **projeto de testes** separado (`clubes-batch.Tests`). 

As responsabilidades estão divididas na seguinte estrutura de pastas:

- **Application**: Contém os modelos de dados (Input/Output), as regras de negócio e a orquestração da transformação dos dados.
- **Infrastructure**: Responsável por detalhes técnicos externos, como a leitura (streaming) do arquivo JSONL e a gravação dos arquivos `.csv`.
- **Program.cs**: Atua como o ponto de entrada da aplicação, orquestrando as dependências e o início do fluxo em lote.

## Modelagem e Regras de Negócio
As seguintes decisões arquiteturais e regras de negócio foram estabelecidas:
- **Sem Camada de Domínio**: O projeto foca no fluxo e transformação de dados em lote. Modelos não possuem comportamento de domínio.
- **Modelos de Entrada (Input)**: Representam a estrutura do JSONL. Permitem campos nulos ou ausentes e armazenam as datas como strings para evitar falhas na desserialização.
- **Modelos de Saída (Output)**: Representam as linhas exatas do CSV (`ClubCsvRecord` e `PlayerCsvRecord`). Possuem apenas propriedades `string` não-anuláveis. Qualquer valor ausente, nulo ou inválido será convertido em `string.Empty` na transformação.
- **Processamento Incremental**: Para suportar arquivos massivos, o JSONL deve ser lido e gravado sequencialmente (streaming), sem carregar o arquivo inteiro na memória. Registros malformados devem ser ignorados individualmente sem quebrar o processamento.
- **Filtro de Campeonatos**: Apenas clubes dos campeonatos "SERIE A" ou "SERIE B" serão processados. Outros devem ser ignorados juntamente com seus jogadores.
- **Regras Específicas de Transformação**:
  - `club_id`: Cada jogador exportado no `players.csv` recebe o id do seu respectivo clube.
  - `colors`: A lista de cores deve ser formatada como uma única string unida por `|` (ex: `preto|branco`).
  - `Datas`: Devem ser validadas durante o processamento. Se válidas, gravadas como `yyyy-MM-dd`. Se inválidas, o campo fica vazio mas o registro continua válido.

## Próximos Passos (A Definir)
Nenhuma decisão de biblioteca técnica específica foi tomada. As seguintes partes **ainda serão implementadas**:
- Lógica e bibliotecas exatas para leitura do JSONL (streaming).
- Lógica de transformação de dados e validações (Application).
- Lógica e bibliotecas exatas para gravação contínua (append) nos arquivos `players.csv` e `clubs.csv`.
