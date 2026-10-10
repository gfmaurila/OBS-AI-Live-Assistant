# TASK-024 — Criar skeleton e lifecycle do plugin OBS

## Objetivo

Criar skeleton e lifecycle do plugin OBS como incremento independente, verificável e alinhado à arquitetura aprovada.

## Epic

EPIC-06

## Contexto

Esta Task entrega uma unidade independente do EPIC-06 e segue a Architecture Baseline aprovada. Paralelismo: **PARALLEL SAFE com TASK-023**. Wave: **4**.

## Requirements

RF-001, RF-032; RNF-001, RNF-015, RNF-024 a RNF-026; SEC-007, SEC-020, SEC-021, SEC-027, SEC-029.

## ADRs

ADR-001, ADR-003, ADR-010.

## Dependências

TASK-002, TASK-003, TASK-004.

## Bloqueia

TASK-025, TASK-038, TASK-040, TASK-045.

## Prioridade

P0

## Complexidade

XL

## Risco

HIGH

## Arquivos/áreas esperadas

`ObsAi.ObsPlugin`; CMake; module lifecycle. Registrar apenas os eventos frontend explicitamente exigidos por esta Task e aprovados em seus requisitos; não antecipar Dock, áudio ou IPC.

## Implementação esperada

Criar plugin C++ x64 mínimo a partir do template, dependências e toolchain fixados e validados. Load/unload deve ser seguro e idempotente. Manter health somente em memória. Registrar obrigatoriamente `OBS_FRONTEND_EVENT_FINISHED_LOADING`; tratar `OBS_FRONTEND_EVENT_STREAMING_STARTED` e `OBS_FRONTEND_EVENT_STREAMING_STOPPED` apenas para atualizar estado em memória. Callbacks devem ser curtos, não bloqueantes e não fazer I/O síncrono.

## Segurança

Cobrir SEC-007, SEC-020, SEC-021, SEC-027, SEC-029. Tratar dados externos como não confiáveis, aplicar least privilege e nunca usar secrets reais em testes ou artefatos. Logs não podem incluir tokens, credenciais, conteúdo privado ou payloads desnecessários. Não adicionar dependência de IA, providers, banco de dados, rede ou secrets. Não incluir Qt, Dock, IPC ou áudio nesta Task. Falhas de inicialização e registro devem ser tratadas sem comprometer o OBS e sem callbacks pendentes após unload.

## Testes obrigatórios

Configure CMake reproduzível; build x64; testes do lifecycle; registro e remoção dos callbacks; tratamento seguro de falhas; smoke test isolado para cada versão OBS da matriz. Não declarar compatibilidade para versões não aprovadas nos testes.

## Critérios de aceite

- Configure CMake reproduzível com template, revisão, dependências, toolchain e hashes fixados e registrados.
- Build x64 concluído; testes verificam lifecycle, idempotência, registro/remoção dos callbacks e caminhos de falha.
- `FINISHED_LOADING` é registrado obrigatoriamente; eventos de streaming alteram somente estado em memória.
- Callbacks são curtos, não bloqueantes e não executam I/O síncrono; logs não contêm dados sensíveis.
- Plugin não contém lógica de negócio nem dependência de IA, providers, banco, rede ou secrets; Core ausente não bloqueia OBS.
- Sem Qt, Dock, IPC ou áudio nesta Task.
- Smoke test executado em OBS portátil isolado, separadamente para cada versão candidata; instalação principal e perfil real do OBS permanecem preservados.
- Compatibilidade declarada somente para versões OBS efetivamente aprovadas e evidenciadas nos testes.
- O escopo não introduz capability, provider ou infraestrutura fora da V1 aprovada.
- A documentação e a matriz de rastreabilidade são atualizadas quando o contrato mudar.

## Definition of Ready

- objetivo e limites revisados;
- Requirements/ADRs acima confirmados;
- dependências marcadas `DONE`;
- critérios e testes refinados sem blocker;
- branch `feature/task-TASK-024-<descricao>` determinável.

## Definition of Done

- implementação, testes e documentação concluídos;
- critérios de aceite e gates aplicáveis aprovados;
- Code Review e Security Review aplicável sem Critical/High;
- Secret Scan e Prompt Traceability aprovados;
- commit, push, PR, validação, merge em `develop` e cleanup concluídos.

## Quality Gates

Native Build Gate; OBS Compatibility Gate; Failure Gate; Security Gate; Architecture Gate; Code Review Gate.

## Status

BACKLOG
