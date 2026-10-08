# ObsAi.FailureIsolation.Tests

Suite de testes de **isolamento de falhas** (categoria `FailureIsolation` na [Arquitetura de Testes](../../../docs/testing/TEST_ARCHITECTURE.md)).

- Objetivo: validar falhas, timeout, retry, filas bounded, IPC e encerramento sem derrubar OBS.
- Gate atual: `EXECUTED` para shutdown ordenado e falhas de publicação da `TASK-007`; filas, timeout e IPC `NOT EXECUTED` em `TASK-008`, `TASK-013` e `TASK-026+`.
- Framework: xunit, `net10.0`, packages centralizados em `Directory.Packages.props`.
- Contrato: referências a `ObsAi.Application` e `ObsAi.Domain`, autorizadas pela `TASK-007`; sem testes fictícios.
- Comando: `dotnet test --no-build`.
