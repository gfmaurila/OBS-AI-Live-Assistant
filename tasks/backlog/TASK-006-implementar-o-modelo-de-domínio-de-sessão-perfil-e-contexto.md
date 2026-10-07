# TASK-006 — Implementar o modelo de domínio de sessão, perfil e contexto

## Objetivo

Implementar o modelo de domínio de sessão, perfil e contexto como incremento independente, verificável e alinhado à arquitetura aprovada.

## Epic

EPIC-02

## Contexto

Esta Task entrega uma unidade independente do EPIC-02 e segue a Architecture Baseline aprovada. Paralelismo: **PARALLEL SAFE com TASK-005**. Wave: **2**.

## Requirements

RF-003, RF-004, RF-015, RF-029; RNF-005, RNF-018, RNF-023, RNF-026; SEC-024, SEC-025, SEC-031.

## ADRs

ADR-008, ADR-009, ADR-011.

## Dependências

TASK-002, TASK-004.

## Bloqueia

TASK-007, TASK-009, TASK-011, TASK-014, TASK-019.

## Prioridade

P0

## Complexidade

L

## Risco

MEDIUM

## Arquivos/áreas esperadas

`ObsAi.Domain`; Session; LiveContext; AssistantProfile.

## Implementação esperada

Implementar entidades, value objects e invariantes de sessão, perfil e LiveContext sem dependências externas e sem Persistent Memory.

## Segurança

Cobrir SEC-024, SEC-025, SEC-031. Tratar todos os dados externos como não confiáveis, aplicar least privilege e nunca usar secrets reais em testes ou artefatos.

## Testes obrigatórios

UNIT; ARCHITECTURE; SECURITY.

## Critérios de aceite

- Contexto é allowlisted, limitado e session-scoped; encerramento impede reutilização; Persistent Memory permanece desabilitada.
- O escopo não introduz capability, provider ou infraestrutura fora da V1 aprovada.
- A documentação e a matriz de rastreabilidade são atualizadas quando o contrato mudar.

## Definition of Ready

- objetivo e limites revisados;
- Requirements/ADRs acima confirmados;
- dependências marcadas `DONE`;
- critérios e testes refinados sem blocker;
- branch `feature/task-TASK-006-<descricao>` determinável.

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
