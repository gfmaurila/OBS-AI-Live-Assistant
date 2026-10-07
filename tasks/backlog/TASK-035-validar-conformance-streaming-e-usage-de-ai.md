# TASK-035 — Validar conformance, streaming e usage de AI

## Objetivo

Validar conformance, streaming e usage de AI como incremento independente, verificável e alinhado à arquitetura aprovada.

## Epic

EPIC-08

## Contexto

Esta Task entrega uma unidade independente do EPIC-08 e segue a Architecture Baseline aprovada. Paralelismo: **SEQUENTIAL**. Wave: **5**.

## Requirements

RF-016, RF-019, RF-020, RF-026, RF-027; RNF-010, RNF-011, RNF-017, RNF-019; SEC-005, SEC-016 a SEC-018, SEC-032 a SEC-034.

## ADRs

ADR-004, ADR-011.

## Dependências

TASK-012, TASK-034.

## Bloqueia

TASK-049, TASK-050.

## Prioridade

P1

## Complexidade

M

## Risco

MEDIUM

## Arquivos/áreas esperadas

AI contract suite; stream aggregation; usage/finish reason/request ID.

## Implementação esperada

Executar contract tests do adapter, validar partial output, cancelamento, usage opcional, limites, erros e substituibilidade com fake/reference adapter.

## Segurança

Cobrir SEC-005, SEC-016 a SEC-018, SEC-032 a SEC-034. Tratar todos os dados externos como não confiáveis, aplicar least privilege e nunca usar secrets reais em testes ou artefatos.

## Testes obrigatórios

CONTRACT; INTEGRATION; SECURITY; FAILURE.

## Critérios de aceite

- Streaming não contorna output validation; usage ausente é suportado; adapter não declara capability que não cumpre; contrato permanece vendor-neutral.
- O escopo não introduz capability, provider ou infraestrutura fora da V1 aprovada.
- A documentação e a matriz de rastreabilidade são atualizadas quando o contrato mudar.

## Definition of Ready

- objetivo e limites revisados;
- Requirements/ADRs acima confirmados;
- dependências marcadas `DONE`;
- critérios e testes refinados sem blocker;
- branch `feature/task-TASK-035-<descricao>` determinável.

## Definition of Done

- implementação, testes e documentação concluídos;
- critérios de aceite e gates aplicáveis aprovados;
- Code Review e Security Review aplicável sem Critical/High;
- Secret Scan e Prompt Traceability aprovados;
- commit, push, PR, validação, merge em `develop` e cleanup concluídos.

## Quality Gates

Build Gate; Contract Gate; Integration Test Gate; Security Gate; Failure Gate; Code Review Gate.

## Status

BACKLOG
