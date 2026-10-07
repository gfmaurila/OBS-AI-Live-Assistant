# TASK-028 — Implementar autorização de capabilities OBS

## Objetivo

Implementar autorização de capabilities OBS como incremento independente, verificável e alinhado à arquitetura aprovada.

## Epic

EPIC-06

## Contexto

Esta Task entrega uma unidade independente do EPIC-06 e segue a Architecture Baseline aprovada. Paralelismo: **SEQUENTIAL**. Wave: **4**.

## Requirements

RF-036; RNF-004, RNF-006, RNF-025; SEC-004, SEC-006, SEC-007, SEC-026, SEC-030.

## ADRs

ADR-001, ADR-002, ADR-008.

## Dependências

TASK-015, TASK-026, TASK-027.

## Bloqueia

TASK-029.

## Prioridade

P0

## Complexidade

L

## Risco

HIGH

## Arquivos/áreas esperadas

OBS capability policy; allowlist; audit events.

## Implementação esperada

Conectar os adapters OBS ao authorization gate, aplicar allowlist por capability e impedir que conteúdo seja interpretado como comando.

## Segurança

Cobrir SEC-004, SEC-006, SEC-007, SEC-026, SEC-030. Tratar todos os dados externos como não confiáveis, aplicar least privilege e nunca usar secrets reais em testes ou artefatos.

## Testes obrigatórios

UNIT; INTEGRATION; SECURITY; IPC.

## Critérios de aceite

- Somente comando estruturado, autorizado e allowlisted atravessa o boundary; chat/IA nunca concedem autoridade; negações são diagnosticáveis.
- O escopo não introduz capability, provider ou infraestrutura fora da V1 aprovada.
- A documentação e a matriz de rastreabilidade são atualizadas quando o contrato mudar.

## Definition of Ready

- objetivo e limites revisados;
- Requirements/ADRs acima confirmados;
- dependências marcadas `DONE`;
- critérios e testes refinados sem blocker;
- branch `feature/task-TASK-028-<descricao>` determinável.

## Definition of Done

- implementação, testes e documentação concluídos;
- critérios de aceite e gates aplicáveis aprovados;
- Code Review e Security Review aplicável sem Critical/High;
- Secret Scan e Prompt Traceability aprovados;
- commit, push, PR, validação, merge em `develop` e cleanup concluídos.

## Quality Gates

Build Gate; Integration Test Gate; IPC Gate; Security Gate; Acceptance Gate; Code Review Gate.

## Status

BACKLOG
