# TASK-027 — Implementar adapter obs-websocket

## Objetivo

Implementar adapter obs-websocket como incremento independente, verificável e alinhado à arquitetura aprovada.

## Epic

EPIC-06

## Contexto

Esta Task entrega uma unidade independente do EPIC-06 e segue a Architecture Baseline aprovada. Paralelismo: **PARALLEL SAFE com IPC após ports**. Wave: **4**.

## Requirements

RF-001, RF-022, RF-027, RF-031; RNF-001, RNF-011, RNF-024, RNF-025; SEC-001, SEC-007, SEC-020, SEC-021, SEC-033.

## ADRs

ADR-001, ADR-004, ADR-005, ADR-010.

## Dependências

TASK-005, TASK-013, TASK-017.

## Bloqueia

TASK-028, TASK-029.

## Prioridade

P1

## Complexidade

L

## Risco

HIGH

## Arquivos/áreas esperadas

`ObsAi.ObsIntegration`; obs-websocket 5.x; capability discovery.

## Implementação esperada

Implementar conexão autenticada, GetVersion/capability discovery, events e requests suportados, com segredo por referência e falha fechada.

## Segurança

Cobrir SEC-001, SEC-007, SEC-020, SEC-021, SEC-033. Tratar todos os dados externos como não confiáveis, aplicar least privilege e nunca usar secrets reais em testes ou artefatos.

## Testes obrigatórios

INTEGRATION; CONTRACT; FAILURE; OBS COMPATIBILITY; SECURITY.

## Critérios de aceite

- Autenticação não é desabilitada em localhost; requests não suportados são recusados; desconexão não afeta OBS.
- O escopo não introduz capability, provider ou infraestrutura fora da V1 aprovada.
- A documentação e a matriz de rastreabilidade são atualizadas quando o contrato mudar.

## Definition of Ready

- objetivo e limites revisados;
- Requirements/ADRs acima confirmados;
- dependências marcadas `DONE`;
- critérios e testes refinados sem blocker;
- branch `feature/task-TASK-027-<descricao>` determinável.

## Definition of Done

- implementação, testes e documentação concluídos;
- critérios de aceite e gates aplicáveis aprovados;
- Code Review e Security Review aplicável sem Critical/High;
- Secret Scan e Prompt Traceability aprovados;
- commit, push, PR, validação, merge em `develop` e cleanup concluídos.

## Quality Gates

Build Gate; Integration Test Gate; Contract Gate; OBS Compatibility Gate; Security Gate; Code Review Gate.

## Status

BACKLOG
