# Lifecycle de sessão e orquestração

Status: **IMPLEMENTADO PELA TASK-007**.

Este documento registra o contrato de lifecycle do Assistant Core: estados explícitos do runtime, admissão de operações por lease, shutdown ordenado e descarte atômico de resultados tardios. A implementação reside em `ObsAi.Application/Lifecycle`, depende apenas dos tipos de `ObsAi.Domain` (sessão, perfil e contexto) e não referencia adapters, persistência, providers, rede, secrets ou integração com o OBS.

## Estados do runtime

`AssistantRuntimeState` é explícito e mutável apenas pelo orquestrador:

| Estado | Significado |
|---|---|
| `Stopped` | Sem sessão e sem admissão; é o estado inicial e o estado final do shutdown. |
| `Running` | Sessão ativa; novas operações são admitidas. |
| `Paused` | Sessão ativa com admissão fechada; trabalho em voo continua válido. |
| `Stopping` | Shutdown em andamento; admissão fechada e trabalho em cancelamento. |

Transições válidas: `Stopped -> Running` (`StartSession`), `Running <-> Paused` (`Pause`/`Resume`), `Running|Paused|Stopping -> Stopped` (`Shutdown`). Transição inválida lança `InvalidOperationException` e não altera estado; um start rejeitado não cria autoridade nem estado parcial.

## Sessão e geração

- `StartSession(sessionId, profileId, liveContext, startedAtUtc)` cria a sessão pelo modelo de domínio (`AssistantSession.Start`), assume a propriedade dela e incrementa a geração do runtime.
- A sessão atual nunca é exposta pelo orquestrador: somente `CurrentSessionId`. Nenhum chamador pode encerrar ou migrar a sessão por trás da autoridade do orquestrador.
- `generation` identifica a vigência da sessão atual; um lease de uma geração anterior nunca pode publicar.

## Admissão e lease

- `TryAcquireOperation(sessionId)` é o único ponto de admissão, independente da origem da operação (chat, manual ou interno).
- Rejeição é sempre explícita e nunca altera o runtime: `RuntimeStopping`, `RuntimeStopped`, `SessionMismatch` ou `RuntimePaused`.
- `OperationLease` carrega `SessionId`, `OperationId`, `Generation`, um `CancellationToken` próprio e os callbacks de cancelamento/liberação do orquestrador. A classe não possui construtor público: autoridade só nasce na admissão.
- Cada operação ativa ocupa uma vaga em `InFlightOperationCount`, que volta a zero quando o lease é liberado (commit, cancelamento ou shutdown).

## Publicação de resultados

- `TryCommitResult(lease, result, commit)` valida a autoridade do lease sob o mesmo lock da admissão: não pode estar liberado, não pode ter sido cancelado, a geração e a sessão têm que ser as atuais, e o runtime tem que estar `Running` ou `Paused`.
- Somente a primeira tentativa aceita executa `commit` e libera o lease; resultado tardio, cancelado, de sessão anterior ou de geração anterior é descartado e reportado como não commitado.
- Uma exceção lançada pelo próprio `commit` não é engolida: o runtime permanece utilizável e o lease segue em voo, permitindo nova tentativa.

## Pausa

`Pause()` fecha a admissão, mas preserva a validade do trabalho em voo: leases existentes seguem canceláveis e podem publicar. `Resume()` reabre a admissão. Pausa em estado diferente de `Running` é rejeitada.

## Shutdown ordenado

`Shutdown(endedAtUtc)` é idempotente e segue uma ordem única:

1. valida timestamp UTC e cronologia frente ao início da sessão;
2. transita para `Stopping`, fechando a admissão;
3. cancela cada lease ativo e libera sua vaga, isolando falhas de observadores de cancelamento;
4. encerra a sessão pelo domínio (`AssistantSession.End`), limpando e invalidando o `LiveContext`;
5. incrementa a geração e assenta em `Stopped`.

Um observador de cancelamento defeituoso nunca interrompe a sequência nem impede o runtime de chegar a `Stopped`; leases remanescentes são liberados mesmo quando o cancelamento falha.

## Limites e configuração

O orquestrador não define números de produto nem políticas de autorização de ações: ele delimita autoridade de sessão e estado. Filas, backpressure, autorização de comandos OBS, persistência e configuração pertencem às Tasks responsáveis.

## Fora de escopo

- filas bounded e backpressure (`TASK-008`);
- validação e normalização de entradas (`TASK-009`);
- construção operacional de contexto e Short-Term Memory (`TASK-011`);
- persistência e store de sessão (`TASK-019` a `TASK-021`);
- IPC com o plugin e contratos versionados (`TASK-022`);
- providers, rede, TTS, chat ou comandos OBS.

## Rastreabilidade

| Critério da TASK-007 | Implementação | Evidência automatizada |
|---|---|---|
| Iniciar, pausar, retomar e encerrar sem depender do OBS | `StartSession`, `Pause`, `Resume`, `Shutdown` | `AssistantSessionOrchestratorTests`, `SessionLifecycleFlowTests` |
| Pausa bloqueia novas chamadas e preserva trabalho em voo | `EvaluateAdmission` e `CanCommit` | `AssistantSessionOrchestratorTests`, `SessionLifecycleFlowTests` |
| Shutdown cancela trabalho e impede saídas tardias | `Shutdown`, `TryCommitResult` | `SessionLifecycleFlowTests`, `SessionLifecycleSecurityTests` |
| Falha do Core não cria autoridade ou estado ambíguo | rejeições explícitas e transição de estado atômica | `SessionLifecycleFailureTests` |
| Cancelamento propagado com segurança e sem ampliar privilégio | `OperationLease.Cancel`, `CancelLease`, `ReleaseLease` | `SessionLifecycleSecurityTests` |
| Sessão e contexto isolados; lease de sessão anterior inutilizável | `generation`, `SessionId` e `CurrentSessionId` | `SessionLifecycleSecurityTests`, `DomainDataMinimizationTests` |
| Superfície sem autoridade forjável | sem construtor público em `OperationLease`; sessão não exposta | `ApplicationLifecycleArchitectureTests` |

Requisitos cobertos: RF-001, RF-009, RF-026, RF-029; RNF-001, RNF-002, RNF-010, RNF-026; SEC-017, SEC-020, SEC-021 e SEC-031. Decisões aplicadas: ADR-003 (isolamento, lifecycle e filas) e ADR-009 (memória efêmera por sessão).
