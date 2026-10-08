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

DONE (2026-10-07)

## Definition of Ready — evidência

- **Resultado:** PASSED em 2026-10-07.
- Objetivo, limites, critérios de aceite, risco e suites obrigatórias estão explícitos neste arquivo.
- Requirements `RF-007`, `RF-016`, `RF-019`, `RF-024`, `RF-027`, `RF-028`; `RNF-009` a `RNF-011`, `RNF-017` a `RNF-019`; `SEC-001`, `SEC-005`, `SEC-016` a `SEC-018`, `SEC-032` a `SEC-034` confirmados nas fontes canônicas.
- ADR-002, ADR-004 e ADR-011 estão `ACCEPTED`; decisões abertas de vendors e valores quantitativos não bloqueiam contracts vendor-neutral.
- Dependências `TASK-002` e `TASK-004` estão `DONE` e integradas em `develop`.
- Branch determinada: `feature/task-TASK-005-application-contracts-ports`; nenhum blocker Critical/High ou `ARCHITECTURE_BLOCKER` aberto.

## Plano de execução

1. Materializar contracts e ports mínimos em `ObsAi.Application`, sem adapters ou infraestrutura concreta.
2. Representar credenciais somente por referência opaca e falhas por classificação comum segura.
3. Modelar capabilities opcionais em interfaces separadas para preservar ISP/LSP.
4. Ativar testes Unit e Contract da TASK-005 e ampliar o Architecture Gate.
5. Atualizar documentação, rastreabilidade e evidências; executar todos os quality gates antes do PR.

## Evidência pré-merge

- Implementação: contracts/ports Chat, AI, TTS, persistência, secrets, OBS e observabilidade em `ObsAi.Application`, sem adapters concretos.
- Capabilities opcionais separadas por interface; credenciais somente por `CredentialReference`; falhas síncronas e streaming usam `ProviderResult<T>` normalizado.
- Quality Gates oficiais: restore, build, tests e format `PASSED` (exit 0); build com 0 avisos/0 erros; 65/65 testes aprovados (43 anteriores + 22 novos).
- Architecture Gate, Contract Gate, Unit Gate, Security Review e Secret Scan: `PASSED`; Secrets: `NONE`.
- Code Review: um finding Medium sobre falhas não normalizadas em streams foi corrigido; findings abertos Critical/High/Medium/Low: 0.
- Prompt Traceability: `docs/prompts/history/prompt21.md`.
- Commit de implementação: `8f27ec4425844a16b379a2bbb2b747528b64358e`.
- PR [#17](https://github.com/gfmaurila/OBS-AI-Live-Assistant/pull/17) mergeado em `develop` no commit `ed555be7343e95b1d18a7b45789428b79917ab2d`.
- Validação pós-merge no `develop`: restore/build/tests/format `PASSED`; 65/65 testes; 0 avisos/0 erros.
- Finalização administrativa, recálculo do grafo e cleanup executados por PR próprio, sem commit direto em `develop`.
