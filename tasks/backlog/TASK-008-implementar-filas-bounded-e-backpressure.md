# TASK-008 — Implementar filas bounded e backpressure

## Objetivo

Implementar filas bounded e backpressure como incremento independente, verificável e alinhado à arquitetura aprovada.

## Epic

EPIC-02

## Contexto

Esta Task entrega uma unidade independente do EPIC-02 e segue a Architecture Baseline aprovada. Paralelismo: **SEQUENTIAL**. Wave: **2**.

## Requirements

RF-013, RF-021, RF-025; RNF-008, RNF-016; SEC-014, SEC-015, SEC-019, SEC-021.

## ADRs

ADR-003.

## Dependências

TASK-005, TASK-007.

## Bloqueia

TASK-010, TASK-012, TASK-013, TASK-037, TASK-044.

## Prioridade

P0

## Complexidade

L

## Risco

HIGH

## Arquivos/áreas esperadas

request queue; response queue; TTS queue; concurrency policies.

## Implementação esperada

Implementar filas locais finitas, concorrência configurável, saturação explícita e fechamento coordenado, sem broker externo.

## Segurança

Cobrir SEC-014, SEC-015, SEC-019, SEC-021. Tratar todos os dados externos como não confiáveis, aplicar least privilege e nunca usar secrets reais em testes ou artefatos.

## Testes obrigatórios

UNIT; INTEGRATION; FAILURE; SECURITY; performance controlado.

## Critérios de aceite

- Carga excedente não expande memória; saturação é observável; uma fila lenta não bloqueia as demais; limites inválidos são rejeitados.
- O escopo não introduz capability, provider ou infraestrutura fora da V1 aprovada.
- A documentação e a matriz de rastreabilidade são atualizadas quando o contrato mudar.
- RNF-008 `REQUIRES DECISION`: capacidades e concurrency por estágio são configuráveis via `WorkQueueSettings`; esta Task entrega o mecanismo e não fixa números de produto (decisão adiada para a TASK-014).

## Plano de implementação

Incremento entregue em `src/ObsAi.Application/Queues` (namespace `ObsAi.Application.Queues`), session-agnostic e vendor-neutral:

1. Domínio das filas: `QueueKind` (Request/Response/Tts), `SaturationPolicy` (Reject/DiscardOldest) e `QueueEnqueueStatus` (Accepted/RejectedFull/DroppedOldest/Closed), com `WorkQueueSettings.Create` que valida e congela `capacity >= 1`, `maxConcurrency >= 1` e enums definidos (limites inválidos são rejeitados).
2. Buffer bounded genérico: `BoundedWorkBuffer<T>` (via `IWorkBuffer`) — FIFO estrito, `Capacity` fixo, políticas não bloqueantes, contadores `TotalAccepted`/`TotalRejected`/`TotalDropped` e fechamento com drain. Decisão: sem `System.Threading.Channels` (empacotamento extra, item descartado não reportável, sem contadores) e sem espera assíncrona por capacidade (fila ilimitada de produtores bloquearia memória — violaria SEC-019/RNF-008).
3. Concorrência configurável: `WorkQueueRunner<T>` (via `IWorkQueueRunner`) — `Start` idempotente, `MaxConcurrency` efetivo, falha de item isolada (`TotalItemFailures` + `onItemFailure` sem derrubar o runner), `ShutdownAsync` coordenado e idempotente sem trabalho órfão.
4. Isolamento: cada `QueueKind` tem buffer/runners próprios; superfície observável não expõe conteúdo de itens nem autoridade de sessão.

## Matriz critério -> teste

| Critério | Teste determinístico |
|---|---|
| Carga excedente não expande memória | `QueueSecurityTests` (flood adversarial), `BoundedWorkBufferTests`.Enqueue overflow, `QueueFlowTests` |
| Saturação observável | `BoundedWorkBufferTests`, `QueueFlowTests` (contadores Accepted/Rejected/Dropped) |
| Uma fila lenta não bloqueia as demais | `QueueFlowTests.SlowStageQueue_DoesNotBlockIndependentStageQueues` |
| Limites inválidos rejeitados | `WorkQueueSettingsTests` |
| Concurrency máximo efetivo | `WorkQueueRunnerTests` (pico de `InFlightCount` <= `MaxConcurrency`) |
| Falha de item não derruba o runner | `QueueFailureTests`, `WorkQueueRunnerTests` |
| Fechamento coordenado sem trabalho órfão | `WorkQueueRunnerTests`, `QueueFailureTests` |
| Superfície sem conteúdo/authority e enums/limites válidos | `QueueSecurityTests`, `ApplicationQueueArchitectureTests` (0/1 dependency rule, allowlist) |

Evidence executions: 166/166 testes determinísticos verdes; quality gates RESTORE/BUILD/TESTES/FORMAT `PASSED`; Architecture Gate e Security Review `PASSED`; Code Review sem Critical/High.

## Definition of Ready

- objetivo e limites revisados;
- Requirements/ADRs acima confirmados;
- dependências marcadas `DONE`;
- critérios e testes refinados sem blocker;
- branch `feature/task-TASK-008-<descricao>` determinável.

## Definition of Done

- implementação, testes e documentação concluídos;
- critérios de aceite e gates aplicáveis aprovados;
- Code Review e Security Review aplicável sem Critical/High;
- Secret Scan e Prompt Traceability aprovados;
- commit, push, PR, validação, merge em `develop` e cleanup concluídos.

## Quality Gates

Build Gate; Unit Test Gate; Integration Test Gate; Security Gate; Performance Gate; Code Review Gate.

## Status

EM IMPLEMENTAÇÃO
