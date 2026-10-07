# TASK-019 — Definir schema e migrations SQLite

## Objetivo

Definir schema e migrations SQLite como incremento independente, verificável e alinhado à arquitetura aprovada.

## Epic

EPIC-04

## Contexto

Esta Task entrega uma unidade independente do EPIC-04 e segue a Architecture Baseline aprovada. Paralelismo: **PARALLEL SAFE com TASK-017**. Wave: **3**.

## Requirements

RF-028 a RF-030, RF-033; RNF-005, RNF-012, RNF-022, RNF-023; SEC-022 a SEC-025, SEC-031.

## ADRs

ADR-004, ADR-009.

## Dependências

TASK-002, TASK-004, TASK-006, TASK-014.

## Bloqueia

TASK-020, TASK-021.

## Prioridade

P0

## Complexidade

L

## Risco

HIGH

## Arquivos/áreas esperadas

SQLite schema; migrations; data classification; paths.

## Implementação esperada

Definir e implementar schema versionado somente para Settings, metadata não secreta, Profiles, Sessions, Moderation e histórico opt-in autorizado.

## Segurança

Cobrir SEC-022 a SEC-025, SEC-031. Tratar todos os dados externos como não confiáveis, aplicar least privilege e nunca usar secrets reais em testes ou artefatos.

## Testes obrigatórios

INTEGRATION; DATABASE; SECURITY.

## Critérios de aceite

- Migrations incompatíveis falham explicitamente; schema não reserva Persistent Memory nem secrets; dados possuem finalidade e lifecycle.
- O escopo não introduz capability, provider ou infraestrutura fora da V1 aprovada.
- A documentação e a matriz de rastreabilidade são atualizadas quando o contrato mudar.

## Definition of Ready

- objetivo e limites revisados;
- Requirements/ADRs acima confirmados;
- dependências marcadas `DONE`;
- critérios e testes refinados sem blocker;
- branch `feature/task-TASK-019-<descricao>` determinável.

## Definition of Done

- implementação, testes e documentação concluídos;
- critérios de aceite e gates aplicáveis aprovados;
- Code Review e Security Review aplicável sem Critical/High;
- Secret Scan e Prompt Traceability aprovados;
- commit, push, PR, validação, merge em `develop` e cleanup concluídos.

## Quality Gates

Build Gate; Integration Test Gate; Database Gate; Security Gate; Documentation Gate; Code Review Gate.

## Status

BACKLOG
