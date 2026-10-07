# TASK-042 — Implementar status operacional, diagnóstico e acessibilidade

## Objetivo

Implementar status operacional, diagnóstico e acessibilidade como incremento independente, verificável e alinhado à arquitetura aprovada.

## Epic

EPIC-10

## Contexto

Esta Task entrega uma unidade independente do EPIC-10 e segue a Architecture Baseline aprovada. Paralelismo: **SEQUENTIAL**. Wave: **6**.

## Requirements

RF-031; RNF-013 a RNF-015, RNF-027; SEC-010, SEC-011, SEC-026.

## ADRs

ADR-008.

## Dependências

TASK-040, TASK-043, TASK-044.

## Bloqueia

TASK-049, TASK-050.

## Prioridade

P1

## Complexidade

M

## Risco

MEDIUM

## Arquivos/áreas esperadas

status views; queues; errors; accessibility.

## Implementação esperada

Exibir estados healthy/degraded/disconnected/saturated/failed, erros acionáveis e fila resumida sem depender somente de cor ou áudio.

## Segurança

Cobrir SEC-010, SEC-011, SEC-026. Tratar todos os dados externos como não confiáveis, aplicar least privilege e nunca usar secrets reais em testes ou artefatos.

## Testes obrigatórios

INTEGRATION; UI ACCESSIBILITY; SECURITY; FAILURE.

## Critérios de aceite

- Estado de cada capacidade é distinguível e redacted; controles críticos funcionam por teclado; falhas possuem orientação segura.
- O escopo não introduz capability, provider ou infraestrutura fora da V1 aprovada.
- A documentação e a matriz de rastreabilidade são atualizadas quando o contrato mudar.

## Definition of Ready

- objetivo e limites revisados;
- Requirements/ADRs acima confirmados;
- dependências marcadas `DONE`;
- critérios e testes refinados sem blocker;
- branch `feature/task-TASK-042-<descricao>` determinável.

## Definition of Done

- implementação, testes e documentação concluídos;
- critérios de aceite e gates aplicáveis aprovados;
- Code Review e Security Review aplicável sem Critical/High;
- Secret Scan e Prompt Traceability aprovados;
- commit, push, PR, validação, merge em `develop` e cleanup concluídos.

## Quality Gates

Build Gate; Integration Test Gate; Security Gate; Accessibility Gate; Acceptance Gate; Code Review Gate.

## Status

BACKLOG
