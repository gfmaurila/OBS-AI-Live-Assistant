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

DONE (2026-10-07)

## Execução (registro)

- Branch: `feature/task-TASK-004-testing-architecture-quality-gates` a partir de `develop` (`1bbc0a7`).
- Fundação determinística estabelecida: `docs/testing/TEST_ARCHITECTURE.md` (fonte canônica), scaffolds por categoria em `tests/` (`Unit`, `Integration`, `Contracts`, `Security`, `FailureIsolation`, `Installer` — xunit, net10.0, packages centralizados, teste-âncora de governança, sem `ProjectReference`), e `ObsCompatibility` mantendo o harness existente (`prototypes/compat-sniff` + `CompatibilityMatrixTests`; suite formal em `TASK-051`) sem projeto novo.
- Tooling: `tooling/quality-gates.ps1` + `tooling/README.md` (runner determinístico dos comandos canônicos; sem instalação de ferramentas, sem secrets).
- Doc de comandos atualizada (`docs/testing/README.md`); `PROJECT_STRUCTURE.md` e `QUALITY_GATES.md` refletem o estado real.
- Controles determinísticos adicionados (`TestingFoundationTests` — 13 testes) no projeto `ObsAi.Architecture.Tests` validando a arquitetura de testes; as seis âncoras dos scaffolds garantem rastreabilidade da categoria na carta.
- Gates pré-merge e pós-merge finais: restore exit 0; build 0 avisos/0 erros; testes aprovados **43/43** (24 anteriores + 13 de fundação + 6 âncoras); format da solução e do protótipo exit 0; Architecture Gate e Acceptance Gate aprovados; Security Gate e Secret Scan sem secrets nem finding Critical/High; Code Review aprovado sem findings.
- Integração: PR [#14](https://github.com/gfmaurila/OBS-AI-Live-Assistant/pull/14) mergeado em `develop` (`078ffeed13f4f8399e5720e4ffa335b29a45e23c`). A primeira validação pós-merge detectou conversão CRLF por `core.autocrlf=true`; a correção durável `.gitattributes` (`*.cs text eol=lf`) foi validada e mergeada pelo PR [#15](https://github.com/gfmaurila/OBS-AI-Live-Assistant/pull/15) (`d5502775289289aa5ef233512c51fb5066bd03ee`). A validação pós-merge final passou integralmente em `develop`.
- Prompt Traceability: `docs/prompts/history/prompt20.md` no commit de implementação.
- A feature original local e remota foi removida após os merges e a validação final; nenhuma outra Task foi executada.
