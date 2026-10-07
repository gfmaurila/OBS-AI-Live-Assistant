# ADR-004 — Provider architecture e persistência local

- **ID:** ADR-004
- **Status:** ACCEPTED
- **Data:** 2026-10-07

## Contexto

Chat, AI e TTS variam por vendor; dados autorizados precisam persistir sem acoplar regras ao SQLite ou misturar secrets.

## Requirements relacionados

RF-002, RF-005, RF-016 a RF-019, RF-024, RF-028 a RF-030; RNF-011, RNF-012, RNF-017, RNF-022.

## Security Requirements relacionados

SEC-001, SEC-005, SEC-022 a SEC-024, SEC-033, SEC-034.

## Research Evidence

RES-011, RES-014, RES-015, RES-017, RES-024; SRC-018..020, SRC-024..043.

## Opções consideradas

SDKs/vendors no Core; ports específicos; abstração universal; SQLite; arquivos; servidor SQL.

## Decisão

Definir ports pequenos equivalentes a `IChatProvider`, `IAiProvider`, `ITtsProvider` e persistence repositories, com capabilities opcionais e erros comuns. Adapters encapsulam vendors. SQLite é a store relacional V1, single-writer, com migrations, integrity/recovery e Backup API. Secrets ficam fora.

## Justificativa

Preserva regras centrais e BYOK sem inventar infraestrutura ou menor denominador falso.

## Trade-offs

Normalização e contract tests têm custo; peculiaridades ficam contidas e substituíveis.

## Consequências positivas

OCP/DIP, testabilidade e deployment local simples.

## Consequências negativas

Features exclusivas exigem capability negotiation; schema/lifecycle ainda requer Data Design.

## Security Impact

Cada adapter valida input/output, destino e privacy; DB não armazena plaintext secrets.

## OBS Impact

Providers e SQLite jamais executam no processo OBS.

## Implementation Impact

Selecionar poucos adapters V1 e versão SQLite corrigida; vendors concretos permanecem decisão de produto.

## Validation Required

Provider contract tests, DB corruption/lock/migration/backup e privacy review por provider.

## Supersedes

Nenhum.

## Superseded By

Nenhum.
