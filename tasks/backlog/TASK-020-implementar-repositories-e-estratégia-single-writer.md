# TASK-020 — Implementar repositories e estratégia single-writer

## Objetivo

Implementar repositories e estratégia single-writer como incremento independente, verificável e alinhado à arquitetura aprovada.

## Epic

EPIC-04

## Contexto

Esta Task entrega uma unidade independente do EPIC-04 e segue a Architecture Baseline aprovada. Paralelismo: **SEQUENTIAL**. Wave: **3**.

## Requirements

RF-002, RF-003, RF-012, RF-028, RF-029; RNF-012, RNF-022; SEC-022, SEC-023.

## ADRs

ADR-004.

## Dependências

TASK-005, TASK-019.

## Bloqueia

TASK-021, TASK-041.

## Prioridade

P0

## Complexidade

L

## Risco

HIGH

## Arquivos/áreas esperadas

SQLite adapter; repositories; transactions; configuration persistence.

## Implementação esperada

Implementar repositories/adapters SQLite com single-writer, transações curtas, validação na leitura e atualização atômica do último estado válido.

## Segurança

Cobrir SEC-022, SEC-023. Tratar todos os dados externos como não confiáveis, aplicar least privilege e nunca usar secrets reais em testes ou artefatos.

## Testes obrigatórios

INTEGRATION; DATABASE; FAILURE; SECURITY.

## Critérios de aceite

- Lock/escrita/leitura falhos não viram sucesso; configuração válida não é substituída silenciosamente; secrets nunca são persistidos.
- O escopo não introduz capability, provider ou infraestrutura fora da V1 aprovada.
- A documentação e a matriz de rastreabilidade são atualizadas quando o contrato mudar.

## Definition of Ready

- objetivo e limites revisados;
- Requirements/ADRs acima confirmados;
- dependências marcadas `DONE`;
- critérios e testes refinados sem blocker;
- branch `feature/task-TASK-020-<descricao>` determinável.

## Definition of Done

- implementação, testes e documentação concluídos;
- critérios de aceite e gates aplicáveis aprovados;
- Code Review e Security Review aplicável sem Critical/High;
- Secret Scan e Prompt Traceability aprovados;
- commit, push, PR, validação, merge em `develop` e cleanup concluídos.

## Quality Gates

Build Gate; Integration Test Gate; Database Gate; Failure Gate; Security Gate; Code Review Gate.

## Status

BACKLOG
