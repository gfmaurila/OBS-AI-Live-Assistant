# ObsAi.Unit.Tests

Suite de testes de **unidade** (categoria `Unit` na [Arquitetura de Testes](../../../docs/testing/TEST_ARCHITECTURE.md)).

- Objetivo: validar regras de domínio e convenções puras de `Domain`/`Application`.
- Gate atual: `EXECUTED` para invariantes dos contracts da TASK-005.
- Framework: xunit, `net10.0`, packages centralizados em `Directory.Packages.props`.
- Contrato: referência exclusiva a `ObsAi.Application`, autorizada pela TASK-005; sem testes fictícios.
- Comando: `dotnet test --no-build`.
