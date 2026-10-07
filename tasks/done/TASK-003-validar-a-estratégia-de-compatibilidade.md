# TASK-003 — Validar a estratégia de compatibilidade

## Objetivo

Validar a estratégia de compatibilidade como incremento independente, verificável e alinhado à arquitetura aprovada.

## Epic

EPIC-12

## Contexto

Esta Task entrega uma unidade independente do EPIC-12 e segue a Architecture Baseline aprovada. Paralelismo: **PARALLEL SAFE após TASK-002**. Wave: **1**.

## Requirements

RF-032; RNF-019, RNF-024, RNF-025; SEC-027, SEC-029, SEC-032.

## ADRs

ADR-001, ADR-007, ADR-010.

## Dependências

TASK-001, TASK-002.

## Bloqueia

TASK-024, TASK-033, TASK-036, TASK-045, TASK-046, TASK-051.

## Prioridade

P0

## Complexidade

L

## Risco

HIGH

## Arquivos/áreas esperadas

matriz de compatibilidade; protótipos .NET/Windows/OBS; ADR-010.

## Implementação esperada

Executar o protótipo e reunir evidências para a matriz OBS 32.x, edições Windows 10/11 x64 e .NET 10. Atualizar ADR-010 para ACCEPTED ou registrar blocker sem ampliar a promessa de suporte.

## Segurança

Cobrir SEC-027, SEC-029, SEC-032. Tratar todos os dados externos como não confiáveis, aplicar least privilege e nunca usar secrets reais em testes ou artefatos.

## Testes obrigatórios

OBS COMPATIBILITY; INTEGRATION; smoke tests por ambiente declarado.

## Critérios de aceite

- A matriz testada e a versão mínima ficam explícitas; combinações desconhecidas falham fechado; a decisão Windows 10/.NET 10 é encaminhada antes de release.
- O escopo não introduz capability, provider ou infraestrutura fora da V1 aprovada.
- A documentação e a matriz de rastreabilidade são atualizadas quando o contrato mudar.

## Definition of Ready

- objetivo e limites revisados;
- Requirements/ADRs acima confirmados;
- dependências marcadas `DONE`;
- critérios e testes refinados sem blocker;
- branch `feature/task-TASK-003-<descricao>` determinável.

## Definition of Done

- implementação, testes e documentação concluídos;
- critérios de aceite e gates aplicáveis aprovados;
- Code Review e Security Review aplicável sem Critical/High;
- Secret Scan e Prompt Traceability aprovados;
- commit, push, PR, validação, merge em `develop` e cleanup concluídos.

## Quality Gates

Build Gate; OBS Compatibility Gate; Security Gate; Acceptance Gate; Documentation Gate; Code Review Gate.

## Status

DONE (2026-10-07)

## Execução (registro)

- Branch: `feature/task-TASK-003-validar-compatibilidade` a partir de `develop` (`d41acd7`).
- Protótipo read-only `prototypes/compat-sniff` criado e executado: SO `supported`, .NET `supported`, OBS `supported`, veredito `supported`, exit 0. Fatores no ambiente declarado: Windows 11 25H2 x64 (build 26200), .NET runtime 10.0.12, OBS 32.1.2 (`bin\64bit\obs64.exe`) com `obs-websocket.dll` presente.
- Matriz de compatibilidade estabelecida: `docs/architecture/compatibility/COMPATIBILITY_MATRIX.md` (baseline V1, versão mínima OBS 32.1.x com mínimo testado 32.1.2, combinações testadas, fail-closed, políticas Windows 10/.NET 10 encaminhadas antes do release).
- ADR-010 atualizado para **ACCEPTED** com registro de validação; sem blocker registrado e sem ampliar a promessa de suporte.
- Controles determinísticos adicionados (`CompatibilityMatrixTests` — 9 testes) no projeto `ObsAi.Architecture.Tests` (OBS Compatibility / INTEGRATION / smoke por ambiente declarado: evidência via execução do protótipo no ambiente real).
- Gates: build 0 avisos/0 erros; testes aprovados (9 arquitetura da Task + baseline); format verificado; Security Gate e Secret Scan sem secret/finding Critical/High; Code Review aprovado.
- Merge em `develop` concluído e validado; branches de feature limpas.
- Prompt Traceability: `docs/prompts/history/prompt19.md` no mesmo commit.
