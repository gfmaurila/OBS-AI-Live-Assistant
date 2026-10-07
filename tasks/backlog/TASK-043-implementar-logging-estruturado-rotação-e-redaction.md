# TASK-043 — Implementar logging estruturado, rotação e redaction

## Objetivo

Implementar logging estruturado, rotação e redaction como incremento independente, verificável e alinhado à arquitetura aprovada.

## Epic

EPIC-11

## Contexto

Esta Task entrega uma unidade independente do EPIC-11 e segue a Architecture Baseline aprovada. Paralelismo: **PARALLEL SAFE com persistence/IPC**. Wave: **3**.

## Requirements

RF-031; RNF-005, RNF-013, RNF-016, RNF-023; SEC-011, SEC-019, SEC-025, SEC-026.

## ADRs

ADR-008, ADR-009.

## Dependências

TASK-005, TASK-016.

## Bloqueia

TASK-042, TASK-044.

## Prioridade

P0

## Complexidade

L

## Risco

HIGH

## Arquivos/áreas esperadas

observability adapter; event IDs; rotation; retention.

## Implementação esperada

Implementar logging local estruturado, event IDs, correlationId, allowlist de campos, rotação, limite total e retenção configurável.

## Segurança

Cobrir SEC-011, SEC-019, SEC-025, SEC-026. Tratar todos os dados externos como não confiáveis, aplicar least privilege e nunca usar secrets reais em testes ou artefatos.

## Testes obrigatórios

UNIT; INTEGRATION; SECURITY com sentinelas; FAILURE.

## Critérios de aceite

- Logs úteis não contêm secrets/chat/AI completos por padrão; rotação limita disco; redaction ocorre antes do sink.
- O escopo não introduz capability, provider ou infraestrutura fora da V1 aprovada.
- A documentação e a matriz de rastreabilidade são atualizadas quando o contrato mudar.

## Definition of Ready

- objetivo e limites revisados;
- Requirements/ADRs acima confirmados;
- dependências marcadas `DONE`;
- critérios e testes refinados sem blocker;
- branch `feature/task-TASK-043-<descricao>` determinável.

## Definition of Done

- implementação, testes e documentação concluídos;
- critérios de aceite e gates aplicáveis aprovados;
- Code Review e Security Review aplicável sem Critical/High;
- Secret Scan e Prompt Traceability aprovados;
- commit, push, PR, validação, merge em `develop` e cleanup concluídos.

## Quality Gates

Build Gate; Unit Test Gate; Integration Test Gate; Security Gate; Secret Scan; Code Review Gate.

## Status

BACKLOG
