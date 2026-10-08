# Filas bounded e backpressure

Status: **IMPLEMENTADO PELA TASK-008**.

Este documento registra o contrato de filas bounded e backpressure do Assistant Core: buffers FIFO de memória limitada por estágio, políticas de saturação explícitas, concurrency configurável e shutdown coordenado. A implementação reside em `ObsAi.Application/Queues` e é intencionalmente **session-agnostic** e **vendor-neutral**: não referencia sessão, perfil, contexto, providers, persistência, rede, secrets ou integração com o OBS.

## Topologia (ADR-003)

Cada estágio do pipeline possui seu próprio buffer independente, conforme ADR-003:

| QueueKind | Estágio | Requirement | Driver |
|---|---|---|---|
| `Request` | Pedidos aprovados aguardando processamento | RF-013 | admission control |
| `Response` | Respostas aprovadas aguardando saída textual e de voz | RF-021 | output coordination |
| `Tts` | Sínteses aprovadas aguardando reprodução | RF-025 | speech synthesis output |

Os buffers são locais ao processo do Assistant Core: não há broker, IPC ou storage entre estágios. Duas filas nunca compartilham estado; uma fila lenta ou falha ocupa apenas os seus próprios slots e não bloqueia os demais estágios.

## Contracto do buffer

`BoundedWorkBuffer<T>` (via `IWorkBuffer`) oferece:

- **Memória estritamente limitada**: `Capacity` fixo por settings validados; o tamanho observado (`Count`) nunca excede `Capacity` mesmo sob carga adversária contínua.
- **FIFO**: `TryDequeue` retorna o item pendente mais antigo; `DiscardOldest` remove exatamente o mais antigo para admitir o novo.
- **Saturação observável**: `TotalAccepted`, `TotalRejected` e `TotalDropped` permitem medir carga excedente sem expor o conteúdo dos itens.
- **Fechamento coordenado**: `Complete()` impede novas admissões, mantém itens pendentes drenáveis e faz `WaitToDequeueAsync` terminar quando drenado.

### Políticas de saturação

| Política | Comportamento quando cheia | Outcome |
|---|---|---|
| `Reject` | o novo item é recusado; memória permanece constante | `RejectedFull` |
| `DiscardOldest` | o item mais antigo é removido e o novo é admitido; memória permanece constante | `DroppedOldest` com `DroppedItem` |

`EnqueueOutcome<T>` é o resultado explícito de cada tentativa: `Accepted`, `RejectedFull`, `DroppedOldest` ou `Closed`. `IsAccepted` é verdadeiro para `Accepted` e `DroppedOldest` — no descarte o item do produtor **foi** enfileirado; tratar isso como falha e reintentar duplicaria trabalho.

### Decisão: por que não espera assíncrona por capacidade

Foi considerada uma política de saturação que fizesse o produtor **aguardar** até haver espaço (`WaitForCapacityAsync`). Essa opção foi **descartada**: produtores bloqueados formariam uma fila de espera potencialmente ilimitada de `TaskCompletionSource`, o que violaria o critério de que carga excedente não expande memória (SEC-019, RNF-008). A saturação aqui é sempre **não bloqueante e explícita**: `Reject` para descarte imediato, `DiscardOldest` para manter a janela mais recente.

### Decisão: por que não `System.Threading.Channels`

A biblioteca BCL `System.Threading.Channels` é uma candidata natural, mas foi avaliada e **não adotada**: é um pacote runtime externo adicional ao `ObsAi.Application`, impõe um modelo de escrita/leitura que dificulta relatar o item descartado (`TryWrite` não devolve o item removido), e não expõe contadores de saturação. O buffer próprio mantém dependências mínimas (RNF-017, SEC-029) e dá visibilidade determinística dos três contadores, mantendo o mesmo assíncrono sem busy-wait.

## Runner e concurrency

`WorkQueueRunner<T>` executa os itens de um `BoundedWorkBuffer<T>` com um pool de workers limitado:

- `Start()` é idempotente (mais de uma chamada é rejeitada).
- `MaxConcurrency` é o limite efetivo: o pico de itens simultâneos nunca o excede; um item lento ocupa apenas a sua vaga.
- `ShutdownAsync(ct)` executa o fechamento coordenado: `Complete()` no buffer, token de cancelamento propagado a cada item, drain dos pendentes quando `ct` não cancela, e retorno quando todos os workers terminaram — sem trabalho órfão. Chamadas múltiplas retornam a mesma tarefa.
- Falhas de `handler` são isoladas por item: incrementam `TotalItemFailures`, chamam `onItemFailure` e não derrubam o runner (SEC-020, SEC-021).
- `Dispose()` cancela o token de shutdown; o chamador deve aguardar `ShutdownAsync` antes de dispor para não abandonar trabalho.

Superfície observável (`IWorkQueueRunner`): `InFlightCount`, `PendingCount`, `TotalProcessed`, `TotalItemFailures`, `IsShutdownRequested`.

## Configuração e limites

`WorkQueueSettings.Create(kind, capacity, maxConcurrency, saturationPolicy)` valida e congela todos os limites:

- `capacity >= 1` e `maxConcurrency >= 1`; enums definidos; tudo o mais lança `ArgumentOutOfRangeException`.
- O registro é imutável e não possui setters públicos.
- Os valores numéricos de produto por estágio **não são fixados pela arquitetura** (RNF-008): capacidades e concurrency são configuráveis pelo streamer numa Task futura de configuração (TASK-014); esta Task entrega o mecanismo, não números de produto.

## Isolamento

- Os buffers são **session-agnostic por design**: o isolamento de sessão é responsabilidade do orquestrador/lifecycle de lease (TASK-007). A camada de filas transmite trabalho, não autoridade.
- A superfície observável não expõe conteúdo de itens pendentes: somente contadores e metadados (SEC-019).
- Cada `QueueKind` tem uma instância própria; encerramento ou saturação de um estágio não altera os demais.

## Fora de escopo

- autorização de ações OBS, IPC, persistência, providers, rede, TTS, chat ou comandos OBS;
- números de produto e políticas de configuração por estágio (TASK-014);
- validação e normalização de entradas (TASK-009);
- timeout, cancellation, retry e erros normalizados (TASK-013);
- health, diagnósticos e métricas locais (TASK-044).

## Rastreabilidade

| Critério da TASK-008 | Implementação | Evidência automatizada |
|---|---|---|
| Carga excedente não expande memória | `Capacity` + políticas `Reject`/`DiscardOldest` não bloqueantes | `QueueFlowTests`, `QueueSecurityTests` (flood adversarial) |
| Saturação é observável | `TotalAccepted`, `TotalRejected`, `TotalDropped`, `Count`, `IsFull` | `BoundedWorkBufferTests`, `QueueFlowTests` |
| Uma fila lenta não bloqueia as demais | buffers e runners independentes por `QueueKind` | `QueueFlowTests.SlowStageQueue_DoesNotBlockIndependentStageQueues` |
| Limites inválidos são rejeitados | validação em `WorkQueueSettings.Create` | `WorkQueueSettingsTests` |
| Falhas de item não derrubam o runner | isolamento por item e `onItemFailure` | `QueueFailureTests`, `WorkQueueRunnerTests` |
| Fechamento sem trabalho órfão | `ShutdownAsync` com drain/cancelamento | `WorkQueueRunnerTests`, `QueueFailureTests` |
| Superfície sem expor conteúdo ou autoridade | `IWorkBuffer`/`IWorkQueueRunner` sem conteúdo de itens | `QueueSecurityTests`, `ApplicationQueueArchitectureTests` |

Requisitos cobertos: RF-013, RF-021, RF-025; RNF-008, RNF-016; SEC-014, SEC-015, SEC-019 e SEC-021. Decisão aplicada: ADR-003 (filas locais bounded request/response/TTS).