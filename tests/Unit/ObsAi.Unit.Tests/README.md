# ObsAi.Unit.Tests

Suite de testes de **unidade** (categoria `Unit` na [Arquitetura de Testes](../../../docs/testing/TEST_ARCHITECTURE.md)).

- Objetivo: validar regras de domínio e convenções puras de `Domain`/`Application`.
- Gate atual: `EXECUTED` para invariantes dos contracts da TASK-005 e do modelo de domínio da TASK-006.
- Framework: xunit, `net10.0`, packages centralizados em `Directory.Packages.props`.
- Contrato: referências a `ObsAi.Application` e `ObsAi.Domain`, autorizadas pelas TASK-005 e TASK-006; sem testes fictícios.
- Comando: `dotnet test --no-build`.
