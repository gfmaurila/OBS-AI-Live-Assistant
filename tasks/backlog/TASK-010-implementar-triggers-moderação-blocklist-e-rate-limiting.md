# TASK-010 — Implementar triggers, moderação, blocklist e rate limiting

## Objetivo

Implementar triggers, moderação, blocklist e rate limiting como incremento independente, verificável e alinhado à arquitetura aprovada.

## Epic

EPIC-02

## Contexto

Esta Task entrega uma unidade independente do EPIC-02 e segue a Architecture Baseline aprovada. Paralelismo: **SEQUENTIAL**. Wave: **2**.

## Requirements

RF-008 a RF-012; RNF-004, RNF-007, RNF-008; SEC-003, SEC-004, SEC-014, SEC-015.

## ADRs

ADR-003, ADR-008.

## Dependências

TASK-008, TASK-009, TASK-014, TASK-015.

## Bloqueia

TASK-012, TASK-032.

## Prioridade

P0

## Complexidade

L

## Risco

HIGH

## Arquivos/áreas esperadas

políticas de trigger; moderação; blocklist; cooldown; anti-spam.

## Implementação esperada

Implementar o gate de entrada deny-by-default para `@assistente`, `!ia`, acionamento manual, moderação, blocklist e limites por viewer/global.

## Segurança

Cobrir SEC-003, SEC-004, SEC-014, SEC-015. Tratar todos os dados externos como não confiáveis, aplicar least privilege e nunca usar secrets reais em testes ou artefatos.

## Testes obrigatórios

UNIT; INTEGRATION; SECURITY; FAILURE.

## Critérios de aceite

- Mensagens sem trigger, bloqueadas ou acima do limite não chegam à IA; acionamento manual passa pelos controles aplicáveis; thresholds são validados.
- O escopo não introduz capability, provider ou infraestrutura fora da V1 aprovada.
- A documentação e a matriz de rastreabilidade são atualizadas quando o contrato mudar.

## Definition of Ready

- objetivo e limites revisados;
- Requirements/ADRs acima confirmados;
- dependências marcadas `DONE`;
- critérios e testes refinados sem blocker;
- branch `feature/task-TASK-010-<descricao>` determinável.

## Definition of Done

- implementação, testes e documentação concluídos;
- critérios de aceite e gates aplicáveis aprovados;
- Code Review e Security Review aplicável sem Critical/High;
- Secret Scan e Prompt Traceability aprovados;
- commit, push, PR, validação, merge em `develop` e cleanup concluídos.

## Quality Gates

Build Gate; Unit Test Gate; Integration Test Gate; Security Gate; Acceptance Gate; Code Review Gate.

## Status

BACKLOG
