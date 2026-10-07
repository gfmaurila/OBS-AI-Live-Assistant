# TASK-032 — Integrar pipeline de chat, acionamento manual e saída textual

## Objetivo

Integrar pipeline de chat, acionamento manual e saída textual como incremento independente, verificável e alinhado à arquitetura aprovada.

## Epic

EPIC-07

## Contexto

Esta Task entrega uma unidade independente do EPIC-07 e segue a Architecture Baseline aprovada. Paralelismo: **SEQUENTIAL**. Wave: **5**.

## Requirements

RF-006 a RF-013, RF-022, RF-027; RNF-002, RNF-004, RNF-008, RNF-011; SEC-001 a SEC-006, SEC-014 a SEC-018, SEC-021.

## ADRs

ADR-001, ADR-003, ADR-004, ADR-008.

## Dependências

TASK-010, TASK-012, TASK-029, TASK-031.

## Bloqueia

TASK-041, TASK-049, TASK-050.

## Prioridade

P0

## Complexidade

L

## Risco

HIGH

## Arquivos/áreas esperadas

chat orchestration; triggers; manual input; YouTube text output.

## Implementação esperada

Conectar recepção/normalização aos gates do Core e publicar texto aprovado no destino autorizado, preservando entrada manual quando chat falhar.

## Segurança

Cobrir SEC-001 a SEC-006, SEC-014 a SEC-018, SEC-021. Tratar todos os dados externos como não confiáveis, aplicar least privilege e nunca usar secrets reais em testes ou artefatos.

## Testes obrigatórios

INTEGRATION; END-TO-END; SECURITY; FAILURE.

## Critérios de aceite

- Fluxo YouTube e manual compartilham controles; texto só é publicado após validação; falha de chat mantém entrada manual e não afeta OBS.
- O escopo não introduz capability, provider ou infraestrutura fora da V1 aprovada.
- A documentação e a matriz de rastreabilidade são atualizadas quando o contrato mudar.

## Definition of Ready

- objetivo e limites revisados;
- Requirements/ADRs acima confirmados;
- dependências marcadas `DONE`;
- critérios e testes refinados sem blocker;
- branch `feature/task-TASK-032-<descricao>` determinável.

## Definition of Done

- implementação, testes e documentação concluídos;
- critérios de aceite e gates aplicáveis aprovados;
- Code Review e Security Review aplicável sem Critical/High;
- Secret Scan e Prompt Traceability aprovados;
- commit, push, PR, validação, merge em `develop` e cleanup concluídos.

## Quality Gates

Build Gate; Integration Test Gate; End-to-End Gate; Security Gate; Failure Gate; Acceptance Gate.

## Status

BACKLOG
