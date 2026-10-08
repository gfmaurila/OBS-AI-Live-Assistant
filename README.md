# OBS-AI-Live-Assistant

OBS-AI-Live-Assistant é uma aplicação Windows independente e planejada para oferecer interação assistida por IA durante transmissões ao vivo no OBS Studio. A aplicação deverá receber solicitações de espectadores ou do streamer e produzir respostas configuráveis em texto e voz, mantendo a estabilidade do OBS independente de falhas de chat, IA, TTS, persistência ou rede.

## Estado atual

- Baseline documental: estabelecida.
- Base do Kit IA Dev: instalada.
- Advanced Skills: **BLOCKED / NON-BLOCKING FOR REQUIREMENTS**, pois o pacote oficial não está disponível.
- Knowledge Quality Gate: **PASSED**.
- Requirements, Security Requirements, Technical Research, Architecture e Backlog: **CONCLUÍDOS / QUALITY GATES PASSED**; Implementation Readiness: **READY**.
- **TASK-001 — Fundação da solution e do tooling: CONCLUÍDA** (solution, `global.json`, convenções comuns e diretórios `src/` e `tests/`).
- **TASK-002 — Materializar projetos e regras de dependência: CONCLUÍDA** (projetos `ObsAi.Domain`, `ObsAi.Application`, `ObsAi.Infrastructure`, `ObsAi.Providers`, `ObsAi.ObsIntegration`, `ObsAi.Host` + testes determinísticos de arquitetura em `tests/Architecture/`).
- **TASK-003 — Validar a estratégia de compatibilidade: CONCLUÍDA** (matriz de compatibilidade em `docs/architecture/compatibility/`, protótipo de evidência `prototypes/compat-sniff` e ADR-010 **ACCEPTED**).
- **TASK-004 — Estabelecer a arquitetura de testes e os quality gates: CONCLUÍDA** (carta canônica em `docs/testing/TEST_ARCHITECTURE.md`, projetos de teste por categoria e runner `tooling/quality-gates.ps1`).
- **TASK-005 — Definir contracts e ports da aplicação: CONCLUÍDA** (contracts/ports vendor-neutral em `ObsAi.Application`; suites Unit, Contract e Architecture aprovadas; merge e validação pós-merge concluídos).
- **TASK-006 — Implementar o modelo de domínio de sessão, perfil e contexto: CONCLUÍDA** (`ObsAi.Domain`; suites Unit, Architecture e Security aprovadas; merge e validação pós-merge concluídos).

## Direção tecnológica

A direção atual utiliza C# / .NET 10 em Windows 10/11 x64, com C/C++ somente quando uma capacidade nativa validada do OBS exigir. OBS Studio 32.x, SQLite para persistência local V1, Modular Monolith, Ports and Adapters, limites de providers, IA com BYOK, TTS multi-provider e prioridade para YouTube Live Chat são insumos de planejamento — não substituem Requirements, Research ou aprovação por ADR.

## Documentação

Comece pelo [índice da documentação do projeto](docs/README.md). Ele separa contexto atual, governança, conhecimento, backlog de pesquisa, segurança, testes, relatórios, histórico de prompts e artefatos planejados de arquitetura.

## GitFlow

O trabalho parte de `develop` em branches `feature/task-*`. Uma Task concluída com sucesso passa por validação, Code Review, push, pull request e merge em `develop` quando todos os gates estiverem aprovados. Promoções para `hml`, `release/1.0.0XXXX` e `main` possuem gates próprios e não são automáticas por Task. Consulte o [GitFlow](docs/governance/GITFLOW.md).

## Pendências controladas

- O pacote oficial das Advanced Skills não foi localizado; essa ausência não bloqueou Requirements nem Security Requirements.
- Integração OBS, IPC, SQLite, secrets, provider boundaries e compatibilidade (ADR-010) possuem ADRs aceitos; áudio e installer possuem direções propostas com validações explícitas; decisões de cliente não bloqueiam o Backlog.

Esta baseline documental não cria arquivos de produto nem dados de runtime do OBS.
