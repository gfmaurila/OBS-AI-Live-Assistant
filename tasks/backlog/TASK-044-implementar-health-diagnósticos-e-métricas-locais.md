# TASK-044 — Implementar health, diagnósticos e métricas locais

## Objetivo

Implementar health, diagnósticos e métricas locais como incremento independente, verificável e alinhado à arquitetura aprovada.

## Epic

EPIC-11

## Contexto

Esta Task entrega uma unidade independente do EPIC-11 e segue a Architecture Baseline aprovada. Paralelismo: **PARALLEL SAFE com providers**. Wave: **5**.

## Requirements

RF-031; RNF-014 a RNF-016; SEC-018, SEC-019, SEC-021, SEC-026.

## ADRs

ADR-003, ADR-008.

## Dependências

TASK-008, TASK-013, TASK-029, TASK-043.

## Bloqueia

TASK-042, TASK-050, TASK-051.

## Prioridade

P1

## Complexidade

L

## Risco

MEDIUM

## Arquivos/áreas esperadas

health aggregation; queue metrics; latency; provider failures.

## Implementação esperada

Agregar health por adapter e métricas locais de fila, rejeição, latência, timeout, cancellation e falha, sem stack cloud obrigatória.

## Segurança

Cobrir SEC-018, SEC-019, SEC-021, SEC-026. Tratar todos os dados externos como não confiáveis, aplicar least privilege e nunca usar secrets reais em testes ou artefatos.

## Testes obrigatórios

UNIT; INTEGRATION; FAILURE; SECURITY.

## Critérios de aceite

- Estados são calculados sem payload sensível; saturation e falhas por provider são observáveis; coleta possui limites.
- O escopo não introduz capability, provider ou infraestrutura fora da V1 aprovada.
- A documentação e a matriz de rastreabilidade são atualizadas quando o contrato mudar.

## Definition of Ready

- objetivo e limites revisados;
- Requirements/ADRs acima confirmados;
- dependências marcadas `DONE`;
- critérios e testes refinados sem blocker;
- branch `feature/task-TASK-044-<descricao>` determinável.

## Definition of Done

- implementação, testes e documentação concluídos;
- critérios de aceite e gates aplicáveis aprovados;
- Code Review e Security Review aplicável sem Critical/High;
- Secret Scan e Prompt Traceability aprovados;
- commit, push, PR, validação, merge em `develop` e cleanup concluídos.

## Quality Gates

Build Gate; Unit Test Gate; Integration Test Gate; Failure Gate; Security Gate; Code Review Gate.

## Status

BACKLOG
