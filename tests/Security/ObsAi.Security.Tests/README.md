# ObsAi.Security.Tests

Suite de testes de **segurança** (categoria `Security` na [Arquitetura de Testes](../../../docs/testing/TEST_ARCHITECTURE.md)).

- Objetivo: validar minimização, isolamento de sessão, autorização, redaction, secrets e políticas (`SEC-017`, `SEC-020`, `SEC-021`, `SEC-024`, `SEC-025`, `SEC-031`, `SEC-032`).
- Gate atual: `EXECUTED` para minimização/isolamento do domínio da TASK-006 e para autoridade de lease, cancelamento e sessão da TASK-007; demais controles permanecem `NOT EXECUTED` até suas Tasks.
- Framework: xunit, `net10.0`, packages centralizados em `Directory.Packages.props`.
- Contrato: referências a `ObsAi.Application` e `ObsAi.Domain`, autorizadas pelas TASK-006 e TASK-007; sem testes fictícios; nenhum secret real em testes.
- Comando: `dotnet test --no-build`.
