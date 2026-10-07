# Release Plan — V1 / 1.0.0

Este plano descreve readiness; não cria branch, tag ou GitHub Release.

## Escopo obrigatório

A V1 inclui todas as 52 Tasks do backlog, exceto capabilities explicitamente condicionais que permaneçam desabilitadas por decisão aprovada. Persistent Memory, Twitch, IA local não selecionada, automatic updater in-app e demais itens `OUT_OF_SCOPE_V1` não são requisitos de release.

## Gates de V1

- TASK-003 valida ADR-010 e a matriz OBS 32.x / Windows / .NET 10;
- TASK-038 valida ADR-006 antes de qualquer áudio definitivo;
- TASK-045 valida ADR-007 antes do installer definitivo;
- TASK-048 comprova supply chain, integridade e signing readiness;
- TASK-049 a TASK-051 consolidam regressão, segurança, failure isolation, desempenho, installer e compatibilidade;
- TASK-052 reúne a evidência para um futuro HML Gate.

O release exige 36/36 RF, 28/28 RNF e 34/34 SEC cobertos; ADRs necessários em estado compatível; Critical/High = 0; Secrets = NONE; installer, OBS compatibility, rollback e failure isolation aprovados.

## Fluxo governado

```text
feature/task-* -> develop
develop -> HML GATE -> hml
hml aprovado -> release/1.0.0XXXX
release/1.0.0XXXX -> RELEASE/PRODUCTION GATE -> main
main -> tag / GitHub Release, quando autorizado
```

`XXXX` é a sequência incremental real, por exemplo `release/1.0.00001`. Ela é escolhida somente na criação autorizada da release; este plano não reserva número.

Promoções `develop -> hml`, criação de `release/*`, merge em `main`, tag e GitHub Release exigem autorizações e gates próprios.
