# TASK-017 — Implementar Secure Credential Store do Windows

## Objetivo

Implementar Secure Credential Store do Windows como incremento independente, verificável e alinhado à arquitetura aprovada.

## Epic

EPIC-03

## Contexto

Esta Task entrega uma unidade independente do EPIC-03 e segue a Architecture Baseline aprovada. Paralelismo: **PARALLEL SAFE com persistence após TASK-016**. Wave: **3**.

## Requirements

RF-018; RNF-003, RNF-006; SEC-008 a SEC-013, SEC-022, SEC-033.

## ADRs

ADR-005.

## Dependências

TASK-005, TASK-016.

## Bloqueia

TASK-018, TASK-027, TASK-034, TASK-037.

## Prioridade

P0

## Complexidade

L

## Risco

HIGH

## Arquivos/áreas esperadas

Credential Manager; DPAPI user-scope; secret references.

## Implementação esperada

Implementar o Secret Store port com Windows Credential Manager e DPAPI user-scope apenas para blobs justificados, mantendo referências opacas fora do Domain.

## Segurança

Cobrir SEC-008 a SEC-013, SEC-022, SEC-033. Tratar todos os dados externos como não confiáveis, aplicar least privilege e nunca usar secrets reais em testes ou artefatos.

## Testes obrigatórios

UNIT; INTEGRATION; SECURITY; lifecycle.

## Critérios de aceite

- Write/read/replace/delete são verificáveis por usuário; nenhum plaintext entra em SQLite/config/log; machine-scope não é default.
- O escopo não introduz capability, provider ou infraestrutura fora da V1 aprovada.
- A documentação e a matriz de rastreabilidade são atualizadas quando o contrato mudar.

## Definition of Ready

- objetivo e limites revisados;
- Requirements/ADRs acima confirmados;
- dependências marcadas `DONE`;
- critérios e testes refinados sem blocker;
- branch `feature/task-TASK-017-<descricao>` determinável.

## Definition of Done

- implementação, testes e documentação concluídos;
- critérios de aceite e gates aplicáveis aprovados;
- Code Review e Security Review aplicável sem Critical/High;
- Secret Scan e Prompt Traceability aprovados;
- commit, push, PR, validação, merge em `develop` e cleanup concluídos.

## Quality Gates

Build Gate; Integration Test Gate; Security Gate; Acceptance Gate; Code Review Gate; Secret Scan.

## Status

BACKLOG
