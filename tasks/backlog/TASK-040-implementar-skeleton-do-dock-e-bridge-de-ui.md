# TASK-040 — Implementar skeleton do Dock e bridge de UI

## Objetivo

Implementar skeleton do Dock e bridge de UI como incremento independente, verificável e alinhado à arquitetura aprovada.

## Epic

EPIC-10

## Contexto

Esta Task entrega uma unidade independente do EPIC-10 e segue a Architecture Baseline aprovada. Paralelismo: **PARALLEL SAFE com TASK-037**. Wave: **6**.

## Requirements

RF-001, RF-009, RF-031; RNF-001, RNF-014, RNF-015, RNF-027; SEC-007, SEC-010, SEC-020, SEC-021, SEC-026.

## ADRs

ADR-001, ADR-008.

## Dependências

TASK-024, TASK-026, TASK-029.

## Bloqueia

TASK-041, TASK-042.

## Prioridade

P1

## Complexidade

XL

## Risco

HIGH

## Arquivos/áreas esperadas

OBS Dock QWidget; presentation contracts; IPC state cache.

## Implementação esperada

Criar Dock fino com lifecycle correto, bridge IPC e estado mínimo redacted, tolerando Core ausente e sem lógica de negócio.

## Segurança

Cobrir SEC-007, SEC-010, SEC-020, SEC-021, SEC-026. Tratar todos os dados externos como não confiáveis, aplicar least privilege e nunca usar secrets reais em testes ou artefatos.

## Testes obrigatórios

OBS COMPATIBILITY; IPC; FAILURE; UI ACCESSIBILITY; SECURITY.

## Critérios de aceite

- Dock não bloqueia frontend; Core ausente mostra estado degradado; nenhum secret/payload integral cruza para apresentação.
- O escopo não introduz capability, provider ou infraestrutura fora da V1 aprovada.
- A documentação e a matriz de rastreabilidade são atualizadas quando o contrato mudar.

## Definition of Ready

- objetivo e limites revisados;
- Requirements/ADRs acima confirmados;
- dependências marcadas `DONE`;
- critérios e testes refinados sem blocker;
- branch `feature/task-TASK-040-<descricao>` determinável.

## Definition of Done

- implementação, testes e documentação concluídos;
- critérios de aceite e gates aplicáveis aprovados;
- Code Review e Security Review aplicável sem Critical/High;
- Secret Scan e Prompt Traceability aprovados;
- commit, push, PR, validação, merge em `develop` e cleanup concluídos.

## Quality Gates

Native Build Gate; OBS Compatibility Gate; IPC Gate; Failure Gate; Security Gate; Acceptance Gate.

## Status

BACKLOG
