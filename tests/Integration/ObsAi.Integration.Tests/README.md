# ObsAi.Integration.Tests

Suite de testes de **integração** (categoria `Integration` na [Arquitetura de Testes](../../../docs/testing/TEST_ARCHITECTURE.md)).

- Objetivo: validar pipelines, adapters e fluxos entre boundaries.
- Gate atual: `EXECUTED` para fluxos de lifecycle de sessão da `TASK-007`; adapters e providers `NOT EXECUTED` a partir de `TASK-015+`.
- Framework: xunit, `net10.0`, packages centralizados em `Directory.Packages.props`.
- Contrato: referências a `ObsAi.Application` e `ObsAi.Domain`, autorizadas pela `TASK-007`; sem testes fictícios.
- Comando: `dotnet test --no-build`.
