# ADR-003 — Isolamento, lifecycle e filas

- **ID:** ADR-003
- **Status:** ACCEPTED
- **Data:** 2026-10-07

## Contexto

Falhas, hangs, rajadas e providers lentos não podem afetar o OBS nem gerar consumo ilimitado.

## Requirements relacionados

RF-001, RF-013, RF-021, RF-025; RNF-001, RNF-008, RNF-016, RNF-026.

## Security Requirements relacionados

SEC-014 a SEC-021.

## Research Evidence

RES-008, RES-010; SRC-017.

## Opções consideradas

Core in-process; processo separado; broker externo.

## Decisão

Executar Assistant Core em processo separado. Usar queues locais bounded para request, response e TTS, concorrência explícita, backpressure, cancellation e shutdown ordenado. Rejeitar broker externo no V1.

## Justificativa

O process boundary contém crash/recursos; mecanismos locais bastam à escala e deployment do V1.

## Trade-offs

IPC e lifecycle adicionais em troca de contenção forte e operação previsível.

## Consequências positivas

Assistant failure != OBS failure; saturação controlada.

## Consequências negativas

Coordenação de startup/reconnect/shutdown e diagnóstico distribuído entre dois processos.

## Security Impact

Limita DoS e blast radius; fallback nunca remove controles.

## OBS Impact

OBS continua ativo com Core ausente, lento ou encerrado.

## Implementation Impact

Host/supervision e políticas de queue serão Tasks futuras; Job Objects são opção, não obrigação.

## Validation Required

Crash/hang, queue saturation, shutdown, late results e recovery sem replay indevido.

## Supersedes

Nenhum.

## Superseded By

Nenhum.
