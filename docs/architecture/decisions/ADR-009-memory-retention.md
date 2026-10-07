# ADR-009 — Memória, histórico e retenção

- **ID:** ADR-009
- **Status:** DEFERRED
- **Data:** 2026-10-07

## Contexto

Short-Term Memory é necessária para continuidade limitada; Persistent Memory e prazos de retenção não possuem finalidade/decisão do cliente.

## Requirements relacionados

RF-004, RF-015, RF-029, RF-030; RNF-005, RNF-023.

## Security Requirements relacionados

SEC-019, SEC-024, SEC-025, SEC-031.

## Research Evidence

RES-011, RES-023, RES-024.

## Opções consideradas

Somente sessão; histórico opt-in; Persistent Memory; retenção ampla.

## Decisão

Adotar apenas memória efêmera, bounded e session-scoped no baseline. Histórico mínimo é opt-in e sujeito a política. Persistent Memory fica desabilitada e adiada até decisão sobre finalidade, consentimento, retenção e exclusão.

## Justificativa

Minimização atende a V1 sem inventar política de produto ou ampliar risco.

## Trade-offs

Menor personalização entre lives em troca de privacidade, simplicidade e recovery previsível.

## Consequências positivas

Sem retenção implícita; resultado tardio não cruza sessão.

## Consequências negativas

Capacidades de continuidade persistente não entram no backlog V1.

## Security Impact

Reduz disclosure, mistura de sessão e crescimento de storage.

## OBS Impact

Nenhum estado de memória pertence ao plugin.

## Implementation Impact

Port opcional futuro; schema não reserva campos especulativos.

## Validation Required

Session isolation, cleanup, delete e ausência de coleta quando desabilitada.

## Supersedes

Nenhum.

## Superseded By

Futuro ADR após Client Decision.
