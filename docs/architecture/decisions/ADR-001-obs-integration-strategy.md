# ADR-001 — Estratégia híbrida de integração OBS

- **ID:** ADR-001
- **Status:** ACCEPTED
- **Data:** 2026-10-07

## Contexto

Dock e eventual source de áudio exigem APIs nativas; estado, eventos e requests já expostos podem usar obs-websocket. Lógica dentro do OBS amplia o blast radius.

## Requirements relacionados

RF-001, RF-005, RF-022, RF-032, RF-036; RNF-001, RNF-006, RNF-025.

## Security Requirements relacionados

SEC-006, SEC-007, SEC-020, SEC-021, SEC-030.

## Research Evidence

RES-001, RES-004, RES-007 e RES-008; SRC-001..013.

## Opções consideradas

Plugin nativo completo; somente obs-websocket; arquitetura Hybrid.

## Decisão

Adotar Hybrid: Native OBS Component mínimo para lifecycle, Dock, frontend events, bridge e áudio somente se validado; obs-websocket para capabilities já expostas; regras, providers, dados e segurança no Core externo.

## Justificativa

É a menor combinação que cobre capabilities nativas e maximiza isolamento.

## Trade-offs

Mais componentes e contratos; menor código privilegiado in-process e melhor substituibilidade.

## Consequências positivas

OBS protegido, capability discovery e manutenção localizada.

## Consequências negativas

Exige IPC, compatibilidade dupla e testes de integração.

## Security Impact

Allowlist e autorização continuam no Core; plugin não confia em chat/AI/IPC por origem.

## OBS Impact

Callbacks curtos, nenhuma rede/provider/DB e degradação quando o Core cair.

## Implementation Impact

Dois adapters OBS e contrato estreito; nenhum foi implementado.

## Validation Required

Smoke tests por versão, perda de Core, Dock lifecycle e capability negotiation.

## Supersedes

Nenhum.

## Superseded By

Nenhum.
