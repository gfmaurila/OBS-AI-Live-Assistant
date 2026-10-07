# TASK-029 — Validar failure isolation e lifecycle OBS/Core

## Objetivo

Validar failure isolation e lifecycle OBS/Core como incremento independente, verificável e alinhado à arquitetura aprovada.

## Epic

EPIC-06

## Contexto

Esta Task entrega uma unidade independente do EPIC-06 e segue a Architecture Baseline aprovada. Paralelismo: **SEQUENTIAL**. Wave: **4**.

## Requirements

RF-001, RF-026, RF-027, RF-031; RNF-001, RNF-002, RNF-026; SEC-020, SEC-021, SEC-026, SEC-030, SEC-031.

## ADRs

ADR-001 a ADR-003.

## Dependências

TASK-007, TASK-026, TASK-027, TASK-028.

## Bloqueia

TASK-032, TASK-038, TASK-040, TASK-044, TASK-045, TASK-050.

## Prioridade

P0

## Complexidade

XL

## Risco

HIGH

## Arquivos/áreas esperadas

startup; shutdown; crash/hang; reconnect; health bridge.

## Implementação esperada

Integrar lifecycle do plugin, Core, IPC e WebSocket e provar contenção de crash, hang, disconnect e shutdown sem replay de saída ambígua.

## Segurança

Cobrir SEC-020, SEC-021, SEC-026, SEC-030, SEC-031. Tratar todos os dados externos como não confiáveis, aplicar least privilege e nunca usar secrets reais em testes ou artefatos.

## Testes obrigatórios

FAILURE; IPC; OBS COMPATIBILITY; END-TO-END; SECURITY.

## Critérios de aceite

- Assistant failure != OBS failure em todos os cenários previstos; restart/reconnect cria sessão nova; OBS permanece responsivo.
- O escopo não introduz capability, provider ou infraestrutura fora da V1 aprovada.
- A documentação e a matriz de rastreabilidade são atualizadas quando o contrato mudar.

## Definition of Ready

- objetivo e limites revisados;
- Requirements/ADRs acima confirmados;
- dependências marcadas `DONE`;
- critérios e testes refinados sem blocker;
- branch `feature/task-TASK-029-<descricao>` determinável.

## Definition of Done

- implementação, testes e documentação concluídos;
- critérios de aceite e gates aplicáveis aprovados;
- Code Review e Security Review aplicável sem Critical/High;
- Secret Scan e Prompt Traceability aprovados;
- commit, push, PR, validação, merge em `develop` e cleanup concluídos.

## Quality Gates

Build Gate; Native Build Gate; Failure Gate; IPC Gate; OBS Compatibility Gate; Security Gate; Acceptance Gate.

## Status

BACKLOG
