# TASK-005 — Definir contracts e ports da aplicação

## Objetivo

Definir contracts e ports da aplicação como incremento independente, verificável e alinhado à arquitetura aprovada.

## Epic

EPIC-02

## Contexto

Esta Task entrega uma unidade independente do EPIC-02 e segue a Architecture Baseline aprovada. Paralelismo: **SEQUENTIAL**. Wave: **2**.

## Requirements

RF-007, RF-016, RF-019, RF-024, RF-027, RF-028; RNF-009 a RNF-011, RNF-017 a RNF-019; SEC-001, SEC-005, SEC-016 a SEC-018, SEC-032 a SEC-034.

## ADRs

ADR-002, ADR-004, ADR-011.

## Dependências

TASK-002, TASK-004.

## Bloqueia

TASK-007, TASK-008, TASK-009, TASK-013, TASK-014, TASK-015, TASK-016, TASK-017, TASK-020, TASK-022, TASK-027, TASK-030, TASK-033, TASK-036, TASK-043.

## Prioridade

P0

## Complexidade

L

## Risco

HIGH

## Arquivos/áreas esperadas

`ObsAi.Application`; contratos Chat/AI/TTS/persistence/secrets/OBS/observability.

## Implementação esperada

Implementar ports pequenos equivalentes a IChatProvider, IAiProvider e ITtsProvider, capabilities opcionais, erros normalizados e contratos para persistência, secrets, OBS e observabilidade.

## Segurança

Cobrir SEC-001, SEC-005, SEC-016 a SEC-018, SEC-032 a SEC-034. Tratar todos os dados externos como não confiáveis, aplicar least privilege e nunca usar secrets reais em testes ou artefatos.

## Testes obrigatórios

UNIT; ARCHITECTURE; CONTRACT.

## Critérios de aceite

- Contratos não expõem SDKs/vendors nem valores de secrets; capabilities não prometem comportamento ausente; adapters futuros podem ser substituídos.
- O escopo não introduz capability, provider ou infraestrutura fora da V1 aprovada.
- A documentação e a matriz de rastreabilidade são atualizadas quando o contrato mudar.

## Definition of Ready

- objetivo e limites revisados;
- Requirements/ADRs acima confirmados;
- dependências marcadas `DONE`;
- critérios e testes refinados sem blocker;
- branch `feature/task-TASK-005-<descricao>` determinável.

## Definition of Done

- implementação, testes e documentação concluídos;
- critérios de aceite e gates aplicáveis aprovados;
- Code Review e Security Review aplicável sem Critical/High;
- Secret Scan e Prompt Traceability aprovados;
- commit, push, PR, validação, merge em `develop` e cleanup concluídos.

## Quality Gates

Build Gate; Unit Test Gate; Architecture Gate; Contract Gate; Security Gate; Code Review Gate.

## Status

BACKLOG
