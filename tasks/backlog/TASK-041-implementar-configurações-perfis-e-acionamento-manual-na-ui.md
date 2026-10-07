# TASK-041 — Implementar configurações, perfis e acionamento manual na UI

## Objetivo

Implementar configurações, perfis e acionamento manual na UI como incremento independente, verificável e alinhado à arquitetura aprovada.

## Epic

EPIC-10

## Contexto

Esta Task entrega uma unidade independente do EPIC-10 e segue a Architecture Baseline aprovada. Paralelismo: **SEQUENTIAL**. Wave: **6**.

## Requirements

RF-001 a RF-004, RF-009, RF-011, RF-012, RF-017, RF-018, RF-023, RF-024; SEC-002 a SEC-004, SEC-009, SEC-010, SEC-014.

## ADRs

ADR-005, ADR-008.

## Dependências

TASK-014, TASK-018, TASK-020, TASK-032, TASK-040.

## Bloqueia

Nenhuma.

## Prioridade

P1

## Complexidade

L

## Risco

MEDIUM

## Arquivos/áreas esperadas

Dock controls; settings views; profiles; manual input.

## Implementação esperada

Implementar controles para lifecycle, perfis, LiveContext, providers, TTS, moderação e acionamento manual por contratos do Core.

## Segurança

Cobrir SEC-002 a SEC-004, SEC-009, SEC-010, SEC-014. Tratar todos os dados externos como não confiáveis, aplicar least privilege e nunca usar secrets reais em testes ou artefatos.

## Testes obrigatórios

INTEGRATION; IPC; SECURITY; UI ACCESSIBILITY.

## Critérios de aceite

- Mudança inválida preserva estado anterior; secrets ficam mascarados; controles não bypassam políticas; ações essenciais funcionam por teclado.
- O escopo não introduz capability, provider ou infraestrutura fora da V1 aprovada.
- A documentação e a matriz de rastreabilidade são atualizadas quando o contrato mudar.

## Definition of Ready

- objetivo e limites revisados;
- Requirements/ADRs acima confirmados;
- dependências marcadas `DONE`;
- critérios e testes refinados sem blocker;
- branch `feature/task-TASK-041-<descricao>` determinável.

## Definition of Done

- implementação, testes e documentação concluídos;
- critérios de aceite e gates aplicáveis aprovados;
- Code Review e Security Review aplicável sem Critical/High;
- Secret Scan e Prompt Traceability aprovados;
- commit, push, PR, validação, merge em `develop` e cleanup concluídos.

## Quality Gates

Build Gate; IPC Gate; Integration Test Gate; Security Gate; Accessibility Gate; Acceptance Gate.

## Status

BACKLOG
