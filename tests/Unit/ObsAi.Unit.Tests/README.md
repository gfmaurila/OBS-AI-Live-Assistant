# ObsAi.Unit.Tests

Suite de testes de **unidade** (categoria `Unit` na [Arquitetura de Testes](../../../docs/testing/TEST_ARCHITECTURE.md)).

- Objetivo: validar regras de domínio e convenções puras de `Domain`/`Application`.
- Gate atual: `NOT EXECUTED` (scaffold da TASK-004). Aplicável a partir de `TASK-005+`.
- Framework: xunit, `net10.0`, packages centralizados em `Directory.Packages.props`.
- Contrato: nenhum `ProjectReference` até a Task correspondente autorizar; sem testes fictícios.
- Comando: `dotnet test --no-build`.
