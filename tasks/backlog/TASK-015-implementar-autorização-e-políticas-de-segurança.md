# TASK-015 — Implementar autorização e políticas de segurança

## Objetivo

Implementar autorização e políticas de segurança como incremento independente, verificável e alinhado à arquitetura aprovada.

## Epic

EPIC-03

## Contexto

Esta Task entrega uma unidade independente do EPIC-03 e segue a Architecture Baseline aprovada. Paralelismo: **PARALLEL SAFE com TASK-014**. Wave: **2**.

## Requirements

RF-010, RF-020, RF-036; RNF-004, RNF-006, RNF-007; SEC-003 a SEC-007, SEC-032.

## ADRs

ADR-001, ADR-008, ADR-011.

## Dependências

TASK-005, TASK-009.

## Bloqueia

TASK-010, TASK-022, TASK-028.

## Prioridade

P0

## Complexidade

L

## Risco

HIGH

## Arquivos/áreas esperadas

policy engine; authorization gate; allowlists; moderation contracts.

## Implementação esperada

Implementar políticas deny-by-default para ações sensíveis, separação entre dados e autoridade e decisões auditáveis sem payload sensível.

## Segurança

Cobrir SEC-003 a SEC-007, SEC-032. Tratar todos os dados externos como não confiáveis, aplicar least privilege e nunca usar secrets reais em testes ou artefatos.

## Testes obrigatórios

UNIT; SECURITY; ARCHITECTURE.

## Critérios de aceite

- Chat e AI output nunca autorizam ação; capability não allowlisted é negada; decisões são testáveis e correlacionáveis.
- O escopo não introduz capability, provider ou infraestrutura fora da V1 aprovada.
- A documentação e a matriz de rastreabilidade são atualizadas quando o contrato mudar.

## Definition of Ready

- objetivo e limites revisados;
- Requirements/ADRs acima confirmados;
- dependências marcadas `DONE`;
- critérios e testes refinados sem blocker;
- branch `feature/task-TASK-015-<descricao>` determinável.

## Definition of Done

- implementação, testes e documentação concluídos;
- critérios de aceite e gates aplicáveis aprovados;
- Code Review e Security Review aplicável sem Critical/High;
- Secret Scan e Prompt Traceability aprovados;
- commit, push, PR, validação, merge em `develop` e cleanup concluídos.

## Quality Gates

Build Gate; Unit Test Gate; Architecture Gate; Security Gate; Acceptance Gate; Code Review Gate.

## Status

BACKLOG
