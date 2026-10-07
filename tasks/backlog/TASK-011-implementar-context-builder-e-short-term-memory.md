# TASK-011 — Implementar Context Builder e Short-Term Memory

## Objetivo

Implementar Context Builder e Short-Term Memory como incremento independente, verificável e alinhado à arquitetura aprovada.

## Epic

EPIC-02

## Contexto

Esta Task entrega uma unidade independente do EPIC-02 e segue a Architecture Baseline aprovada. Paralelismo: **PARALLEL SAFE com TASK-010 após dependências**. Wave: **2**.

## Requirements

RF-004, RF-014, RF-015, RF-029; RNF-005, RNF-007, RNF-023; SEC-004, SEC-024, SEC-025, SEC-031.

## ADRs

ADR-008, ADR-009.

## Dependências

TASK-006, TASK-009, TASK-014.

## Bloqueia

TASK-012, TASK-034.

## Prioridade

P0

## Complexidade

L

## Risco

HIGH

## Arquivos/áreas esperadas

context builder; short-term memory; session isolation.

## Implementação esperada

Compor instruções confiáveis, perfil, LiveContext e memória temporária com allowlist, limites e isolamento por sessão.

## Segurança

Cobrir SEC-004, SEC-024, SEC-025, SEC-031. Tratar todos os dados externos como não confiáveis, aplicar least privilege e nunca usar secrets reais em testes ou artefatos.

## Testes obrigatórios

UNIT; SECURITY; FAILURE.

## Critérios de aceite

- Nenhum secret ou histórico ilimitado entra no prompt; dados encerrados/excluídos deixam de ser usados; resultados não cruzam sessão.
- O escopo não introduz capability, provider ou infraestrutura fora da V1 aprovada.
- A documentação e a matriz de rastreabilidade são atualizadas quando o contrato mudar.

## Definition of Ready

- objetivo e limites revisados;
- Requirements/ADRs acima confirmados;
- dependências marcadas `DONE`;
- critérios e testes refinados sem blocker;
- branch `feature/task-TASK-011-<descricao>` determinável.

## Definition of Done

- implementação, testes e documentação concluídos;
- critérios de aceite e gates aplicáveis aprovados;
- Code Review e Security Review aplicável sem Critical/High;
- Secret Scan e Prompt Traceability aprovados;
- commit, push, PR, validação, merge em `develop` e cleanup concluídos.

## Quality Gates

Build Gate; Unit Test Gate; Security Gate; Acceptance Gate; Code Review Gate.

## Status

BACKLOG
