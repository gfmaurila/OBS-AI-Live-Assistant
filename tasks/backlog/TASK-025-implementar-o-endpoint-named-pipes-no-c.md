# TASK-025 — Implementar o endpoint Named Pipes no C++

## Objetivo

Implementar o endpoint Named Pipes no C++ como incremento independente, verificável e alinhado à arquitetura aprovada.

## Epic

EPIC-05

## Contexto

Esta Task entrega uma unidade independente do EPIC-05 e segue a Architecture Baseline aprovada. Paralelismo: **SEQUENTIAL após TASK-024**. Wave: **4**.

## Requirements

RF-026, RF-027; RNF-001, RNF-009, RNF-010, RNF-026; SEC-016, SEC-017, SEC-020, SEC-030.

## ADRs

ADR-002, ADR-003.

## Dependências

TASK-022, TASK-024.

## Bloqueia

TASK-026.

## Prioridade

P0

## Complexidade

XL

## Risco

HIGH

## Arquivos/áreas esperadas

plugin C++; pipe client/server; async I/O.

## Implementação esperada

Implementar peer nativo do contrato IPC sem bloquear threads do OBS, com lifecycle, framing, timeout, cancelamento e reconexão.

## Segurança

Cobrir SEC-016, SEC-017, SEC-020, SEC-030. Tratar todos os dados externos como não confiáveis, aplicar least privilege e nunca usar secrets reais em testes ou artefatos.

## Testes obrigatórios

IPC; CONTRACT; FAILURE; OBS COMPATIBILITY; SECURITY.

## Critérios de aceite

- Peer C++ interopera com .NET; I/O é assíncrona; perda do pipe apenas degrada o plugin; protocolo incompatível é recusado.
- O escopo não introduz capability, provider ou infraestrutura fora da V1 aprovada.
- A documentação e a matriz de rastreabilidade são atualizadas quando o contrato mudar.

## Definition of Ready

- objetivo e limites revisados;
- Requirements/ADRs acima confirmados;
- dependências marcadas `DONE`;
- critérios e testes refinados sem blocker;
- branch `feature/task-TASK-025-<descricao>` determinável.

## Definition of Done

- implementação, testes e documentação concluídos;
- critérios de aceite e gates aplicáveis aprovados;
- Code Review e Security Review aplicável sem Critical/High;
- Secret Scan e Prompt Traceability aprovados;
- commit, push, PR, validação, merge em `develop` e cleanup concluídos.

## Quality Gates

Native Build Gate; IPC Gate; Contract Gate; Failure Gate; Security Gate; Code Review Gate.

## Status

BACKLOG
