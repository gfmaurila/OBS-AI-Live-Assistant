# TASK-030 — Implementar OAuth e descoberta do YouTube Live Chat

## Objetivo

Implementar OAuth e descoberta do YouTube Live Chat como incremento independente, verificável e alinhado à arquitetura aprovada.

## Epic

EPIC-07

## Contexto

Esta Task entrega uma unidade independente do EPIC-07 e segue a Architecture Baseline aprovada. Paralelismo: **PARALLEL SAFE com TASK-033**. Wave: **5**.

## Requirements

RF-005, RF-018, RF-027; RNF-003, RNF-006, RNF-011; SEC-009, SEC-011 a SEC-013, SEC-033, SEC-034.

## ADRs

ADR-004, ADR-005.

## Dependências

TASK-005, TASK-018.

## Bloqueia

TASK-031.

## Prioridade

P0

## Complexidade

L

## Risco

HIGH

## Arquivos/áreas esperadas

YouTube adapter; OAuth desktop PKCE; broadcast discovery.

## Implementação esperada

Implementar autenticação pelo browser do sistema/loopback/PKCE, scopes mínimos, descoberta de broadcast e liveChatId, sem client secret embarcado.

## Segurança

Cobrir SEC-009, SEC-011 a SEC-013, SEC-033, SEC-034. Tratar todos os dados externos como não confiáveis, aplicar least privilege e nunca usar secrets reais em testes ou artefatos.

## Testes obrigatórios

INTEGRATION; CONTRACT; SECURITY; FAILURE.

## Critérios de aceite

- Token expirado/revogado é rejeitado; conexão informa estado real; nenhum código/token aparece em log; quota e scopes são rastreáveis.
- O escopo não introduz capability, provider ou infraestrutura fora da V1 aprovada.
- A documentação e a matriz de rastreabilidade são atualizadas quando o contrato mudar.

## Definition of Ready

- objetivo e limites revisados;
- Requirements/ADRs acima confirmados;
- dependências marcadas `DONE`;
- critérios e testes refinados sem blocker;
- branch `feature/task-TASK-030-<descricao>` determinável.

## Definition of Done

- implementação, testes e documentação concluídos;
- critérios de aceite e gates aplicáveis aprovados;
- Code Review e Security Review aplicável sem Critical/High;
- Secret Scan e Prompt Traceability aprovados;
- commit, push, PR, validação, merge em `develop` e cleanup concluídos.

## Quality Gates

Build Gate; Integration Test Gate; Contract Gate; Security Gate; Acceptance Gate; Code Review Gate.

## Status

BACKLOG
