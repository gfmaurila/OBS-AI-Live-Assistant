# TASK-001 — Criar a fundação da solution e do tooling

## Objetivo

Criar a fundação da solution e do tooling como incremento independente, verificável e alinhado à arquitetura aprovada.

## Epic

EPIC-01

## Contexto

Esta Task entrega uma unidade independente do EPIC-01 e segue a Architecture Baseline aprovada. Paralelismo: **SEQUENTIAL**. Wave: **1**.

## Requirements

RNF-018, RNF-019, RNF-028; SEC-029, SEC-032.

## ADRs

ADR-010, ADR-011.

## Dependências

Nenhuma.

## Bloqueia

TASK-002, TASK-003, TASK-004.

## Prioridade

P0

## Complexidade

M

## Risco

MEDIUM

## Arquivos/áreas esperadas

solution; `global.json`; `Directory.Build.props`; `Directory.Packages.props`; `.editorconfig`; `src/`; `tests/`.

## Implementação esperada

Criar a solution .NET 10, as convenções comuns e os diretórios aprovados, sem adicionar funcionalidades de produto. Registrar comandos reais de restore, build e format somente após validá-los.

## Segurança

Cobrir SEC-029, SEC-032. Tratar todos os dados externos como não confiáveis, aplicar least privilege e nunca usar secrets reais em testes ou artefatos.

## Testes obrigatórios

ARCHITECTURE; validação determinística de restore/build/format.

## Critérios de aceite

- A solution restaura e compila de forma reproduzível; a estrutura respeita PROJECT_STRUCTURE.md; AGENTS.md passa a registrar somente comandos executados com sucesso.
- O escopo não introduz capability, provider ou infraestrutura fora da V1 aprovada.
- A documentação e a matriz de rastreabilidade são atualizadas quando o contrato mudar.

## Definition of Ready

- objetivo e limites revisados;
- Requirements/ADRs acima confirmados;
- dependências marcadas `DONE`;
- critérios e testes refinados sem blocker;
- branch `feature/task-TASK-001-<descricao>` determinável.

## Definition of Done

- implementação, testes e documentação concluídos;
- critérios de aceite e gates aplicáveis aprovados;
- Code Review e Security Review aplicável sem Critical/High;
- Secret Scan e Prompt Traceability aprovados;
- commit, push, PR, validação, merge em `develop` e cleanup concluídos.

## Quality Gates

Build Gate; Architecture Gate; Documentation Gate; Code Review Gate; Secret Scan; Prompt Traceability; GitFlow Gate.

## Status

DONE

## Execução

- Data: 2026-10-07
- Branch: `feature/task-TASK-001-foundation`
- Solution: `OBS-AI-Live-Assistant.slnx` (vazia; projetos em TASK-002)
- `.NET`: 10.0.401 (SDK fixado em `global.json`)
- Criações: `global.json`; `Directory.Build.props`; `Directory.Packages.props`; `.editorconfig`; `src/`; `tests/`
- Restore/Build/Test/Format: PASSED (comandos registrados em `AGENTS.md`)
- Architecture Gate: PASSED (nenhum projeto, nenhuma dependência, nenhuma capability fora da V1)
- Security Gate: PASSED; Secrets: **NONE**
- Code Review: APROVADO; Critical 0, High 0, Medium 0, Low 0
- Acceptance Criteria: PASSED
- Definition of Done: PASSED
- Prompt arquivado: `docs/prompts/history/prompt17.md`
- Merge em `develop`: CONCLUÍDO
