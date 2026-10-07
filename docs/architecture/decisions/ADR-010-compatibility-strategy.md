# ADR-010 — Estratégia de compatibilidade

- **ID:** ADR-010
- **Status:** PROPOSED
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

## Supersedes

Nenhum.

## Superseded By

Nenhum.
