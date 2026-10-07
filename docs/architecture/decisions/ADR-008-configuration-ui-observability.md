# ADR-008 — Configuração, UI e observabilidade

- **ID:** ADR-008
- **Status:** ACCEPTED
- **Data:** 2026-10-07

## Contexto

O streamer precisa controlar e diagnosticar o Assistant sem colocar lógica, secrets ou trabalho pesado no Dock.

## Requirements relacionados

RF-002 a RF-004, RF-009 a RF-012, RF-031; RNF-004, RNF-013, RNF-014, RNF-027.

## Security Requirements relacionados

SEC-002 a SEC-004, SEC-010, SEC-011, SEC-026.

## Research Evidence

RES-004, RES-025; SRC-003, SRC-051, SRC-052.

## Opções consideradas

UI toda nativa; UI externa; Dock fino + Core; logs livres versus estruturados/redacted.

## Decisão

Usar Dock fino para status e controles essenciais, com estado no Core. Validar configuração antes de aplicar. Adotar logs locais estruturados, correlation IDs, health por adapter, redaction central, rotação e limites; sem telemetria cloud obrigatória.

## Justificativa

Preserva UX integrada e responsividade, mantendo dados e regras fora do OBS.

## Trade-offs

IPC para toda interação do Dock; isolamento e testabilidade superiores.

## Consequências positivas

Diagnóstico consistente, acessível e sem exposição de secrets.

## Consequências negativas

Dock degradado depende de estados/cache mínimos e contratos de apresentação.

## Security Impact

Masking, allowlist de campos, redaction irremovível por log level e confirmação de ações sensíveis.

## OBS Impact

UI não bloqueia frontend e tolera Core ausente.

## Implementation Impact

View models e observability ports separados; nenhuma UI criada nesta fase.

## Validation Required

Keyboard operation, status degraded, log rotation/redaction, sentinel secrets e Core disconnect.

## Supersedes

Nenhum.

## Superseded By

Nenhum.
