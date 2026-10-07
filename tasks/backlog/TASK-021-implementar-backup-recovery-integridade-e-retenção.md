# TASK-021 — Implementar backup, recovery, integridade e retenção

## Objetivo

Implementar backup, recovery, integridade e retenção como incremento independente, verificável e alinhado à arquitetura aprovada.

## Epic

EPIC-04

## Contexto

Esta Task entrega uma unidade independente do EPIC-04 e segue a Architecture Baseline aprovada. Paralelismo: **SEQUENTIAL**. Wave: **3**.

## Requirements

RF-004, RF-015, RF-028 a RF-030; RNF-005, RNF-012, RNF-016, RNF-023, RNF-026; SEC-019, SEC-022 a SEC-025, SEC-031.

## ADRs

ADR-004, ADR-009.

## Dependências

TASK-019, TASK-020.

## Bloqueia

TASK-047.

## Prioridade

P1

## Complexidade

L

## Risco

HIGH

## Arquivos/áreas esperadas

Backup API; integrity check; retention; cleanup; history opt-in.

## Implementação esperada

Implementar backup consistente, health/integrity, recovery explícito e retenção/exclusão por categoria, sem copiar arquivo em uso nem habilitar Persistent Memory.

## Segurança

Cobrir SEC-019, SEC-022 a SEC-025, SEC-031. Tratar todos os dados externos como não confiáveis, aplicar least privilege e nunca usar secrets reais em testes ou artefatos.

## Testes obrigatórios

INTEGRATION; DATABASE; FAILURE; SECURITY.

## Critérios de aceite

- Corrupção/lock produzem modo degradado e caminho recuperável; dados expirados/excluídos deixam de ser usados; prazos não definidos permanecem configuráveis/desabilitados.
- O escopo não introduz capability, provider ou infraestrutura fora da V1 aprovada.
- A documentação e a matriz de rastreabilidade são atualizadas quando o contrato mudar.

## Definition of Ready

- objetivo e limites revisados;
- Requirements/ADRs acima confirmados;
- dependências marcadas `DONE`;
- critérios e testes refinados sem blocker;
- branch `feature/task-TASK-021-<descricao>` determinável.

## Definition of Done

- implementação, testes e documentação concluídos;
- critérios de aceite e gates aplicáveis aprovados;
- Code Review e Security Review aplicável sem Critical/High;
- Secret Scan e Prompt Traceability aprovados;
- commit, push, PR, validação, merge em `develop` e cleanup concluídos.

## Quality Gates

Build Gate; Integration Test Gate; Database Gate; Failure Gate; Security Gate; Acceptance Gate.

## Status

BACKLOG
