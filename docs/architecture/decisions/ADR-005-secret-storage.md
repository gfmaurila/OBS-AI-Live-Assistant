# ADR-005 — Armazenamento e lifecycle de secrets

- **ID:** ADR-005
- **Status:** ACCEPTED
- **Data:** 2026-10-07

## Contexto

BYOK e OAuth exigem persistência segura, masking, rotação e remoção sem plaintext em arquivos, SQLite, prompts ou logs.

## Requirements relacionados

RF-018; RNF-003, RNF-006.

## Security Requirements relacionados

SEC-008 a SEC-013, SEC-022, SEC-033.

## Research Evidence

RES-012, RES-013, RES-014, RES-021; SRC-021..030.

## Opções consideradas

Credential Manager; DPAPI user-scope; DPAPI machine-scope; SQLite/config plaintext.

## Decisão

Usar Windows Credential Manager para secrets discretos por usuário. Usar DPAPI user-scope apenas para blobs app-owned quando necessário. Persistir somente referências opacas e metadata não secreta no SQLite. Rejeitar machine-scope como default e qualquer plaintext.

## Justificativa

O modelo acompanha o usuário e oferece APIs explícitas de Write/Read/Delete com menor gestão criptográfica própria.

## Trade-offs

Backup/portabilidade ficam limitados; o lifecycle fica seguro e explícito.

## Consequências positivas

Separação forte de configuração, masking e remoção verificável.

## Consequências negativas

Migração entre máquinas não transporta secrets automaticamente; wrappers/interops precisam revisão.

## Security Impact

Redaction antes de sinks, uso de referências, scopes mínimos e revogação do provider quando aplicável.

## OBS Impact

Plugin e Dock nunca recebem valor integral.

## Implementation Impact

Secret Store port e testes sentinela/lifecycle serão obrigatórios.

## Validation Required

Usuário incorreto, logs/errors, replace/delete, upgrade/repair/uninstall e OAuth revocation.

## Supersedes

Nenhum.

## Superseded By

Nenhum.
