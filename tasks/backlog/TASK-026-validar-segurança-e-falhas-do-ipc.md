# TASK-026 — Validar segurança e falhas do IPC

## Objetivo

Validar segurança e falhas do IPC como incremento independente, verificável e alinhado à arquitetura aprovada.

## Epic

EPIC-05

## Contexto

Esta Task entrega uma unidade independente do EPIC-05 e segue a Architecture Baseline aprovada. Paralelismo: **SEQUENTIAL**. Wave: **4**.

## Requirements

RF-026, RF-027, RF-036; RNF-004, RNF-006, RNF-009, RNF-010; SEC-006, SEC-007, SEC-016, SEC-017, SEC-030, SEC-032.

## ADRs

ADR-002, ADR-003.

## Dependências

TASK-023, TASK-025.

## Bloqueia

TASK-028, TASK-029, TASK-040, TASK-049.

## Prioridade

P0

## Complexidade

L

## Risco

HIGH

## Arquivos/áreas esperadas

DACL; peer identity; replay; malformed tests; backpressure.

## Implementação esperada

Aplicar DACL ao usuário/logon SID, negar rede, validar peers e executar testes de framing, tamanho, replay, sequência, timeout, cancellation e reconnect.

## Segurança

Cobrir SEC-006, SEC-007, SEC-016, SEC-017, SEC-030, SEC-032. Tratar todos os dados externos como não confiáveis, aplicar least privilege e nunca usar secrets reais em testes ou artefatos.

## Testes obrigatórios

IPC; CONTRACT; SECURITY; FAILURE.

## Critérios de aceite

- Cliente não autorizado e mensagem inválida não produzem ação; acesso de rede é negado; desconexão/saturação não afetam OBS.
- O escopo não introduz capability, provider ou infraestrutura fora da V1 aprovada.
- A documentação e a matriz de rastreabilidade são atualizadas quando o contrato mudar.

## Definition of Ready

- objetivo e limites revisados;
- Requirements/ADRs acima confirmados;
- dependências marcadas `DONE`;
- critérios e testes refinados sem blocker;
- branch `feature/task-TASK-026-<descricao>` determinável.

## Definition of Done

- implementação, testes e documentação concluídos;
- critérios de aceite e gates aplicáveis aprovados;
- Code Review e Security Review aplicável sem Critical/High;
- Secret Scan e Prompt Traceability aprovados;
- commit, push, PR, validação, merge em `develop` e cleanup concluídos.

## Quality Gates

IPC Gate; Contract Gate; Security Gate; Failure Gate; Acceptance Gate; Code Review Gate.

## Status

BACKLOG
