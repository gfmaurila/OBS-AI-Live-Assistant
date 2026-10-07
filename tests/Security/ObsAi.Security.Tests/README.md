# ObsAi.Security.Tests

Suite de testes de **segurança** (categoria `Security` na [Arquitetura de Testes](../../../docs/testing/TEST_ARCHITECTURE.md)).

- Objetivo: validar autorização, redaction, secrets e políticas (`SEC-032`).
- Gate atual: `NOT EXECUTED` (scaffold da TASK-004). Aplicável a partir de `TASK-015+`.
- Framework: xunit, `net10.0`, packages centralizados em `Directory.Packages.props`.
- Contrato: nenhum `ProjectReference` até a Task correspondente autorizar; sem testes fictícios; nenhum secret real em testes.
- Comando: `dotnet test --no-build`.
