# ObsAi.Contract.Tests

Suite de testes de **contratos** (categoria `Contracts` na [Arquitetura de Testes](../../../docs/testing/TEST_ARCHITECTURE.md)).

- Objetivo: validar ports, contratos IPC e envelope versionado.
- Gate atual: `EXECUTED` para ports da TASK-005; contratos IPC permanecem `NOT EXECUTED` até TASK-022+.
- Framework: xunit, `net10.0`, packages centralizados em `Directory.Packages.props`.
- Contrato: referência exclusiva a `ObsAi.Application`, autorizada pela TASK-005; sem testes fictícios.
- Comando: `dotnet test --no-build`.
