# ObsAi.Integration.Tests

Suite de testes de **integração** (categoria `Integration` na [Arquitetura de Testes](../../../docs/testing/TEST_ARCHITECTURE.md)).

- Objetivo: validar pipelines, adapters e fluxos entre boundaries.
- Gate atual: `NOT EXECUTED` (scaffold da TASK-004). Aplicável a partir de `TASK-007+`.
- Framework: xunit, `net10.0`, packages centralizados em `Directory.Packages.props`.
- Contrato: nenhum `ProjectReference` até a Task correspondente autorizar; sem testes fictícios.
- Comando: `dotnet test --no-build`.
