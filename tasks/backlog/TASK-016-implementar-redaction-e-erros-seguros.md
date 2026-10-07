# TASK-016 — Implementar redaction e erros seguros

## Objetivo

Implementar redaction e erros seguros como incremento independente, verificável e alinhado à arquitetura aprovada.

## Epic

EPIC-03

## Contexto

Esta Task entrega uma unidade independente do EPIC-03 e segue a Architecture Baseline aprovada. Paralelismo: **PARALLEL SAFE com TASK-015**. Wave: **2**.

## Requirements

RF-018, RF-031; RNF-003, RNF-005, RNF-013; SEC-008, SEC-010, SEC-011, SEC-026.

## ADRs

ADR-005, ADR-008.

## Dependências

TASK-005, TASK-014.

## Bloqueia

TASK-012, TASK-017, TASK-043.

## Prioridade

P0

## Complexidade

M

## Risco

HIGH

## Arquivos/áreas esperadas

data classification; redaction; masking; error mapping.

## Implementação esperada

Criar redaction central, allowlist de campos e erros seguros aplicados antes de logs, UI, diagnósticos e respostas.

## Segurança

Cobrir SEC-008, SEC-010, SEC-011, SEC-026. Tratar todos os dados externos como não confiáveis, aplicar least privilege e nunca usar secrets reais em testes ou artefatos.

## Testes obrigatórios

UNIT; SECURITY com secrets sentinela.

## Critérios de aceite

- Valores sentinela não aparecem em nenhum sink; elevar log level não desativa redaction; erros de provider não ecoam dados sensíveis.
- O escopo não introduz capability, provider ou infraestrutura fora da V1 aprovada.
- A documentação e a matriz de rastreabilidade são atualizadas quando o contrato mudar.

## Definition of Ready

- objetivo e limites revisados;
- Requirements/ADRs acima confirmados;
- dependências marcadas `DONE`;
- critérios e testes refinados sem blocker;
- branch `feature/task-TASK-016-<descricao>` determinável.

## Definition of Done

- implementação, testes e documentação concluídos;
- critérios de aceite e gates aplicáveis aprovados;
- Code Review e Security Review aplicável sem Critical/High;
- Secret Scan e Prompt Traceability aprovados;
- commit, push, PR, validação, merge em `develop` e cleanup concluídos.

## Quality Gates

Build Gate; Unit Test Gate; Security Gate; Documentation Gate; Code Review Gate; Secret Scan.

## Status

BACKLOG
