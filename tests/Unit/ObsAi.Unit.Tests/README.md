# ObsAi.Unit.Tests

Suite de testes de **unidade** (categoria `Unit` na [Arquitetura de Testes](../../../docs/testing/TEST_ARCHITECTURE.md)).

- Objetivo: validar regras de domínio e convenções puras de `Domain`/`Application`.
- Gate atual: `EXECUTED` para invariantes dos contracts da TASK-005, do modelo de domínio da TASK-006 e do lifecycle da TASK-007.
- Framework: xunit, `net10.0`, packages centralizados em `Directory.Packages.props`.
- Contrato: referências a `ObsAi.Application` e `ObsAi.Domain`, autorizadas pelas TASK-005, TASK-006 e TASK-007; sem testes fictícios.
- Comando: `dotnet test --no-build`.
