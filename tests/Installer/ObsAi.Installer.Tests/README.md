# ObsAi.Installer.Tests

Suite de testes do **installer** (categoria `Installer` na [Arquitetura de Testes](../../../docs/testing/TEST_ARCHITECTURE.md)).

- Objetivo: validar detecção de ambiente, upgrade/repair/uninstall/rollback.
- Gate atual: `NOT EXECUTED` (scaffold da TASK-004). Aplicável a partir de `TASK-046+`.
- Framework: xunit, `net10.0`, packages centralizados em `Directory.Packages.props`.
- Contrato: nenhum `ProjectReference` até a Task correspondente autorizar; sem testes fictícios.
- Comando: `dotnet test --no-build`.
