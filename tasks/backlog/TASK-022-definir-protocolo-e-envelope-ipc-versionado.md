# TASK-022 — Definir protocolo e envelope IPC versionado

## Objetivo

Definir protocolo e envelope IPC versionado como incremento independente, verificável e alinhado à arquitetura aprovada.

## Epic

EPIC-05

## Contexto

Esta Task entrega uma unidade independente do EPIC-05 e segue a Architecture Baseline aprovada. Paralelismo: **PARALLEL SAFE com persistence**. Wave: **3**.

## Requirements

RF-026, RF-027; RNF-004, RNF-009, RNF-010, RNF-026; SEC-016, SEC-017, SEC-030, SEC-031.

## ADRs

ADR-002.

## Dependências

TASK-005, TASK-013, TASK-015.

## Bloqueia

TASK-023, TASK-025.

## Prioridade

P0

## Complexidade

L

## Risco

HIGH

## Arquivos/áreas esperadas

`ObsBridge.Contracts`; framing; handshake; versioning.

## Implementação esperada

Definir protocolo framed com protocolVersion, messageType, messageId, correlationId, sessionId, expiry e payload versionado para requests, responses e events.

## Segurança

Cobrir SEC-016, SEC-017, SEC-030, SEC-031. Tratar todos os dados externos como não confiáveis, aplicar least privilege e nunca usar secrets reais em testes ou artefatos.

## Testes obrigatórios

UNIT; CONTRACT; IPC; SECURITY.

## Critérios de aceite

- Mensagens unknown, oversize, expiradas, repetidas ou incompatíveis falham fechado; contrato não contém tipos OBS/vendor.
- O escopo não introduz capability, provider ou infraestrutura fora da V1 aprovada.
- A documentação e a matriz de rastreabilidade são atualizadas quando o contrato mudar.

## Definition of Ready

- objetivo e limites revisados;
- Requirements/ADRs acima confirmados;
- dependências marcadas `DONE`;
- critérios e testes refinados sem blocker;
- branch `feature/task-TASK-022-<descricao>` determinável.

## Definition of Done

- implementação, testes e documentação concluídos;
- critérios de aceite e gates aplicáveis aprovados;
- Code Review e Security Review aplicável sem Critical/High;
- Secret Scan e Prompt Traceability aprovados;
- commit, push, PR, validação, merge em `develop` e cleanup concluídos.

## Quality Gates

Build Gate; Contract Gate; IPC Gate; Security Gate; Architecture Gate; Code Review Gate.

## Status

BACKLOG
