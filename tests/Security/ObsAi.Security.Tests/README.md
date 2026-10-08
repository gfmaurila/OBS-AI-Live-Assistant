# ObsAi.Security.Tests

Suite de testes de **segurança** (categoria `Security` na [Arquitetura de Testes](../../../docs/testing/TEST_ARCHITECTURE.md)).

- Objetivo: validar minimização, isolamento de sessão, autorização, redaction, secrets e políticas (`SEC-024`, `SEC-025`, `SEC-031`, `SEC-032`).
- Gate atual: `EXECUTED` para minimização e isolamento do domínio da TASK-006; demais controles permanecem `NOT EXECUTED` até suas Tasks.
- Framework: xunit, `net10.0`, packages centralizados em `Directory.Packages.props`.
- Contrato: referência exclusiva a `ObsAi.Domain`, autorizada pela TASK-006; sem testes fictícios; nenhum secret real em testes.
- Comando: `dotnet test --no-build`.
