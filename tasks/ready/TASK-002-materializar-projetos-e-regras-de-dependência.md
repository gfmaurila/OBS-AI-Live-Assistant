# TASK-002 — Materializar projetos e regras de dependência

## Objetivo

Materializar projetos e regras de dependência como incremento independente, verificável e alinhado à arquitetura aprovada.

## Epic

EPIC-01

## Contexto

Esta Task entrega uma unidade independente do EPIC-01 e segue a Architecture Baseline aprovada. Paralelismo: **SEQUENTIAL**. Wave: **1**.

## Requirements

RNF-017, RNF-018, RNF-019, RNF-028; SEC-007, SEC-032.

## ADRs

ADR-004, ADR-011.

## Dependências

TASK-001.

## Bloqueia

TASK-003, TASK-004, TASK-005, TASK-006, TASK-019, TASK-024.

## Prioridade

P0

## Complexidade

M

## Risco

MEDIUM

## Arquivos/áreas esperadas

projetos `ObsAi.*`; referências entre projetos; composition roots.

## Implementação esperada

Criar os projetos aprovados em PROJECT_STRUCTURE.md, referências mínimas e regras de dependência, mantendo Domain e Application livres de adapters e SDKs externos.

## Segurança

Cobrir SEC-007, SEC-032. Tratar todos os dados externos como não confiáveis, aplicar least privilege e nunca usar secrets reais em testes ou artefatos.

## Testes obrigatórios

ARCHITECTURE; UNIT para convenções auxiliares.

## Critérios de aceite

- Todos os projetos possuem propósito documentado; referências proibidas falham em teste; nenhuma responsabilidade nova é inventada.
- O escopo não introduz capability, provider ou infraestrutura fora da V1 aprovada.
- A documentação e a matriz de rastreabilidade são atualizadas quando o contrato mudar.

## Definition of Ready

- objetivo e limites revisados;
- Requirements/ADRs acima confirmados;
- dependências marcadas `DONE`;
- critérios e testes refinados sem blocker;
- branch `feature/task-TASK-002-<descricao>` determinável.

## Definition of Done

- implementação, testes e documentação concluídos;
- critérios de aceite e gates aplicáveis aprovados;
- Code Review e Security Review aplicável sem Critical/High;
- Secret Scan e Prompt Traceability aprovados;
- commit, push, PR, validação, merge em `develop` e cleanup concluídos.

## Quality Gates

Build Gate; Architecture Gate; Documentation Gate; Code Review Gate; Secret Scan; GitFlow Gate.

## Status

READY
