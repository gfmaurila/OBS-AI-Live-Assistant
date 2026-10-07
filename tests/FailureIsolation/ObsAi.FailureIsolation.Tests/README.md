# ObsAi.FailureIsolation.Tests

Suite de testes de **isolamento de falhas** (categoria `FailureIsolation` na [Arquitetura de Testes](../../../docs/testing/TEST_ARCHITECTURE.md)).

- Objetivo: validar falhas, timeout, retry, filas bounded, IPC e encerramento sem derrubar OBS.
- Gate atual: `NOT EXECUTED` (scaffold da TASK-004). Aplicável em `TASK-008`, `TASK-013` e `TASK-026+`.
- Framework: xunit, `net10.0`, packages centralizados em `Directory.Packages.props`.
- Contrato: nenhum `ProjectReference` até a Task correspondente autorizar; sem testes fictícios.
- Comando: `dotnet test --no-build`.
