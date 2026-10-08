# Tasks de implementação da V1

Este diretório transforma a documentação aprovada em backlog executável. A autoridade de produto continua nos Requirements; a autoridade técnica continua na Architecture Baseline e nos ADRs; este diretório define unidades de entrega, dependências, estados e gates.

## Estado

- Backlog Quality Gate: **PASSED**
- Security Review: **PASSED**
- Implementation Readiness: **READY**
- Tasks: **52**
- Tasks DONE: **5** (`TASK-001` a `TASK-005`)
- Tasks IN_PROGRESS: **1** (`TASK-006`)
- Tasks READY: **0** formalizada (candidata a READY com dependências concluídas: **`TASK-024`** — sem execução)
- Tasks REVIEW: **0**
- Dependency Cycles: **0**
- Product Source Code: **PARTIAL** (`ObsAi.Application` com contracts/ports e `ObsAi.Domain` com sessão, perfil e contexto efêmero; demais funcionalidades e adapters ainda não iniciados)
- Implementation: **IN PROGRESS** (`TASK-001` a `TASK-005` concluídas em `develop`; `TASK-006` em execução)

## Navegação

- [Backlog consolidado](BACKLOG.md)
- [Dependency Graph](DEPENDENCY_GRAPH.md)
- [Ordem e Waves](IMPLEMENTATION_ORDER.md)
- [Matriz de rastreabilidade](TRACEABILITY_MATRIX.md)
- [Definition of Ready](DEFINITION_OF_READY.md)
- [Definition of Done](DEFINITION_OF_DONE.md)
- [Release Plan](RELEASE_PLAN.md)
- [Relatório](../docs/reports/BACKLOG_REPORT.md)

As Tasks ficam em uma única pasta de estado: `backlog/`, `ready/`, `in-progress/`, `review/`, `blocked/` ou `done/`. Não se duplica arquivo entre estados.

## Epics

| Epic | Nome | Propósito | Tasks |
|---|---|---|---:|
| EPIC-01 | Foundation | Solution, convenções e estrutura física. | 2 |
| EPIC-02 | Domain / Application Core | Domínio, orchestration, filas, contexto e pipeline. | 9 |
| EPIC-03 | Security Foundation | Authorization, redaction, secrets e BYOK. | 4 |
| EPIC-04 | Persistence / Configuration | Configuração, SQLite, migrations e recovery. | 4 |
| EPIC-05 | IPC | Contrato e peers Named Pipes protegidos. | 4 |
| EPIC-06 | OBS Native Integration | Plugin mínimo, obs-websocket e isolamento. | 4 |
| EPIC-07 | Chat Integration | YouTube Live Chat e fluxo textual/manual. | 3 |
| EPIC-08 | AI Providers | Seleção, adapter e conformance de IA. | 3 |
| EPIC-09 | TTS / Audio | Seleção, adapter, protótipo e áudio aprovado. | 4 |
| EPIC-10 | OBS Dock / UI | Dock, controles e acessibilidade. | 3 |
| EPIC-11 | Observability / Diagnostics | Logging, health e métricas locais. | 2 |
| EPIC-12 | Compatibility / Installer / Update | Matriz, protótipo, installer e supply chain. | 5 |
| EPIC-13 | Testing / Quality | Fundação e consolidação das suites. | 2 |
| EPIC-14 | Integration / Hardening | E2E, failure isolation, segurança e desempenho. | 2 |
| EPIC-15 | Release Preparation | Evidências e readiness da V1. | 1 |

## Project → componente → rastreabilidade

| Projeto planejado | Componente | Requirements principais | ADRs | Tasks principais |
|---|---|---|---|---|
| `ObsAi.Domain` | Session, LiveContext, Profile e policies puras | RF-003, RF-004, RF-015, RF-029; RNF-018 | ADR-009, ADR-011 | TASK-006, TASK-010, TASK-011, TASK-015 |
| `ObsAi.Application` | Use cases, ports, pipeline e queues | RF-001, RF-007 a RF-027; RNF-008 a RNF-019 | ADR-002 a ADR-004, ADR-008, ADR-011 | TASK-005, TASK-007 a TASK-013 |
| `ObsAi.Infrastructure` | SQLite, secret store, configuração e logging | RF-002, RF-018, RF-028 a RF-031 | ADR-004, ADR-005, ADR-008, ADR-009 | TASK-014, TASK-016 a TASK-021, TASK-043, TASK-044 |
| `ObsAi.Providers` | YouTube, AI e TTS adapters | RF-005 a RF-007, RF-016 a RF-025 | ADR-004, ADR-006 | TASK-030 a TASK-037 |
| `ObsAi.ObsIntegration` | obs-websocket, IPC e policy OBS | RF-001, RF-022, RF-026, RF-027, RF-036 | ADR-001, ADR-002 | TASK-022, TASK-023, TASK-026 a TASK-029 |
| `ObsAi.Host` | Processo, composition, lifecycle e health | RF-001, RF-027, RF-031; RNF-001, RNF-026 | ADR-003, ADR-008 | TASK-007, TASK-029, TASK-044 |
| `ObsAi.ObsPlugin` | Dock, lifecycle, IPC e áudio validado | RF-001, RF-023 a RF-025, RF-031, RF-032 | ADR-001, ADR-002, ADR-006, ADR-010 | TASK-024, TASK-025, TASK-038 a TASK-042 |

## Regra de execução

Selecionar a Task `READY` de maior prioridade, validar dependências, criar `feature/task-TASK-XXX-<descricao>` e executar a Task integralmente até merge em `develop` e cleanup. Uma Task só muda para `READY` conforme a [Definition of Ready](DEFINITION_OF_READY.md).

Twitch, Persistent Memory, RAG, microservices, brokers externos, cloud própria, runtime multi-agent e automatic narrator permanecem fora deste backlog V1.
