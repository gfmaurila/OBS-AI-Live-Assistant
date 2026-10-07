# TASK-037 — Implementar TTS Provider Adapter

## Objetivo

Implementar TTS Provider Adapter como incremento independente, verificável e alinhado à arquitetura aprovada.

## Epic

EPIC-09

## Contexto

Esta Task entrega uma unidade independente do EPIC-09 e segue a Architecture Baseline aprovada. Paralelismo: **PARALLEL SAFE com UI foundation**. Wave: **6**.

## Requirements

RF-023 a RF-027; RNF-002, RNF-009 a RNF-011, RNF-017; SEC-005, SEC-011, SEC-015 a SEC-018, SEC-021, SEC-033, SEC-034.

## ADRs

ADR-004, ADR-006.

## Dependências

TASK-008, TASK-012, TASK-013, TASK-017, TASK-036, TASK-038.

## Bloqueia

TASK-039, TASK-049.

## Prioridade

P1

## Complexidade

XL

## Risco

HIGH

## Arquivos/áreas esperadas

TTS port adapter; audio format; voices; cancellation.

## Implementação esperada

Implementar o adapter aprovado com texto limitado, voz/locale, formato negociado, timeout/cancellation, erro normalizado e streaming opcional honesto.

## Segurança

Cobrir SEC-005, SEC-011, SEC-015 a SEC-018, SEC-021, SEC-033, SEC-034. Tratar todos os dados externos como não confiáveis, aplicar least privilege e nunca usar secrets reais em testes ou artefatos.

## Testes obrigatórios

UNIT; CONTRACT; INTEGRATION simulado; SECURITY; FAILURE.

## Critérios de aceite

- Somente texto aprovado chega ao TTS; provider falho não impede texto; duração/fila/cancelamento respeitam políticas.
- O escopo não introduz capability, provider ou infraestrutura fora da V1 aprovada.
- A documentação e a matriz de rastreabilidade são atualizadas quando o contrato mudar.

## Definition of Ready

- objetivo e limites revisados;
- Requirements/ADRs acima confirmados;
- dependências marcadas `DONE`;
- critérios e testes refinados sem blocker;
- branch `feature/task-TASK-037-<descricao>` determinável.

## Definition of Done

- implementação, testes e documentação concluídos;
- critérios de aceite e gates aplicáveis aprovados;
- Code Review e Security Review aplicável sem Critical/High;
- Secret Scan e Prompt Traceability aprovados;
- commit, push, PR, validação, merge em `develop` e cleanup concluídos.

## Quality Gates

Build Gate; Contract Gate; Integration Test Gate; Security Gate; Failure Gate; Acceptance Gate.

## Status

BACKLOG
