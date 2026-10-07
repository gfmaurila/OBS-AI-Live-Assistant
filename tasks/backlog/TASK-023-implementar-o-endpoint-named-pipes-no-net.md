# TASK-023 — Implementar o endpoint Named Pipes no .NET

## Objetivo

Implementar o endpoint Named Pipes no .NET como incremento independente, verificável e alinhado à arquitetura aprovada.

## Epic

EPIC-05

## Contexto

Esta Task entrega uma unidade independente do EPIC-05 e segue a Architecture Baseline aprovada. Paralelismo: **PARALLEL SAFE com TASK-024**. Wave: **4**.

## Requirements

RF-026, RF-027, RF-031; RNF-009, RNF-010, RNF-014, RNF-026; SEC-016 a SEC-018, SEC-026, SEC-030.

## ADRs

ADR-002, ADR-003.

## Dependências

TASK-022.

## Bloqueia

TASK-026.

## Prioridade

P0

## Complexidade

L

## Risco

HIGH

## Arquivos/áreas esperadas

`ObsAi.ObsIntegration`; Named Pipes .NET; reconnect.

## Implementação esperada

Implementar peer .NET assíncrono, request/response, events, correlation, timeout, cancellation, reconexão e backpressure.

## Segurança

Cobrir SEC-016 a SEC-018, SEC-026, SEC-030. Tratar todos os dados externos como não confiáveis, aplicar least privilege e nunca usar secrets reais em testes ou artefatos.

## Testes obrigatórios

UNIT; IPC; INTEGRATION; FAILURE; SECURITY.

## Critérios de aceite

- Disconnect não bloqueia Core; reconnect negocia nova sessão; resultados antigos não são reutilizados; filas do pipe são finitas.
- O escopo não introduz capability, provider ou infraestrutura fora da V1 aprovada.
- A documentação e a matriz de rastreabilidade são atualizadas quando o contrato mudar.

## Definition of Ready

- objetivo e limites revisados;
- Requirements/ADRs acima confirmados;
- dependências marcadas `DONE`;
- critérios e testes refinados sem blocker;
- branch `feature/task-TASK-023-<descricao>` determinável.

## Definition of Done

- implementação, testes e documentação concluídos;
- critérios de aceite e gates aplicáveis aprovados;
- Code Review e Security Review aplicável sem Critical/High;
- Secret Scan e Prompt Traceability aprovados;
- commit, push, PR, validação, merge em `develop` e cleanup concluídos.

## Quality Gates

Build Gate; IPC Gate; Integration Test Gate; Failure Gate; Security Gate; Code Review Gate.

## Status

BACKLOG
