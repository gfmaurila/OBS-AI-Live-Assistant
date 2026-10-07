# ADR-010 — Estratégia de compatibilidade

- **ID:** ADR-010
- **Status:** ACCEPTED (validado na TASK-003 em 2026-10-07)
- **Data:** 2026-10-07

## Contexto

OBS não promete ABI irrestrita; layouts mudam. .NET 10 não sustenta promessa genérica de Windows 10 em todas as edições.

## Requirements relacionados

RF-032; RNF-019, RNF-024.

## Security Requirements relacionados

SEC-027, SEC-029, SEC-032.

## Research Evidence

RES-001 a RES-003, RES-022, RES-027; SRC-001..007, SRC-055, SRC-056.

## Opções consideradas

Versão OBS exata; faixa 32.x testada; promessa ampla; restringir Windows 10; revisar target/runtime.

## Decisão

Usar OBS 32.x x64 como baseline, declarar versão mínima e matriz testada por release, detectar versão/RPC/capabilities e falhar fechado. Windows 11 x64 é baseline confirmável; Windows 10 só será prometido para edições oficialmente suportadas pelo runtime ou após revisão explícita do target.

## Justificativa

Compatibilidade declarada deve refletir evidência, não o nome amplo do SO/major.

## Trade-offs

Mais matrix tests e política comercial pendente; menos risco de promessa inválida.

## Consequências positivas

Installer/runtime version-aware e regressões detectáveis.

## Consequências negativas

Faixa final depende de Client Decision e capacidade de laboratório.

## Security Impact

Combinações desconhecidas são recusadas; dependências e binários são fixados/auditados.

## OBS Impact

Plugin é reconstruído/testado por versão suportada e layout correto.

## Implementation Impact

Matriz exata é definida antes do release, não bloqueia backlog.

## Validation Required

Smoke/regression em versões declaradas, unknown version, layout, RPC e Windows editions.

## Validação executada (TASK-003)

- Protótipo read-only `prototypes/compat-sniff` executado em 2026-10-07 no ambiente de desenvolvimento declarado (Windows 11 25H2 x64 build 26200, .NET runtime 10.0.12, OBS 32.1.2 com `obs-websocket.dll` presente): dimensões SO/.NET/OBS classificadas `supported`; veredito `supported`; exit 0.
- Matriz de compatibilidade estabelecida em `docs/architecture/compatibility/COMPATIBILITY_MATRIX.md`: baseline, versão mínima (OBS 32.1.x, mínimo testado 32.1.2), combinações testadas, fail-closed para combinações desconhecidas e política de Windows 10/.NET 10 encaminhada (CLIENT_DECISION antes do release).
- Controles determinísticos adicionados (RNF-019, SEC-032) validam invariantes da matriz via `ObsAi.Architecture.Tests` (Compatibility Matrix Gate).
- A decisão permanece a mesma e sem ampliação da promessa de suporte; não foi registrado blocker.

## Supersedes

Nenhum.

## Superseded By

Nenhum.
