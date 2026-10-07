# ADR-002 — IPC local por Named Pipes

- **ID:** ADR-002
- **Status:** ACCEPTED
- **Data:** 2026-10-07

## Contexto

O boundary C++/.NET precisa de duplex local, timeout, cancellation, versionamento e controle de acesso sem dependência externa.

## Requirements relacionados

RF-026, RF-027; RNF-004, RNF-009, RNF-010, RNF-026.

## Security Requirements relacionados

SEC-016, SEC-017, SEC-030, SEC-031.

## Research Evidence

RES-009, RES-010; SRC-014..017.

## Opções consideradas

Named Pipes; gRPC; local sockets; shared memory; COM.

## Decisão

Usar Named Pipes duplex locais, DACL explícita para usuário/logon SID, rede negada, instâncias/tamanho limitados e protocolo framed/versionado com request/response, events, correlation, session, timeout, cancellation e reconnect.

## Justificativa

Interop nativo C++/.NET, segurança Windows e deployment simples atendem o V1.

## Trade-offs

O projeto mantém protocolo próprio; evita runtime/dependências e superfície de rede.

## Consequências positivas

Boundary autenticável, local e failure-aware.

## Consequências negativas

Versionamento, framing e contract tests são responsabilidade do produto.

## Security Impact

ACL default é proibida; mensagens inválidas, repetidas, incompatíveis ou oversize falham fechado.

## OBS Impact

I/O assíncrona nunca bloqueia threads críticas; perda do pipe apenas degrada o plugin.

## Implementation Impact

Integration Design definirá envelope e handshake; nada implementado nesta fase.

## Validation Required

DACL, peer identity, malformed/oversize, replay, timeout, reconnect, backpressure e version mismatch.

## Supersedes

Nenhum.

## Superseded By

Nenhum.
