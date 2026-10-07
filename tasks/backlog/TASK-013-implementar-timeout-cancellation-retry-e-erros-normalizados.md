# TASK-013 — Implementar timeout, cancellation, retry e erros normalizados

## Objetivo

Implementar timeout, cancellation, retry e erros normalizados como incremento independente, verificável e alinhado à arquitetura aprovada.

## Epic

EPIC-02

## Contexto

Esta Task entrega uma unidade independente do EPIC-02 e segue a Architecture Baseline aprovada. Paralelismo: **PARALLEL SAFE com TASK-009**. Wave: **2**.

## Requirements

RF-026, RF-027; RNF-009 a RNF-011, RNF-026; SEC-016 a SEC-018, SEC-021.

## ADRs

ADR-002 a ADR-004.

## Dependências

TASK-005, TASK-007, TASK-008.

## Bloqueia

TASK-022, TASK-027, TASK-031, TASK-034, TASK-037, TASK-044.

## Prioridade

P0

## Complexidade

L

## Risco

HIGH

## Arquivos/áreas esperadas

políticas de resiliência; cancellation; result types.

## Implementação esperada

Implementar timeout, propagação de cancelamento, retry limitado somente para operações seguras e classificação comum de falhas.

## Segurança

Cobrir SEC-016 a SEC-018, SEC-021. Tratar todos os dados externos como não confiáveis, aplicar least privilege e nunca usar secrets reais em testes ou artefatos.

## Testes obrigatórios

UNIT; CONTRACT; FAILURE; SECURITY.

## Critérios de aceite

- Toda operação termina em estado explícito; cancelamento impede nova saída; retry não duplica publicação/custo nem cria loop infinito.
- O escopo não introduz capability, provider ou infraestrutura fora da V1 aprovada.
- A documentação e a matriz de rastreabilidade são atualizadas quando o contrato mudar.

## Definition of Ready

- objetivo e limites revisados;
- Requirements/ADRs acima confirmados;
- dependências marcadas `DONE`;
- critérios e testes refinados sem blocker;
- branch `feature/task-TASK-013-<descricao>` determinável.

## Definition of Done

- implementação, testes e documentação concluídos;
- critérios de aceite e gates aplicáveis aprovados;
- Code Review e Security Review aplicável sem Critical/High;
- Secret Scan e Prompt Traceability aprovados;
- commit, push, PR, validação, merge em `develop` e cleanup concluídos.

## Quality Gates

Build Gate; Unit Test Gate; Contract Gate; Failure Gate; Security Gate; Code Review Gate.

## Status

BACKLOG
