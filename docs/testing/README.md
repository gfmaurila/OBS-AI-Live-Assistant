# Documentação de Testing

A fundação de testes e quality gates foi estabelecida pela `TASK-004`. A fonte canônica é a [Arquitetura de Testes e Quality Gates](TEST_ARCHITECTURE.md), que define categorias, projeto/harness, comandos e gates por estágio.

## Estado atual

| Categoria | Suite | Gate atual |
|---|---|---|
| Architecture | `tests/Architecture/ObsAi.Architecture.Tests` | EXECUTADO (testes determinísticos; inclui `TestingFoundationTests`) |
| Unit | `tests/Unit/ObsAi.Unit.Tests` | EXECUTED — invariantes dos contracts da TASK-005, do domínio da TASK-006 e do lifecycle da TASK-007 |
| Integration | `tests/Integration/ObsAi.Integration.Tests` | EXECUTED — fluxos de lifecycle de sessão da TASK-007; adapters e providers TASK-015+ |
| Contracts | `tests/Contracts/ObsAi.Contract.Tests` | EXECUTED — ports da TASK-005; IPC NOT EXECUTED até TASK-022+ |
| Security | `tests/Security/ObsAi.Security.Tests` | EXECUTED — minimização e isolamento de dados da TASK-006 e autoridade de lease da TASK-007; demais controles TASK-015+ |
| FailureIsolation | `tests/FailureIsolation/ObsAi.FailureIsolation.Tests` | EXECUTED — shutdown ordenado e falhas de publicação da TASK-007; filas, timeout e IPC TASK-008/013/026+ |
| Installer | `tests/Installer/ObsAi.Installer.Tests` (scaffold) | Âncora EXECUTED; comportamento de produto NOT EXECUTED — TASK-046+ |
| ObsCompatibility | harness `prototypes/compat-sniff` + `CompatibilityMatrixTests` | PARTIAL — smoke (TASK-003); formal NOT CREATED (TASK-051) |
| Regression/E2E | consolidação em TASK-049/TASK-050 | NOT CREATED |

Os projetos são xunit (net10.0) e não contêm testes fictícios. A TASK-005 ativou Unit e Contracts para `ObsAi.Application`; a TASK-006 ampliou Unit para `ObsAi.Domain` e ativou Security para o domínio; a TASK-007 ampliou Unit, Security, Integration e FailureIsolation para `ObsAi.Application` e `ObsAi.Domain`. Installer permanece como scaffold sem `ProjectReference` até sua Task autorizadora (verificado por `TestingFoundationTests`).

## Comandos canônicos

| Comando | Gate |
|---|---|
| `dotnet restore` | Restore |
| `dotnet build --no-restore` | Build |
| `dotnet test --no-build` | Testes determinísticos |
| `dotnet format --verify-no-changes --no-restore` | Format (solucao) |
| `dotnet format --verify-no-changes --no-restore prototypes\compat-sniff\CompatibilityProbe.csproj` | Format (prototipo) |
| `dotnet run --project prototypes\compat-sniff\CompatibilityProbe.csproj` | ObsCompatibility smoke |
| `powershell -ExecutionPolicy Bypass -File tooling\quality-gates.ps1` | Runner determinístico de conveniência |

Gates não executados permanecem identificados (`NOT EXECUTED` / `NOT CREATED` / `NOT APPLICABLE`) e nunca são aprovados sem comandos e artefatos reais. Detalhes por categoria e rastreabilidade no `TEST_ARCHITECTURE.md`.
