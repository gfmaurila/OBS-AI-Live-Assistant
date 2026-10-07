# ObsAi.Contract.Tests

Suite de testes de **contratos** (categoria `Contracts` na [Arquitetura de Testes](../../../docs/testing/TEST_ARCHITECTURE.md)).

- Objetivo: validar ports, contratos IPC e envelope versionado.
- Gate atual: `NOT EXECUTED` (scaffold da TASK-004). Aplicável em `TASK-005` (ports) e `TASK-022+` (IPC).
- Framework: xunit, `net10.0`, packages centralizados em `Directory.Packages.props`.
- Contrato: nenhum `ProjectReference` até a Task correspondente autorizar; sem testes fictícios.
- Comando: `dotnet test --no-build`.
