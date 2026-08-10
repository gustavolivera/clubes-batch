# Guia de Desenvolvimento (AGENTS.md)

## Contexto do Projeto
Este projeto é uma aplicação Console desenvolvida em **.NET 9**, focada no processamento em lote (Batch). O objetivo principal é receber um arquivo de entrada no formato **JSONL** (potencialmente muito grande) e processar esses dados para gerar dois arquivos de saída:
- `players.csv`
- `clubs.csv`

## Arquitetura
Para manter a organização sem adicionar complexidade desnecessária, o projeto adota princípios da **Clean Architecture** através de uma estrutura de pastas dentro de um **único projeto principal**, acompanhado de um **projeto de testes** separado (`clubes-batch.Tests`). 

As responsabilidades estão divididas na seguinte estrutura de pastas:

- **Domain**: Contém os conceitos centrais, entidades e modelos de domínio da aplicação.
- **Application**: Contém as regras de negócio e os casos de uso (orquestração das operações).
- **Infrastructure**: Responsável por detalhes técnicos externos, como a leitura do arquivo JSONL de entrada e a gravação (geração) dos arquivos `.csv` de saída.
- **Program.cs**: Atua como o ponto de entrada da aplicação, responsável por configurar as dependências (Injeção de Dependência, se houver) e orquestrar o início do fluxo de processamento em lote.

## Próximos Passos (A Definir)
Nenhuma decisão de implementação técnica específica foi tomada até o momento. As seguintes partes **ainda serão definidas e implementadas nas próximas etapas**:
- Regras de negócio específicas e validações.
- Lógica exata e bibliotecas para leitura otimizada e eficiente do arquivo JSONL.
- Lógica exata e bibliotecas para a geração dos arquivos `players.csv` e `clubs.csv`.
- Tratamento de memória e performance, considerando a premissa de um arquivo JSONL muito grande.
