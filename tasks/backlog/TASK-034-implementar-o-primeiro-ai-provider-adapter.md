# TASK-034 — Implementar o primeiro AI Provider Adapter

## Objetivo

Implementar o primeiro AI Provider Adapter como incremento independente, verificável e alinhado à arquitetura aprovada.

## Epic

EPIC-08

## Contexto

Esta Task entrega uma unidade independente do EPIC-08 e segue a Architecture Baseline aprovada. Paralelismo: **SEQUENTIAL após seleção**. Wave: **5**.

## Requirements

RF-016 a RF-020, RF-026, RF-027; RNF-009 a RNF-011, RNF-017; SEC-001, SEC-004, SEC-005, SEC-011, SEC-016 a SEC-018, SEC-033, SEC-034.

## ADRs

ADR-004, ADR-005.

## Dependências

TASK-011, TASK-013, TASK-017, TASK-033.

## Bloqueia

TASK-035.

## Prioridade

P0

## Complexidade

XL

## Risco

HIGH

## Arquivos/áreas esperadas

`ObsAi.Providers`; selected AI adapter; HTTP/stream parsing.

## Implementação esperada

Implementar o adapter do provider aprovado com credential reference, system/input separados, streaming quando suportado, output limit, timeout/cancellation e erros normalizados.

## Segurança

Cobrir SEC-001, SEC-004, SEC-005, SEC-011, SEC-016 a SEC-018, SEC-033, SEC-034. Tratar todos os dados externos como não confiáveis, aplicar least privilege e nunca usar secrets reais em testes ou artefatos.

## Testes obrigatórios

UNIT; CONTRACT; INTEGRATION simulado; SECURITY; FAILURE.

## Critérios de aceite

- Somente input aprovado é enviado; capability é honesta; credenciais não vazam; falhas liberam fila e não produzem saída falsa.
- O escopo não introduz capability, provider ou infraestrutura fora da V1 aprovada.
- A documentação e a matriz de rastreabilidade são atualizadas quando o contrato mudar.

## Definition of Ready

- objetivo e limites revisados;
- Requirements/ADRs acima confirmados;
- dependências marcadas `DONE`;
- critérios e testes refinados sem blocker;
- branch `feature/task-TASK-034-<descricao>` determinável.

## Definition of Done

- implementação, testes e documentação concluídos;
- critérios de aceite e gates aplicáveis aprovados;
- Code Review e Security Review aplicável sem Critical/High;
- Secret Scan e Prompt Traceability aprovados;
- commit, push, PR, validação, merge em `develop` e cleanup concluídos.

## Quality Gates

Build Gate; Unit Test Gate; Contract Gate; Integration Test Gate; Security Gate; Failure Gate.

## Status

BACKLOG
