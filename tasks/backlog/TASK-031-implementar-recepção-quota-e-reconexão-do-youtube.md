# TASK-031 — Implementar recepção, quota e reconexão do YouTube

## Objetivo

Implementar recepção, quota e reconexão do YouTube como incremento independente, verificável e alinhado à arquitetura aprovada.

## Epic

EPIC-07

## Contexto

Esta Task entrega uma unidade independente do EPIC-07 e segue a Architecture Baseline aprovada. Paralelismo: **SEQUENTIAL**. Wave: **5**.

## Requirements

RF-005 a RF-007, RF-027; RNF-005, RNF-009, RNF-011, RNF-017; SEC-001, SEC-002, SEC-016, SEC-018, SEC-024, SEC-033, SEC-034.

## ADRs

ADR-004.

## Dependências

TASK-009, TASK-013, TASK-030.

## Bloqueia

TASK-032.

## Prioridade

P0

## Complexidade

L

## Risco

HIGH

## Arquivos/áreas esperadas

streamList/list; polling; page tokens; reconnect; provider errors.

## Implementação esperada

Receber mensagens via capability aprovada, respeitar pollingInterval/quota, normalizar erros e reconectar com backoff limitado.

## Segurança

Cobrir SEC-001, SEC-002, SEC-016, SEC-018, SEC-024, SEC-033, SEC-034. Tratar todos os dados externos como não confiáveis, aplicar least privilege e nunca usar secrets reais em testes ou artefatos.

## Testes obrigatórios

CONTRACT; INTEGRATION; FAILURE; SECURITY.

## Critérios de aceite

- Chat encerrado, quota, forbidden, notFound e rede são distintos; retry não é infinito; mensagem inválida não alcança pipeline.
- O escopo não introduz capability, provider ou infraestrutura fora da V1 aprovada.
- A documentação e a matriz de rastreabilidade são atualizadas quando o contrato mudar.

## Definition of Ready

- objetivo e limites revisados;
- Requirements/ADRs acima confirmados;
- dependências marcadas `DONE`;
- critérios e testes refinados sem blocker;
- branch `feature/task-TASK-031-<descricao>` determinável.

## Definition of Done

- implementação, testes e documentação concluídos;
- critérios de aceite e gates aplicáveis aprovados;
- Code Review e Security Review aplicável sem Critical/High;
- Secret Scan e Prompt Traceability aprovados;
- commit, push, PR, validação, merge em `develop` e cleanup concluídos.

## Quality Gates

Build Gate; Contract Gate; Integration Test Gate; Failure Gate; Security Gate; Code Review Gate.

## Status

BACKLOG
