# TASK-004 — Estabelecer a arquitetura de testes e os quality gates

## Objetivo

Estabelecer a arquitetura de testes e os quality gates como incremento independente, verificável e alinhado à arquitetura aprovada.

## Epic

EPIC-13

## Contexto

Esta Task entrega uma unidade independente do EPIC-13 e segue a Architecture Baseline aprovada. Paralelismo: **PARALLEL SAFE após TASK-002**. Wave: **1**.

## Requirements

RNF-019, RNF-028; SEC-029, SEC-032.

## ADRs

ADR-010, ADR-011.

## Dependências

TASK-001, TASK-002.

## Bloqueia

TASK-005, TASK-006, TASK-019, TASK-024, TASK-048.

## Prioridade

P0

## Complexidade

M

## Risco

MEDIUM

## Arquivos/áreas esperadas

projetos de teste; tooling; scripts de validação; documentação de comandos.

## Implementação esperada

Criar a fundação determinística para testes unitários, integração, arquitetura, contratos, segurança, IPC, falhas, compatibilidade e installer, sem testes fictícios.

## Segurança

Cobrir SEC-029, SEC-032. Tratar todos os dados externos como não confiáveis, aplicar least privilege e nunca usar secrets reais em testes ou artefatos.

## Testes obrigatórios

UNIT; INTEGRATION; ARCHITECTURE; CONTRACT; SECURITY.

## Critérios de aceite

- Cada categoria possui projeto ou harness justificável; comandos executados são documentados; gates ainda não aplicáveis permanecem identificados.
- O escopo não introduz capability, provider ou infraestrutura fora da V1 aprovada.
- A documentação e a matriz de rastreabilidade são atualizadas quando o contrato mudar.

## Definition of Ready

- objetivo e limites revisados;
- Requirements/ADRs acima confirmados;
- dependências marcadas `DONE`;
- critérios e testes refinados sem blocker;
- branch `feature/task-TASK-004-<descricao>` determinável.

## Definition of Done

- implementação, testes e documentação concluídos;
- critérios de aceite e gates aplicáveis aprovados;
- Code Review e Security Review aplicável sem Critical/High;
- Secret Scan e Prompt Traceability aprovados;
- commit, push, PR, validação, merge em `develop` e cleanup concluídos.

## Quality Gates

Build Gate; Unit Test Gate; Architecture Gate; Documentation Gate; Code Review Gate; Secret Scan.

## Status

BACKLOG
