# Timeout, cancellation, retry e erros normalizados

Status: **IMPLEMENTAÇÃO LOCAL PRONTA PELA TASK-013; INTEGRAÇÃO PENDENTE**.

Este documento registra o mecanismo vendor-neutral de resiliência materializado em `ObsAi.Application.Resilience`. Ele aplica limites configuráveis a operações potencialmente demoradas sem introduzir provider, rede, persistência, IPC, capability OBS ou números de produto.

## Contratos

- `ResiliencePolicy` congela timeout por tentativa, timeout total opcional, máximo de tentativas e uma agenda de delays entre tentativas. A quantidade de delays deve corresponder exatamente ao máximo configurado; os valores concretos pertencem à configuração futura.
- `OperationIdempotency` exige declaração explícita: `NonIdempotent` é o estado seguro para operações que podem publicar, cobrar ou produzir outro efeito; somente `Idempotent` habilita retry.
- `ResilienceExecutor` recebe uma operação assíncrona, política, idempotência, deadline opcional e `CancellationToken`. `TimeProvider` permite tempo determinístico em testes sem dependência externa.
- `FailureCategory` deriva de `ProviderFailureCode` e preserva `Timeout`, `Cancelled`, `TransientFailure`, `PermanentFailure`, `InvalidInput` e `InternalFailure`.

## Timeout e resultados tardios

Cada tentativa disputa sua conclusão com o timeout aplicável. O menor limite entre timeout da tentativa, timeout total restante e deadline da request governa a tentativa. Ao expirar, o token ligado à operação é cancelado, a capacidade do chamador é liberada e o resultado público é `TimedOut`.

Cancelamento cooperativo não garante interrupção imediata de uma dependência externa. Por isso, uma conclusão posterior é somente observada para impedir fault não observado; ela nunca substitui o timeout já retornado nem adquire autoridade para publicação. A autoridade final continua sendo validada pelo `OperationLease` da TASK-007.

## Cancellation

- token já cancelado impede a invocação;
- cancelamento durante operação cancela o token ligado e produz `Cancelled`;
- cancelamento durante backoff impede nova tentativa;
- cancelamento após conclusão não reescreve um resultado já concluído;
- shutdown/cancelamento de sessão flui pelo token do lease e `TryCommitResult` rejeita qualquer resultado sem autoridade.

## Retry e idempotência

Retry exige simultaneamente:

1. tentativa disponível dentro de `MaximumAttempts`;
2. operação declarada `Idempotent`;
3. falha marcada transitória e com código allowlisted (`TimedOut`, `RateLimited` ou `Unavailable`);
4. cancellation e timeout total ainda não expirados.

Falhas de input, autenticação, autorização, quota, resposta inválida, capability incompatível e erro interno não são repetidas, mesmo se um adapter as marcar incorretamente como transitórias. `RetryAfter` pode ampliar o delay configurado, nunca encurtá-lo. A agenda pode conter valores previamente jittered; o mecanismo não inventa defaults nem executa busy waiting.

## Erros seguros

Exceções síncronas ou assíncronas inesperadas viram `ProviderFailureCode.Unknown`/`InternalFailure` com mensagem constante. Mensagem de exception, stack trace, payload e detalhe de vendor não atravessam o boundary. Redaction geral e logging pertencem às TASK-016 e TASK-043.

## Integrações existentes

- **Lifecycle:** o executor aceita o `CancellationToken` do `OperationLease`; cancelamento libera a lease e `TryCommitResult` impede output tardio.
- **Bounded queues:** o executor pode ser usado dentro de um handler de `WorkQueueRunner<T>`; cancellation de shutdown continua propagada e falha normalizada não derruba o runner.
- **Providers:** adapters futuros devolvem `ProviderResult<T>` e não são conhecidos pelo mecanismo.

## Fora de escopo

Adapters concretos, chamadas externas, IPC, circuit breaker tecnológico, configuração/persistência de valores, observabilidade operacional, redaction ampla, seleção de provider, publicação e TTS reais.

## Rastreabilidade

| Drivers | Implementação | Evidência |
|---|---|---|
| RF-026, RNF-010, SEC-017 | cancellation propagada e resultado tardio descartado | Unit e Security |
| RF-027, RNF-009, SEC-016 | timeout por tentativa/total/deadline e resultado explícito | Unit e FailureIsolation |
| RNF-011, SEC-018 | retry limitado, cancelável, allowlisted e idempotente | Unit, Contract, FailureIsolation e Security |
| RNF-026, SEC-021 | integração com lease/shutdown e degradação sem ampliar efeito | Security e Integration |
| ADR-002 a ADR-004 | contrato reutilizável por boundaries, queues locais e providers | Architecture e Contract |
