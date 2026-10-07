# ADR-006 — TTS e roteamento de áudio

- **ID:** ADR-006
- **Status:** PROPOSED
- **Data:** 2026-10-07

## Contexto

TTS deve ser cancelável, limitado e independente do texto, sem rede/síntese em callback OBS. O melhor routing depende de protótipo.

## Requirements relacionados

RF-023 a RF-025; RNF-002, RNF-015, RNF-025.

## Security Requirements relacionados

SEC-005, SEC-015 a SEC-021.

## Research Evidence

RES-005, RES-006, RES-017; SRC-008..011, SRC-040..043.

## Opções consideradas

Source nativa; Application Audio Capture; virtual audio device.

## Decisão

Gerar TTS no Core e usar fila bounded. Prototipar primeiro Application Audio Capture; manter source nativa atrás de port se tracks/mixer exigirem. Não adotar virtual driver por padrão.

## Justificativa

Minimiza código in-process enquanto preserva caminho para controle nativo comprovado.

## Trade-offs

Captura externa simplifica isolamento, mas pode exigir configuração; source oferece melhor mixer com maior blast radius.

## Consequências positivas

Texto continua com TTS falho; provider e routing são substituíveis.

## Consequências negativas

Decisão final depende de latência, eco, monitoring e tracks.

## Security Impact

Somente texto aprovado alcança TTS; duração, fila e provider são limitados.

## OBS Impact

Nenhuma síntese/rede em thread OBS; source nativa, se adotada, consome buffers prontos.

## Implementation Impact

Spike obrigatório antes de promover para `ACCEPTED`.

## Validation Required

Latência, mute/volume, cancel, crash, monitoramento, eco, recording/stream tracks e device changes.

## Supersedes

Nenhum.

## Superseded By

Nenhum.
