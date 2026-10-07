# TASK-007 — Implementar lifecycle e orquestração de sessão

## Objetivo

Implementar lifecycle e orquestração de sessão como incremento independente, verificável e alinhado à arquitetura aprovada.

## Epic

EPIC-02

## Contexto

Esta Task entrega uma unidade independente do EPIC-02 e segue a Architecture Baseline aprovada. Paralelismo: **SEQUENTIAL**. Wave: **2**.

## Requirements

RF-001, RF-009, RF-026, RF-029; RNF-001, RNF-002, RNF-010, RNF-026; SEC-017, SEC-020, SEC-021, SEC-031.

## ADRs

ADR-003, ADR-009.

## Dependências

TASK-005, TASK-006.

## Bloqueia

TASK-008, TASK-013, TASK-029.

## Prioridade

P0

## Complexidade

L

## Risco

HIGH

## Arquivos/áreas esperadas

`ObsAi.Application`; lifecycle; use cases; shutdown.

## Implementação esperada

Implementar iniciar, pausar, retomar e encerrar o Assistant, com shutdown ordenado, estados explícitos e descarte de resultados tardios.

## Segurança

Cobrir SEC-017, SEC-020, SEC-021, SEC-031. Tratar todos os dados externos como não confiáveis, aplicar least privilege e nunca usar secrets reais em testes ou artefatos.

## Testes obrigatórios

UNIT; INTEGRATION; FAILURE; SECURITY.

## Critérios de aceite

- Pausa bloqueia novas chamadas; shutdown cancela trabalho e impede saídas tardias; falha do Core não cria autoridade ou estado ambíguo.
- O escopo não introduz capability, provider ou infraestrutura fora da V1 aprovada.
- A documentação e a matriz de rastreabilidade são atualizadas quando o contrato mudar.

## Definition of Ready

- objetivo e limites revisados;
- Requirements/ADRs acima confirmados;
- dependências marcadas `DONE`;
- critérios e testes refinados sem blocker;
- branch `feature/task-TASK-007-<descricao>` determinável.

## Definition of Done

- implementação, testes e documentação concluídos;
- critérios de aceite e gates aplicáveis aprovados;
- Code Review e Security Review aplicável sem Critical/High;
- Secret Scan e Prompt Traceability aprovados;
- commit, push, PR, validação, merge em `develop` e cleanup concluídos.

## Quality Gates

Build Gate; Unit Test Gate; Integration Test Gate; Failure Gate; Security Gate; Acceptance Gate.

## Status

BACKLOG
