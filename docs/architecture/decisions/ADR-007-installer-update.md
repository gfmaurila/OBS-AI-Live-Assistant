# ADR-007 — Installer e update

- **ID:** ADR-007
- **Status:** PROPOSED
- **Data:** 2026-10-07

## Contexto

Core e plugin têm layouts/lifecycles diferentes. Install, update, repair e uninstall precisam compatibilidade, assinatura e rollback.

## Requirements relacionados

RF-032 a RF-035; RNF-020, RNF-021, RNF-024, RNF-025.

## Security Requirements relacionados

SEC-007, SEC-027 a SEC-029.

## Research Evidence

RES-002, RES-003, RES-018 a RES-022, RES-026, RES-027; SRC-044..056.

## Opções consideradas

WiX/MSI + Burn; Inno Setup; MSIX puro; update manual, installer-based ou in-app.

## Decisão

Adotar installer tradicional, assinado, version-aware e com componentes separados. Preferir update installer-based; aceitar manual inicialmente; deixar in-app automático `FUTURE`. WiX/Burn e Inno Setup continuam finalistas até spike.

## Justificativa

O deployment externo do plugin e rollback favorecem controle tradicional; MSIX puro não cobre bem o layout.

## Trade-offs

Tecnologia final aberta reduz falsa certeza, mas exige spike antes do Installation Design final.

## Consequências positivas

Detecção, rollback, repair e assinatura em um lifecycle controlado.

## Consequências negativas

Mais testes em máquina limpa e tratamento de elevação/layout por versão.

## Security Impact

Authenticode/timestamp, hash/origem, least privilege, OBS fechado e supply-chain gate.

## OBS Impact

Nunca substituir plugin com OBS em execução nem tocar configuração não autorizada.

## Implementation Impact

Spike WiX/Burn versus Inno; default de dados/secrets no uninstall exige client decision.

## Validation Required

Clean install, incompatibilidade, adulteração, rollback, upgrade, repair, uninstall e coexistência.

## Supersedes

Nenhum.

## Superseded By

Nenhum.
